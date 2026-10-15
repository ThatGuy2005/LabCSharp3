using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabCSharp2
{
    public partial class AddKeyword : Form
    {
        public AddKeyword()
        {
            InitializeComponent();
        }
        public string Keyword => keywordBox.Text.Trim();
        private void add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Keyword))
            {
                MessageBox.Show("Introduceți un keyword!");
                return;                         // form stays open
            }
            DialogResult = DialogResult.OK;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Hide();
        }
    }
}
