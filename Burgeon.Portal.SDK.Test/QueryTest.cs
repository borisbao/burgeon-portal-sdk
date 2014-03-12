using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace Burgeon.Portal.SDK.Test
{
    public partial class QueryTest : Form
    {
        public string url = "";
        public QueryTest()
        {
            InitializeComponent();
            url = this.tb_url.Text.Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ITopClient client = new DefaultTopClient(url, this.tb_appkey.Text.Trim(), this.tb_appsecret.Text.Trim());
            C_PROVINCE_Query_Request req=new C_PROVINCE_Query_Request();
            req.Table = "15633";
            
            C_PROVINCE_Query_Response rep=client.Execute<C_PROVINCE_Query_Response>(req);

            this.richTextBox1.Text = rep.ReqUrl;
            this.richTextBox2.Text = rep.Body;
            if (!rep.IsError)
            {
                this.lbl_result.Text = "成功!";
            }
            else
            {
                this.lbl_result.Text = "失败!";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string str = System.Web.HttpUtility.UrlDecode(this.richTextBox1.Text.Trim());
            MessageBox.Show(str);
        }
    }
}
