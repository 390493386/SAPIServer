using System;
using System.Windows.Forms;

namespace SiweiSoft.SAPIServer
{
    public partial class LBItemDetail : Form
    {
        public LBItemDetail(string logDetail)
        {
            InitializeComponent();

            ldRichTextBox.Text = logDetail;
        }

        private void CloseDetails_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CopyLog_Click(object sender, EventArgs e)
        {
            CopyLog.Enabled = false;
            Clipboard.SetDataObject(ldRichTextBox.Text);
            ldRichTextBox.Text += "\n已复制到粘贴板！";
        }
    }
}
