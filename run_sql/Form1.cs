using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace run_sql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                fp_tb.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are absolutely sure?", "What!", MessageBoxButtons.OKCancel) == DialogResult.OK){
                runner r = new runner(db_tb.Text, dn_tb.Text, fp_tb.Text, fb_tb);

                await r.run();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fp_tb.Text == "" || db_tb.Text == "" || dn_tb.Text == "") button2.Enabled = false;
            else button2.Enabled = true;
        }
    }
}
