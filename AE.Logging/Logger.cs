using AE.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AE.Logging
{
    public class Logger
    {
        private static Logger root;

        private Logger(string name)
        {
            try
            {
                if (!Directory.Exists(DefaultDirectory))
                {
                    Directory.CreateDirectory(DefaultDirectory);
                }
            }
            catch
            {
                //empty 
            }
            this.name = name;

            using (var configWatch = new FileSystemWatcher()
            {
                Path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "*.config"
            })
            {
                configWatch.Changed += OnConfigChange;
                configWatch.EnableRaisingEvents = true;
            }
        }

        private static bool configInit = false;

        private void OnConfigChange(object source, FileSystemEventArgs e)
        {
            SetUpFromConfig();
        }

        private static void SetUpFromConfig()
        {
            var section = ConfigurationManager.GetSection("aeLogger") as LoggerConfigurationSection;

            if (section != null)
            {
                foreach (Configuration.Appender ac in section.Appenders)
                {

                    var dir = ac.Directory == "" ? DefaultDirectory : ac.Directory;
                    int ll;
                    switch (ac.LogLevel)
                    {
                        case "all":
                            ll = LOG_ALL;
                            break;
                        case "info":
                            ll = LOG_INFO;
                            break;
                        case "error":
                            ll = LOG_ERROR;
                            break;
                        case "off":
                            ll = LOG_OFF;
                            break;
                        default:
                            ll = LOG_ALL;
                            break;
                    }

                    if (ac.Type == "file")
                    {
                        RootLogger.AddAppenderA(new CustomFileAppender(ac.Name, ll, dir, ac.FileName, ac.HideTimeAndLogLevel));
                    }

                    if (ac.Type == "file_rolling")
                    {
                        RootLogger.AddAppender(new RollingCustomFileAppender(ac.Name, ll, dir, ac.FileName, ac.CapInKB, ac.HideTimeAndLogLevel));
                    }
                }

                foreach (Configuration.AppenderRef ar in section.AppenderRefs)
                {
                    var l = GetLogger(ar.Logger);
                    l.AddAppender(ar.Appender);
                }
            }

        }
        static readonly object key = new object();

        public static Logger GetLogger(string name)
        {
            if (!configInit)
            {
                configInit = true;
                SetUpFromConfig();
            }
            Logger l;
            lock (key)
            {
                if (!loggers.TryGetValue(name, out l))
                {
                    l = new Logger(name);

                    loggers.Add(name, l);
                }
            }
            return l;
        }

        public static Logger RootLogger
        {
            get
            {
                if (root == null)
                {
                    root = new Logger("Root");
                }

                return root;
            }
        }

        public static readonly string DefaultDirectory = Path.Combine(System.Environment.GetEnvironmentVariable("APPDATA"), "AELogs");

        private List<Appender> appenders;
        public List<Appender> Appenders
        {
            get
            {
                if (appenders == null)
                {
                    appenders = new List<Appender>();
                }

                return appenders;
            }
        }

        public Appender GetAppender(string name)
        {
            return Appenders.FirstOrDefault(x => x.Name == name);
        }

        private readonly static Dictionary<string, Logger> loggers = new Dictionary<string, Logger>();

        private readonly string name;

        public static readonly int LOG_OFF = 0;
        public static readonly int LOG_ALL = int.MaxValue;
        public static readonly int LOG_INFO = 2;
        public static readonly int LOG_VERBOSE = 3;
        public static readonly int LOG_ERROR = 1;

        public bool ContainsAppender(string name)
        {
            return Appenders.Any(a => a.Name.Equals(name));
        }

        private void AddAppenderA(Appender a)
        {
            if (a.Name.Equals(""))
            {
                a.Name = name;
            }
            if (Appenders.Contains(a))
            {
                Appenders.Remove(a);
            }
            Appenders.Add(a);
        }


        public void AddAppender(Appender a)
        {

            AddAppenderA(a);

            if (this != RootLogger)
            {
                if (RootLogger.Appenders.Contains(a))
                {
                    RootLogger.Appenders.Remove(a);
                }

                RootLogger.AddAppender(a);
            }
        }

        public void AddAppender(string a)
        {
            var appender = RootLogger.GetAppender(a);
            if (appender != null)
            {
                AddAppenderA(appender);
            }
        }

        public void RemoveAppender(Appender a)
        {
            if (Appenders.Contains(a))
            {
                Appenders.Remove(a);
            }
        }

        public void Log(int flag, string msg)
        {
            foreach (Appender a in Appenders.ToList())
            {
                a.Send(flag, msg, false);
            }
        }

        public void LogException(Exception e)
        {
            foreach (Appender a in Appenders.ToList())
            {
                a.Send(Logger.LOG_ERROR, e.GetType().Name + ": " + e.Message, false);
                a.Send(Logger.LOG_ERROR, e.StackTrace, true);
            }
        }

        public void Debug(string msg)
        {
            Log(LOG_VERBOSE, msg);
        }

        public void Info(string msg)
        {
            Log(LOG_INFO, msg);
        }

        public void Info (string msg, params object[] values)
        {
            Info(Format(msg, values));
        }

        private string Format(string msg, params object[] values)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(msg, values);
            return sb.ToString();
        }

        public void Error(string msg)
        {
            Log(LOG_ERROR, msg);
        }

        public void Error(Exception e)
        {
            LogException(e);
        }
    }

    public abstract class Appender
    {
        protected void TryWriteFile(string path, string output)
        {
            var i = 1;
            var n = 3;
            while (true)
            {
                try
                {
                    using (StreamWriter w = System.IO.File.AppendText(path))
                    {
                        w.WriteLine(output);
                    }

                    break;
                }
                catch (Exception e)
                {
                    if (i < n)
                    {
                        Thread.Sleep(1000);
                        i++;
                    }
                    else
                    {
                        var dt = DateTime.Now;
                        var erPath = Logger.DefaultDirectory + "\\LogError_" + dt.Year + "_" + dt.Month + "_" + dt.Day + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Minute + ".log";

                        using (StreamWriter w = System.IO.File.AppendText(erPath))
                        {
                            w.WriteLine("Error while writing Log");
                            w.WriteLine("Exception: " + e.Message);
                            w.WriteLine("Original Message: " + output);
                        }
                        break;
                    }
                }
            }

        }


        protected Appender(string name, int logLevel, bool hideTimeAndLogLevel)
        {
            this.LogLevel = logLevel;
            this.HideTimeAndLogLevel = hideTimeAndLogLevel;
            this.Name = name;
        }

        protected abstract void Target(int flag, string output);


        public int LogLevel { get; set; }


        public bool HideTimeAndLogLevel { get; set; }

        public string Name { get; set; }

        readonly object key = new object();

        public void Send(int flag, string msg, bool hideTimeAndLogLevel)
        {
            if (flag <= LogLevel)
            {
                lock (key)
                {
                    Target(flag, (this.HideTimeAndLogLevel || hideTimeAndLogLevel) ? msg : DateTime.Now + "\t [" + GetLogLevelName(flag) + "] \t" + msg);

                }
            }
        }

        private string GetLogLevelName(int val)
        {
            foreach (var prop in typeof(Logger).GetFields())
            {
                if (val.ToString() == prop.GetValue(null).ToString() && prop.Name.StartsWith("LOG_"))
                {
                    return prop.Name;
                }
            }
            return "Log_Flag" + val;
        }

        override public bool Equals(object obj)
        {
            if (obj is Appender)
            {
                return this.Name.Equals((obj as Appender).Name);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }

    /// <summary>
    /// Appends the messages to a log stored in a predefined path (%tmp%/AELogs)
    /// </summary>
    public class FileAppender : CustomFileAppender
    {
        public FileAppender(string name, int logLevel, string fileName)
            : base(name, logLevel, Logger.DefaultDirectory, fileName, false)
        {
        }

    }

    /// <summary>
    /// Applies a String Action, for example Console.Write, on the message
    /// </summary>
    public class CustomAppender : Appender
    {

        public CustomAppender(string name, int logLevel, Action<string> msgHandler)
            : base(name, logLevel, true)
        {
            this.Handler = (x, y) => msgHandler(y);
        }

        public CustomAppender(string name, int logLevel, Action<int, string> msgHandler) : base(name, logLevel, true)
        {
            this.Handler = msgHandler;
        }

        public Action<int, string> Handler { get; set; }

        override protected void Target(int flag, string output)
        {
            Handler(flag, output);
        }




    }

    /// <summary>
    /// Appends messages to a log file.
    /// </summary>
    public class CustomFileAppender : Appender
    {
        public string Directory { get; set; }
        public string FileName { get; set; }
        private string File
        {
            get
            {
                return Directory + "\\" + Path.GetFileNameWithoutExtension(FileName) + ".log";
            }
        }

        public CustomFileAppender(string name, int logLevel, string directory, string fileName, bool hideTimeAndLogLevel)
            : base(name, logLevel, hideTimeAndLogLevel)
        {
            FileName = fileName;
            Directory = directory;

        }

        override protected void Target(int flag, string output)
        {

            var dir = new DirectoryInfo(Directory);
            if (!dir.Exists)
            {
                dir.Create();
            }

            TryWriteFile(File, output);
        }

    }

    /// <summary>
    /// Appends messages to a log file.
    /// The log file will be replaced with an empty one, once the capInKiloBytes is reached.
    /// The old log is saved as backup once.
    /// </summary>
    public class RollingCustomFileAppender : Appender
    {
        public RollingCustomFileAppender(string name, int logLevel, string directory, string fileName, int capInKiloByte, bool hideTimeAndLogLevel)
            : base(name, logLevel, hideTimeAndLogLevel)
        {
            this.capInKiloByte = capInKiloByte;
            Directory = directory;
            FileName = fileName;

        }

        public string Directory { get; set; }
        public string FileName { get; set; }
        private string File
        {
            get
            {
                return Directory + "\\" + Path.GetFileNameWithoutExtension(FileName) + ".log";
            }
        }
        private readonly int capInKiloByte;





        override protected void Target(int flag, string output)
        {

            var dir = new DirectoryInfo(Directory);
            if (!dir.Exists)
            {
                dir.Create();
            }

            var fInfo = new FileInfo(File);

            if (fInfo.Exists && fInfo.Length / 1024 >= capInKiloByte)
            {
                var pathseperator = "\\";
                fInfo.CopyTo(Directory + pathseperator + FileName + ".1", true);
                var i = 1;
                var n = 3;
                while (true)
                {
                    try
                    {
                        fInfo.Delete();
                        break;
                    }
                    catch (IOException) when (i <= n)
                    {
                        i++;
                    }
                }
            }

            TryWriteFile(File, output);

        }


    }

    /// <summary>
    /// Appends messages to a log file.
    /// The log file will be replaced with an empty one, once the capInKiloBytes is reached.
    /// The old log is saved as backup once.
    /// 
    /// Uses a predefined path as directory (%tmp%/AELogs)
    /// </summary>
    public class RollingFileAppender : RollingCustomFileAppender
    {
        public RollingFileAppender(string name, int logLevel, string fileName)
            : base(name, logLevel, Logger.DefaultDirectory, fileName, 5000, false)
        {

        }

    }
}

namespace AE.Logging.Configuration
{



    public class LoggerConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("appenders")]
        public AppenderCollection Appenders
        {
            get { return (AppenderCollection)this["appenders"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("appender_refs")]
        public LoggerAppenderCollection AppenderRefs
        {
            get { return (LoggerAppenderCollection)this["appender_refs"]; }
            set { this["appender_refs"] = value; }
        }
    }

    public class AppenderRef : ConfigurationElement
    {

        [ConfigurationProperty("appender", IsRequired = true)]
        public string Appender
        {
            get { return (string)this["appender"]; }
            set { this["appender"] = value; }
        }

        [ConfigurationProperty("logger", IsRequired = true)]
        public string Logger
        {
            get { return (string)this["logger"]; }
            set { this["logger"] = value; }
        }

    }


    public class Appender : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
        [ConfigurationProperty("type", IsRequired = false, DefaultValue = "file")]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["string"] = value; }
        }
        [ConfigurationProperty("logLevel", IsRequired = false, DefaultValue = "all")]
        public string LogLevel
        {
            get { return (string)this["logLevel"]; }
            set { this["logLevel"] = value; }
        }
        [ConfigurationProperty("fileName", IsRequired = true)]
        public string FileName
        {
            get { return (string)this["fileName"]; }
            set { this["fileName"] = value; }
        }
        [ConfigurationProperty("directory", IsRequired = false, DefaultValue = "")]
        public string Directory
        {
            get { return (string)this["directory"]; }
            set { this["directory"] = value; }
        }
        [ConfigurationProperty("hideTimeAndLevel", IsRequired = false, DefaultValue = false)]
        public bool HideTimeAndLogLevel
        {
            get { return (bool)this["hideTimeAndLevel"]; }
            set { this["hideTimeAndLevel"] = value; }
        }
        [ConfigurationProperty("capInKB", IsRequired = false, DefaultValue = 2000)]
        public int CapInKB
        {
            get { return (int)this["capInKB"]; }
            set { this["capInKB"] = value; }
        }
    }



    [ConfigurationCollection(typeof(AppenderRef), AddItemName = "appender_ref")]
    public class LoggerAppenderCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AppenderRef();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var ar = (AppenderRef)element;
            return ar.Logger + "_" + ar.Appender;
        }
    }

    [ConfigurationCollection(typeof(Appender), AddItemName = "appender")]
    public class AppenderCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Appender();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Appender)element).Name;
        }


    }

}





