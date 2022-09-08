using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.Timers;

namespace AE.Graphics
{
    public delegate void PasteClickEventHandler(Object sender, PasteEventArgs args);
    public class ToolStripClipBoard : ToolStripButton
    {
        public ToolStripClipBoard() : base()
        {
            Image = Properties.Resources.clipboard;
            ImageTransparentColor = System.Drawing.Color.Magenta;
            Click += new System.EventHandler(this.OnClick);
            Text = "Paste";
        }

        public event PasteClickEventHandler PasteClick;       

        protected void OnClick(object sender, EventArgs e)
        {
            var args = new PasteEventArgs()
            {
                ClipBoardGrid = GetClipBoardGrid(),
                ClipBoardText = GetClipBoardText()
            };

            if (PasteClick != null)
            {
                PasteClick.Invoke(this, args);
            }
        }

        protected string[][] GetClipBoardGrid()
        {
            var text = GetClipBoardText();
            string[] pastedRows = Regex.Split(text.TrimEnd("\r\n".ToCharArray()), "\r\n");
            string[][] pastedGrid = new string[pastedRows.Count()][];
            var i = 0;
            foreach (var pastedRow in pastedRows)
            {
                var pastedRowCells = pastedRow.Split(new char[] { '\t' });
                pastedGrid[i++] = pastedRowCells;
            }
            return pastedGrid;

        }


        protected string GetClipBoardText()
        {
            var res = "";
            Thread staThread = new Thread(delegate()
            {
                try
                {
                    res = Clipboard.GetText();
                }
                catch (Exception)
                {
                    res = "";
                }
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return res;
        }
    }

    public class PasteEventArgs : EventArgs
    {
        public string[][] ClipBoardGrid { get; set; }
        public string ClipBoardText { get; set; }
    }
}
