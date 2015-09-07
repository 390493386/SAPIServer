using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiweiSoft.SAPIServer
{
    public partial class ExitConfirm : Form
    {
        public ExitConfirm()
        {
            InitializeComponent();
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            if (backProcess.Checked)
            {
            }
            else if (exitProcess.Checked)
            {
            }
            this.Close();
        }
    }
}
