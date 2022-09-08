using System;
using System.Collections.Generic;
using System.Linq;
using AE.Graphics.Wpf.View;
using System.Windows;
using AE.Graphics.Wpf.ViewModel;
using System.Threading;
using System.Globalization;

namespace AE.Graphics.Wpf
{
    public sealed class AEGraphics
    {
        public static readonly WindowStartupLocation DialogStartPosition = WindowStartupLocation.CenterScreen;
        private static readonly List<int> DontShowMeAgainIds = new List<int>();

        private AEGraphics()
        {
        }

        public static void FromSTA(Action action)
        {          
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA || !Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(action);

            }
            else
            {
                action();
            }
        }

        [STAThreadAttribute]
        public static void ShowMessage(string message, string title = "", MessageBoxIcon icon = MessageBoxIcon.Information, int timeOutLength = -1, Action onTimeOut = null, bool showAsDialog = true, int dontShowMeAgainId = -1)
        {
            if (DontShowMeAgainIds.Contains(dontShowMeAgainId))
            {
                return;
            }
            FromSTA(() =>
            {
                var current = Application.Current.Windows.OfType<AEWindow>().SingleOrDefault(x => x.IsActive);
                var timeOutActive = false;
                if (current != null)
                {
                    timeOutActive = current.TimeOutActive;
                    current.TimeOutActive = false;
                }

                using (AEMessageBoxView aeMessageBoxView = new AEMessageBoxView(message, title, icon, dontShowMeAgainId == -1 ? MessageBoxMode.Message : MessageBoxMode.DontShowMeAgainMessage, timeOutLength: timeOutLength, onTimeOut: onTimeOut))
                {
                    ShowAEWindow(aeMessageBoxView, showAsDialog);
                    if (showAsDialog &&  aeMessageBoxView.ViewModel.DontShowMeAgain)
                    {
                        DontShowMeAgainIds.Add(dontShowMeAgainId);
                    }
                }

                if (current != null)
                {
                    current.TimeOutActive = timeOutActive;
                }

            });
        }

        [STAThreadAttribute]
        public static bool? ShowWaitView(string title, string message, int waitSeconds)
        {
            bool? dialogResult = null;

            FromSTA(() =>
            {
                using (AEWaitView aeWaitView = new AEWaitView(GetTitle(title), message, waitSeconds))
                {
                    dialogResult = ShowAEWindow(aeWaitView);
                }
            });

            return dialogResult;
        }

        [STAThreadAttribute]
        public static bool ShowQuestion(string message, string title, string yesButtonText, string noButtonText, MessageBoxIcon icon = MessageBoxIcon.Question, Window owner = null, int timeOutLength = -1, Action onTimeOut = null, int dontShowMeAgainId = -1)
        {
            if (DontShowMeAgainIds.Contains(dontShowMeAgainId))
            {
                return true;
            }
            var ret = false;
            FromSTA(() =>
            {
                using (AEMessageBoxView aeMessageBoxView = new AEMessageBoxView(message, title, icon, dontShowMeAgainId == -1 ? MessageBoxMode.Ask : MessageBoxMode.DontShowMeAgainAsk, yesButtonText, noButtonText, timeOutLength: timeOutLength, onTimeOut: onTimeOut))
                {
                    ShowAEWindow(aeMessageBoxView);
                    ret = aeMessageBoxView.DialogResult ?? false;
                    if (aeMessageBoxView.ViewModel.DontShowMeAgain)
                    {
                        DontShowMeAgainIds.Add(dontShowMeAgainId);
                    }
                }
            });

            return ret;
        }

        public static void ShowUrlLinkMessageBox(string title, string message, string link)
        {
            ShowLinkMessageBox(title, message, link, link, LinkType.Url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="linkText"></param>
        /// <param name="urlLink"></param>
        public static void ShowUrlLinkMessageBox(string title, string message, string linkText, string urlLink)
        {
            ShowLinkMessageBox(title, message, linkText, urlLink, LinkType.Url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="linkText"></param>
        /// <param name="link"></param>
        /// <param name="linkType"></param>
        private static void ShowLinkMessageBox(string title, string message, string linkText, string link, LinkType linkType)
        {
            using (AELinkView aeLinkView = new AELinkView(title, message, linkText, link, linkType))
            {
                ShowAEWindow(aeLinkView);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        public static void ShowError(string msg, string title, int timeOutLength = -1, Action onTimeOut = null, bool showAsDialog = true, int dontShowMeAgainId = -1)
        {
            try
            {

                ShowMessage(msg, title: title, icon: MessageBoxIcon.Error, timeOutLength: timeOutLength, onTimeOut: onTimeOut, showAsDialog: showAsDialog, dontShowMeAgainId: dontShowMeAgainId);

            }
            catch (Exception exp)
            {
                throw new InvalidOperationException(exp.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        public static void ShowWarning(string msg, string title, int timeOutLength = -1, Action onTimeOut = null, bool showAsDialog = true, int dontShowMeAgainId = -1)
        {
            try
            {
                ShowMessage(msg, title: GetTitle(title), icon: MessageBoxIcon.Warning, timeOutLength: timeOutLength, onTimeOut: onTimeOut, showAsDialog: showAsDialog, dontShowMeAgainId: dontShowMeAgainId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="title"></param>
        /// <param name="selectionTitle"></param>
        /// <param name="selectedItem"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool GetUserItemSelection<T>(string title, string selectionTitle, out T selectedItem, Dictionary<string, T> items)
        {
            selectedItem = AESelectionView.GetUserItemSelection<T>(title, selectionTitle, items);

            if (selectedItem != null)
            { return true; }
            else
            {
                selectedItem = default;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="title"></param>
        /// <param name="selectionTitle"></param>
        /// <param name="selectedItem"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool GetUserItemMultiSelection<T>(string title, string selectionTitle, string selectAllText, out List<T> selectedItems, Dictionary<string, T> items)
        {
            selectedItems = AEMultiSelectionView.GetUserItemMultiSelection<T>(title, selectionTitle, selectAllText, items);

            if (selectedItems != null)
            { return true; }
            else
            {
                selectedItems = default;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="selectionTitle"></param>
        /// <param name="defaultPath"></param>
        /// <param name="filePath"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool GetUserFileSelection(string title, string selectionTitle, string defaultPath, out string filePath, string filter, string reloadCacheId = null, string reloadCacheToolTip = "")
        {
            filePath = string.Empty;

            using (var dia = new AEFileSelectionView(title, selectionTitle, filter, reloadCacheId, reloadCacheToolTip))
            {
                dia.ViewModel.FilePath = defaultPath;
                ShowAEWindow(dia);

                if (dia.ViewModel.DialogResult)
                {
                    filePath = dia.ViewModel.FilePath;
                    return true;
                }
                else
                {
                    filePath = "";
                    return false;
                }
            }
        }

        public static bool GetUser2FileSelection(string title, string selectionTitle, string firstFileNameLabel, string secondFileNameLabel, string defaultPathFirst, string defaultPathSecond, out string filePathFirst, out string filePathSecond, string filter, string reloadCacheId = null, string reloadCacheToolTipp = "")
        {
            using (var dia = new AE2FileSelectionView(title, selectionTitle, firstFileNameLabel, secondFileNameLabel, filter, reloadCacheId, reloadCacheToolTipp))
            {
                dia.ViewModel.FilePathFirst = defaultPathFirst;
                dia.ViewModel.FilePathSecond = defaultPathSecond;
                ShowAEWindow(dia);

                if (dia.DialogResult == true)
                {
                    filePathFirst = dia.ViewModel.FilePathFirst;
                    filePathSecond = dia.ViewModel.FilePathSecond;
                    return true;
                }
                else
                {
                    filePathFirst = "";
                    filePathSecond = "";
                    return false;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="yesText"></param>
        /// <param name="noText"></param>
        /// <returns></returns>
        public static bool GetCriticalUserConfirmination(string msg, string title, string yesText, string noText, int timeOutLength = -1, Action onTimeOut = null, int dontShowMeAgainId = -1)
        {
            try
            {
                return ShowQuestion(msg, GetTitle(title), yesText, noText, MessageBoxIcon.Warning, null, timeOutLength: timeOutLength, onTimeOut: onTimeOut, dontShowMeAgainId: dontShowMeAgainId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="yesText"></param>
        /// <param name="noText"></param>
        /// <returns></returns>
        public static bool GetUserConfirmination(string msg, string title, string yesText, string noText, int timeOutLength = -1, Action onTimeOut = null, int dontShowMeAgainId = -1 )
        {
            try
            {
                return ShowQuestion(msg, GetTitle(title), yesText, noText, owner: null, timeOutLength: timeOutLength, onTimeOut: onTimeOut, dontShowMeAgainId: dontShowMeAgainId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="yesButtonText"></param>
        /// <param name="noButtonText"></param>
        /// <returns></returns>
        public static bool GetFatalUserConfirmination(string msg, string title, string yesButtonText, string noButtonText, int timeOutLength = -1, Action onTimeOut = null, int dontShowMeAgainId = -1)
        {
            try
            {
                return ShowQuestion(msg, GetTitle(title), yesButtonText, noButtonText, MessageBoxIcon.Error, timeOutLength: timeOutLength, onTimeOut: onTimeOut, dontShowMeAgainId: dontShowMeAgainId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="yesText"></param>
        /// <param name="noText"></param>
        /// <returns></returns>
        public static bool GetOnErrorUserConfirmination(string msg, string title, string yesText, string noText)
        {
            try
            {
                return ShowQuestion(msg, GetTitle(title), yesText, noText, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool GetUserInput(string msg, string title, out string input, string defaultInput = "")
        {
            using (var dia = new AEInputView(title, msg, ViewModel.InputType.Text))
            {
                dia.ViewModel.Input = defaultInput;

                ShowAEWindow(dia);

                if (dia.ViewModel.DialogResult)
                {
                    input = dia.ViewModel.Input.ToString();
                    return true;
                }
                else
                {
                    input = string.Empty;
                    return false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool GetUserInputNumeric(string msg, string title, out decimal input)
        {
            using (var dia = new AEInputView(title, msg, ViewModel.InputType.Number))
            {
                ShowAEWindow(dia);

                if (dia.ViewModel.DialogResult)
                {
                    input = Decimal.Parse(dia.ViewModel.Input, CultureInfo.InvariantCulture);
                    return true;
                }
                else
                {
                    input = 0;
                    return false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="result"></param>
        /// <param name="labels"></param>
        /// <returns></returns>
        public static bool GetMultipleUserInput(string msg, string title, out Dictionary<string, string> result, params string[] labels)
        {
            using (var dia = new AEMultipleInputView(title, msg, labels))
            {
                ShowAEWindow(dia);

                if (dia.DialogResult == true)
                {
                    result = dia.Result;
                    return true;
                }
                else
                {
                    result = new Dictionary<string, string>();
                    return false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="addedItems"></param>
        /// <param name="changedItems"></param>
        /// <param name="upToDateItems"></param>
        public static void ShowResults(string title, List<string> addedItems = null, List<string> changedItems = null, List<string> upToDateItems = null)
        {
            using (var dia = new AEResultView(title, addedItems, changedItems, upToDateItems))
            {
                ShowAEWindow(dia);
            }
        }

        [STAThreadAttribute]
        private static bool? ShowAEWindow(AEWindow window, bool showAsDialog = true)
        {
            window.WindowStartupLocation = DialogStartPosition;

            if (showAsDialog)
            {
                return window.ShowDialog();
            }
            else
            {
                window.Show();
            }

            return null;
        }

        public static void ShowNotificationPopUp(AENotificationView.NotificationType nt, string message)
        {
            new AENotificationView(nt, message).Show();
        }
        public static void ShowNotificationPopUp(Window owner, AENotificationView.NotificationType nt, string message, int freeSpaceRight = 0)
        {
            new AENotificationView(owner, nt, message, freeSpaceRight).Show();
        }

        private static string GetTitle(string title)
        {
            var p = (title.StartsWith("autoelectric")) ? "" : "autoelectric - ";
            string pre = string.IsNullOrEmpty(title) ? "autoelectric" : p;
            return pre + title;
        }
    }

    public static class DontShowAgaingIDs
    {
        public readonly static int RunReportInVestigo = 1;
    }
}