using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Burgeon.Portal.SDK.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QueryTest queryTest = new QueryTest();
            queryTest.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files\Internet Explorer\iexplore.exe", "http://g.burgeon.cn:90/html/nds/schema/resthome.jsp");

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files\Internet Explorer\iexplore.exe", "http://g.burgeon.cn:90/html/nds/schema/testrest.jsp");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProcessOrderTest test1 = new ProcessOrderTest();
            test1.ShowDialog();
        }
    }
}
