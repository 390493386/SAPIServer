using System.Drawing;
using System.Windows.Forms;

namespace SiweiSoft.SAPIServer
{
    public enum ItemType
    {
        Info,
        Warn,
        Error
    }

    public class SListBoxItem
    {
        public string Text { get; set; }

        public ItemType Type { get; set; }

        public SListBoxItem(string text, ItemType type)
        {
            Text = text;
            Type = type;
        }
    }

    public class SListBox : ListBox
    {
        public SListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Brush fontBrush = null;
            if (e.Index > -1)
            {
                SListBoxItem item = (SListBoxItem)Items[e.Index];
                if (item.Type == ItemType.Info)
                    fontBrush = Brushes.DarkGreen;
                else if (item.Type == ItemType.Warn)
                    fontBrush = Brushes.Goldenrod;
                else if (item.Type == ItemType.Error)
                    fontBrush = Brushes.Red;

                e.DrawBackground();
                e.Graphics.DrawString(item.Text, e.Font, fontBrush, e.Bounds);
                e.DrawFocusRectangle();
            }
            base.OnDrawItem(e);
        }
    }
}
