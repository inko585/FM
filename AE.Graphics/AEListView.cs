using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace AE.Graphics
{
    public class CheckAllEventArgs : EventArgs
    {
        public bool Checked { get; private set; }


        public CheckAllEventArgs(bool check)
        {
            this.Checked = check;
        }

    }

    public class AEListView : ListView
    {




        public AEListView()
        {
            Columns.Add("", -2, HorizontalAlignment.Center);


            OwnerDraw = true;
            GridLines = true;
            View = View.Details;
            HideSelection = true;
            FullRowSelect = true;
            ShowItemToolTips = true;
            CheckBoxes = true;


            DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListView1_DrawColumnHeader);
            ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView1_ColumnClick);
            DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.ListView1_DrawItem);
            DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListView1_DrawSubItem);
            ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_ColumnWidthChanging);
        }

        public delegate void CheckAllEventHandler(object sender, CheckAllEventArgs e);
        public event CheckAllEventHandler CheckAll;
        public delegate void AfterCheckAllEventHandler(object sender, CheckAllEventArgs e);
        public event AfterCheckAllEventHandler AfterCheckAll;

        protected void OnAllChecked(CheckAllEventArgs e)
        {
            CheckAll(this, e);
        }

        protected void AfterAllChecked(CheckAllEventArgs e)
        {
            AfterCheckAll(this, e);
        }

        public new void Clear()
        {

            base.Clear();

            if (CheckBoxes)
            {
                Columns.Add("", -2, HorizontalAlignment.Center);
            }
            else
            {
                Columns.Add("", 0, HorizontalAlignment.Center);
            }
        }

        private readonly object sync = new object();
        /// <summary>
        /// Converts object arrays to ListViewItems<para/>
        /// First object of each row array is used as id of the ListViewItem, the remaining values are used as SubItems
        /// </summary>
        /// <param name="obs"></param>
        public void Fill(IEnumerable<object[]> obs)
        {
            lock (sync)
            {
                Items.Clear();
                var items = new List<ListViewItem>();
                foreach (var o in obs)
                {
                    var item = new ListViewItem
                    {
                        Name = o[0].ToString()
                    };
                    for (int i = 1; i < o.Length; i++)

                    {
                        item.SubItems.Add(o[i].ToString());
                    }
                    if (filter(item))
                    {
                        items.Add(item);
                    }
                }
                Items.AddRange(items.ToArray());
            }

        }

        public void ScrollToBottom()
        {
            Items[Items.Count - 1].EnsureVisible();
        }

        private Func<ListViewItem, bool> filter = x => true;

        public void SetFilter(Func<ListViewItem, bool> condition)
        {
            filter = condition;
        }

        internal List<int> NumericalColumns = new List<int>();


        private bool clicked = false;

        private CheckBoxState state;

        private void ListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {

            if (e.ColumnIndex == 0)
            {
                e.Cancel = true;
                e.NewWidth = Columns[0].Width;
            }
        }


        private void ListView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if ((e.ColumnIndex == 0 && base.CheckBoxes))
            {

                var loc = new Point(ClientRectangle.Location.X + 4, ClientRectangle.Location.Y + 2);

                e.DrawBackground();
                CheckBoxRenderer.DrawCheckBox(e.Graphics, loc, state);

            }
            else
            {
                e.DrawDefault = true;
            }
        }

        public void AutoResizeColumns()
        {
            foreach (ColumnHeader column in Columns)
            {
                if (column.Index != 0 || CheckBoxes)
                {
                    column.Width = -2;

                }

            }
        }


        public new bool CheckBoxes
        {
            get
            {
                return base.CheckBoxes;
            }
            set
            {
                base.CheckBoxes = value;
                if (value)
                {
                    Columns[0].Width = -2;
                }
                else
                {
                    Columns[0].Width = 0;
                }



            }
        }
        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            if (e.Column == 0 && base.CheckBoxes)
            {
                if (!clicked)
                {
                    clicked = true;
                    state = CheckBoxState.CheckedPressed;
                    var caa = new CheckAllEventArgs(true);
                    OnAllChecked(caa);
                    foreach (ListViewItem item in Items)
                    {
                        item.Checked = true;
                    }

                    Invalidate();
                    AfterAllChecked(caa);

                }
                else
                {
                    clicked = false;
                    state = CheckBoxState.UncheckedNormal;

                    var caa = new CheckAllEventArgs(true);
                    OnAllChecked(caa);
                    foreach (ListViewItem item in Items)
                    {
                        item.Checked = false;
                    }

                    Invalidate();
                    AfterAllChecked(caa);
                }
            }
            else
            {
                if (Columns[e.Column].DateFormat() == null)
                {
                    ListViewItemSorter = new AEListViewItemComparer(e.Column, Sorting, Columns[e.Column].IsNumerical());
                }
                else
                {
                    ListViewItemSorter = new AEListViewItemComparer(e.Column, Sorting, Columns[e.Column].DateFormat());
                }
                Sorting = (Sorting == SortOrder.Descending) ? SortOrder.Ascending : SortOrder.Descending;
            }
        }

        private void ListView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void ListView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }
    }

    public class AEListViewItemComparer : IComparer
    {
        private readonly int col;
        private readonly SortOrder order;
        private readonly bool numerical;
        private readonly string dateFormat;

        public AEListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
            numerical = false;
            dateFormat = null;

        }
        public AEListViewItemComparer(int column, SortOrder order, bool numerical)
        {
            col = column;
            this.order = order;
            this.numerical = numerical;
        }

        public AEListViewItemComparer(int column, SortOrder order, string dateFormat)
        {
            col = column;
            this.order = order;
            this.numerical = false;
            this.dateFormat = dateFormat;
        }

        public int Compare(object x, object y)
        {
            int returnVal;
            if (numerical)
            {
                var t1 = ((ListViewItem)x).SubItems[col].Text;
                var t2 = ((ListViewItem)y).SubItems[col].Text;
                var d1 = t1 == "" ? 0 : Decimal.Parse(t1);
                var d2 = t2 == "" ? 0 : Decimal.Parse(t2);
                returnVal = Decimal.Compare(d1, d2);
            }
            else
            {
                if (dateFormat != null)
                {
                    returnVal = DateTime.Compare(DateTime.ParseExact(((ListViewItem)x).SubItems[col].Text, dateFormat, CultureInfo.InvariantCulture), DateTime.ParseExact(((ListViewItem)y).SubItems[col].Text, dateFormat, CultureInfo.InvariantCulture));
                }
                else
                {
                    returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                            ((ListViewItem)y).SubItems[col].Text);
                }
            }
            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }

    }

    public static class RichColumnHeaderCollection
    {
        private static readonly Dictionary<ColumnHeader, bool> isNumerical = new Dictionary<ColumnHeader, bool>();
        private static readonly Dictionary<ColumnHeader, string> dateFormats = new Dictionary<ColumnHeader, string>();

        public static ColumnHeader Add(this ListView.ColumnHeaderCollection chc, string text, int width, HorizontalAlignment textAlign, string dateFormat)
        {
            var ch = chc.Add(text, width, textAlign);
            dateFormats[ch] = dateFormat;
            return ch;

        }
        public static ColumnHeader Add(this ListView.ColumnHeaderCollection chc, string text, int width, HorizontalAlignment textAlign, bool numerical)
        {
            var ch = chc.Add(text, width, textAlign);
            isNumerical[ch] = numerical;
            return ch;
        }

        public static bool IsNumerical(this ColumnHeader ch)
        {
            if (isNumerical.TryGetValue(ch, out bool numerical))
            {
                return numerical;
            }
            else
            {
                return false;
            }
        }

        public static string DateFormat(this ColumnHeader ch)
        {
            if (dateFormats.TryGetValue(ch, out string dateFormat))
            {
                return dateFormat;
            }
            else
            {
                return null;
            }
        }



        public static void SetNumerical(this ColumnHeader ch, bool numerical)
        {
            isNumerical[ch] = numerical;
        }
    }
}
