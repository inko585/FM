using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AE.Graphics
{
    public static class AEGraphics
    {

        private static readonly List<int> DontShowMeAgainIds = new List<int>();

        private static int idCount = 0;

        public static readonly FormStartPosition DialogStartPosition = FormStartPosition.CenterScreen;

        public static int NextId()
        {
            return idCount++;
        }

        public static void ShowMessage(string msg, string title, bool modal = true)
        {

            try
            {
                if (modal)
                {
                    AEMessageBox.ShowDialog(msg, GetTitle(title));
                }
                else
                {
                    AEMessageBox.Show(msg, GetTitle(title));
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Try setting Util.Testmode to true! Original Message: " + e.Message);
            }

        }

        public static void ShowMessage(string msg)
        {
            AEMessageBox.ShowDialog(msg, "autoelectric");
        }

        public static void ShowWarning(string txt, string title)
        {
            try
            {
                AEMessageBox.ShowDialog(txt, GetTitle(title), AEMessageBox.MessageSymbol.Warning);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Try setting Util.Testmode to true! Original Message: " + e.Message);
            }
        }

        public static void ShowError(string msg, string title)
        {
            try
            {
                AEMessageBox.ShowDialog(msg, GetTitle(title), AEMessageBox.MessageSymbol.Error);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Try setting Util.Testmode to true! Original Message: " + e.Message);
            }
        }

        public static Boolean GetUserItemSelection<T>(string msg, string title, string selectionTitle, out T selectedItem, IEnumerable<T> items, Func<T, string> itemDesc)
        {
            using (var dia = new RadioSelectionForm<T>(msg, GetTitle(title), selectionTitle, items, itemDesc))
            {
                if (dia.ShowDialog() == DialogResult.OK)
                {
                    selectedItem = dia.Selection;
                    return true;
                }
                else
                {
                    selectedItem = default;
                    return false;
                }
            }
        }

        public static Boolean GetUserItemMultiSelection<T>(string msg, string title, string selectionTitle, out List<T> selectedItems, IEnumerable<T> items, Func<T, string> itemDesc)
        {

            using (var dia = new CheckBoxSelectionForm<T>(msg, GetTitle(title), selectionTitle, items, itemDesc))
            {
                if (dia.ShowDialog() == DialogResult.OK)
                {
                    selectedItems = dia.Selection;
                    return true;
                }
                else
                {
                    selectedItems = null;
                    return false;
                }
            }

        }

        public static bool GetUserFileSelection(string msg, string title, string fileType, out string filePath)
        {
            try
            {
                using (var dia = new FileSelectionForm(msg, GetTitle(title), fileType))
                {
                    var result = dia.ShowDialog();
                    filePath = dia.Result;
                    return (result == DialogResult.OK);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Try setting Util.Testmode to true! Original Message: " + e.Message);
            }
        }

        public static Boolean GetUserInput(string msg, string title, out string input)
        {
            return GetUserInput(msg, title, "", out input);
        }

        public static Boolean GetUserInput(string msg, string title, string defaultValue, out string input)
        {
            try
            {
                using (var dia = new TextInputForm(msg, GetTitle(title)) { DefaultValue = defaultValue })
                {
                    var result = dia.ShowDialog();
                    input = dia.Result;
                    return (result == DialogResult.OK);
                }   
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Try setting Util.Testmode to true! Original Message: " + e.Message);
            }
        }

        public static Boolean GetUserSourceCode(string pre, string title, out string input)
        {
            try
            {
                using (var dia = new SourceInputForm(pre, GetTitle(title)))
                {
                    var result = dia.ShowDialog();
                    input = dia.Result;
                    return (result == DialogResult.OK);
                } 
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Try setting Util.Testmode to true! Original Message: " + e.Message);
            }
        }


        public static bool GetUserInputNumeric(string msg, string title, out decimal input)
        {
            try
            {
                using (var dia = new NumberInputForm(msg, GetTitle(title)))
                {
                    var result = dia.ShowDialog();
                    input = dia.Result;
                    return (result == DialogResult.OK);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Try setting Util.Testmode to true! Original Message: " + e.Message);
            }
        }

        public static void ShowErrors(string headmsg, List<string> errors)
        {
            if (errors.Count > 0)
            {
                var builder = new StringBuilder();
                builder.AppendLine(headmsg);
                builder.AppendLine("");
                errors.ForEach(x => builder.AppendLine(x));
                ShowError(builder.ToString(), "");
            }
        }

        private static string GetTitle(string title)
        {
            var p = (title.StartsWith("autoelectric")) ? "" : "autoelectric - ";
            string pre = string.IsNullOrEmpty(title) ? "autoelectric" : p;
            return pre + title;
        }


        public static Boolean GetCriticalUserConfirmination(string text, string title, string yesText, string noText)
        {
            try
            {
                return AEMessageBox.Ask(text, GetTitle(title), yesText, noText, AEMessageBox.MessageSymbol.Warning);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Try setting Util.Testmode to true! Original Message: " + e.Message);
            }
        }

        public static Boolean GetCriticalUserConfirmination(string text, string title, string yesText, string noText, int dontAskAgainId)
        {
            if (DontShowMeAgainIds.Contains(dontAskAgainId))
            {
                return true;
            }
            else
            {
                try
                {
                    var ret = AEMessageBox.Ask(text, GetTitle(title), yesText, noText, AEMessageBox.MessageSymbol.Warning, out bool dontAskMeAgain);
                    if (dontAskMeAgain)
                    {
                        DontShowMeAgainIds.Add(dontAskAgainId);
                    }
                    return ret;
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Try setting Util.Testmode to true! Original Message: " + e.Message);
                }
            }

        }

        public static Boolean GetFatalUserConfirmination(string text, string title, string yesText, string noText)
        {
            try
            {
                return AEMessageBox.Ask(text, GetTitle(title), yesText, noText, AEMessageBox.MessageSymbol.Error);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Try setting Util.Testmode to true! Original Message: " + e.Message);
            }
        }

        public static Boolean GetUserConfirmination(string text, string title, string yesText, string noText)
        {
            try
            {
                return AEMessageBox.Ask(text, GetTitle(title), yesText, noText);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Try setting Util.Testmode to true! Original Message: " + e.Message);
            }
        }

        public static Boolean GetOnErrorUserConfirmination(string text, string title, string yesText, string noText)
        {
            try
            {
                return AEMessageBox.Ask(text, GetTitle(title), yesText, noText, AEMessageBox.MessageSymbol.Error);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Try setting Util.Testmode to true! Original Message: " + e.Message);
            }
        }
    }
}

