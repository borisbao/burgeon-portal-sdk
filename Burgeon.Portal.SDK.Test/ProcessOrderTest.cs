using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Top.Api;
using Top.Api.Domain;
using Top.Api.Request;
using Top.Api.Response;

namespace Burgeon.Portal.SDK.Test
{
    public partial class ProcessOrderTest : Form
    {
        public string url = "";
        public ProcessOrderTest()
        {
            InitializeComponent();
            url = this.tb_url.Text.Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ITopClient client = new DefaultTopClient(url, this.tb_appkey.Text.Trim(), this.tb_appsecret.Text.Trim());
            //实例化PortalRest请求
            IES_VIP_SALE_ProcessOrder_Request req = new IES_VIP_SALE_ProcessOrder_Request();
        
            req.IsAutoSubmit = true;
            //头表
            req.IES_Vip_Sale = BuildIESVipSaleObject();
            //明细表
            req.IES_Vip_SaleItem = BuildIESVipSaleItem(req.IES_Vip_Sale.EXTORDERID);
            //关联表
            req.DetailObjsRefTables = new int[] { 4275 };
            

            IES_VIP_SALE_ProcessOrder_Response rep = client.Execute<IES_VIP_SALE_ProcessOrder_Response>(req);


            this.richTextBox1.Text = rep.ReqUrl;
            this.richTextBox2.Text = rep.Body;
            if (!rep.IsError)
            {
                this.lbl_result.Text = "成功!返回单号:"+rep.ObjectId;
            }
            else
            {
                this.lbl_result.Text = "失败!";
            }

        }

        public IES_VIP_SALE BuildIESVipSaleObject()
        {
            IES_VIP_SALE sale = new IES_VIP_SALE();
            sale.EXTORDERID = "Wing-"+DateTime.Now.ToString("yyyyMMddHHmmss000");
            sale.ORDERDATETIME = DateTime.Now;
            sale.RECEIVEADDRESS = "上海";
            sale.RECEIVECODE = "TEST0222";
            sale.RECEIVENAME = "TEST0222";
            sale.SENDCODE = "234";
            sale.SENDNAME = "234";
            sale.table = "22633";//主表的表ID或表名
            sale.id = "-1";
            return sale;
        }

        public ProcessOrder_Detailobjs_Refobjs BuildIESVipSaleItem(string EXTORDERID)
        {
            IList<IES_VIP_SALEITEM> iesVipSaleItemList = new List<IES_VIP_SALEITEM>();
            
            
            IES_VIP_SALEITEM item1 = new IES_VIP_SALEITEM();
            item1.SKU = "109434C535552";
            item1.QTY = 101;
            item1.EXTORDERID = EXTORDERID;
            iesVipSaleItemList.Add(item1);

            IES_VIP_SALEITEM item2 = new IES_VIP_SALEITEM();
            item2.SKU = "109434C535555";
            item2.QTY = 102;
            item2.EXTORDERID = EXTORDERID;
            iesVipSaleItemList.Add(item2);

            

            ProcessOrder_Detailobjs_Refobjs refobj = new ProcessOrder_Detailobjs_Refobjs();
            refobj.table = "22637";
            refobj.addList = iesVipSaleItemList;
            return refobj;
            
            //refobj.table = "";
            //refobj.addList=
            

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text.Trim()))
            {
                string str = System.Web.HttpUtility.UrlDecode(this.richTextBox1.Text.Trim());
                MessageBox.Show(str);
            }
            else
            {
                MessageBox.Show("此按钮是对右边文本框字符进行解码,为空时不能处理");
            }

        }
    }
}
