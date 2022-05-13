using System;
using System.Collections;
using System.Windows.Forms;

namespace Файловый_менеджер
{
    internal class ColumnSorter :IComparer
    {
        private int currentColumn;
        private SortOrder sortOrder;

        public int SortedColumn
        {
            get { return currentColumn; }
            set { currentColumn = value; }
        }

        public SortOrder Order
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        private CaseInsensitiveComparer objectCompare;
        public ColumnSorter()
        {
            currentColumn = 0;
            sortOrder = SortOrder.None;
            objectCompare = new CaseInsensitiveComparer();
        }

        public int Compare(object x, object y)
        {
            int result;
            ListViewItem itemX = (ListViewItem)x, itemY = (ListViewItem)y;
            result = objectCompare.Compare(itemX.SubItems[currentColumn].Text,
                itemY.SubItems[currentColumn].Text);

            if (sortOrder == SortOrder.Ascending) return result;
            //if (sortOrder == SortOrder.Descending) return -result;
            return 0;
        }
    }
}
