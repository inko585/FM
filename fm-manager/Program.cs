using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;
using FM.Entities.Base;
using FM.Common;

namespace fm_manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //AEMySQLDatabase.CreateLocalDatabase("fm_gen", "6666", "fm_admin", "fm_admin");

            Application.Run(new Form1());
        }

        //public static Gen_Entities Entities = new Gen_Entities
        public static BindingList<Nation> Nations = new BindingList<Nation>();
        public static BindingList<Ethnie> Ethnies = new BindingList<Ethnie>();
        public static BindingList<Association> Associations = new BindingList<Association>();
        public static BindingList<AssociationLook> AssociationLooks = new BindingList<AssociationLook>();
        public static BindingList<PlayerLook> PlayerLooks = new BindingList<PlayerLook>();

        public static void Export()
        {
            var xs = new XmlSerializer(typeof(FM.Entities.Base.World));
            string path;
            var entities = new World()
            {
                Nations = Nations.ToList(),
                Ethnies = Ethnies.ToList(),
                Associations = Associations.ToList(),
                AssociationLooks = AssociationLooks.ToList(),
                PlayerLooks = PlayerLooks.ToList()
            };

            if (Util.TryGetXMLSavePath("gendata1.xml", out path))
            {
                using (var tw = new StreamWriter(path))
                {
                    xs.Serialize(tw, entities);
                }

            }

        }

        public static void Import()
        {
            string path;

            if (Util.TryGetXMLOpenPath(out path))
            {
                var w = World.ReadWorld(path);
                Nations.Clear();
                Ethnies.Clear();
                Associations.Clear();
                AssociationLooks.Clear();
                PlayerLooks.Clear();
                foreach (var n in w.Nations)
                {
                    Nations.Add(n);
                }
                foreach (var e in w.Ethnies)
                {
                    Ethnies.Add(e);
                }
                foreach(var a in w.Associations)
                {
                    Associations.Add(a);
                }

                foreach (var a in w.AssociationLooks)
                {
                    AssociationLooks.Add(a);
                }
                foreach (var p in w.PlayerLooks)
                {
                    PlayerLooks.Add(p);
                }

            }

        }


    }
}
