using System;
using System.Collections.Generic;
using System.Text;
using Top.Api.Domain;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    public class IES_VIP_SALE_ProcessOrder_Request : BaseProcessOrderRequest,ITopRequest<IES_VIP_SALE_ProcessOrder_Response>
    {

        #region 属性
        private IES_VIP_SALE _IES_Vip_Sale;
        /// <summary>
        /// IES唯品会出库单 头表
        /// </summary>
        public IES_VIP_SALE IES_Vip_Sale
        {
            get { return _IES_Vip_Sale; }
            set { _IES_Vip_Sale = value; }
        }


        private ProcessOrder_Detailobjs_Refobjs _IES_Vip_SaleItem;
        /// <summary>
        /// detailobjs 属性中 refobjs 元素（1:m）
        /// </summary>
        public ProcessOrder_Detailobjs_Refobjs IES_Vip_SaleItem
        {
            get { return _IES_Vip_SaleItem; }
            set { _IES_Vip_SaleItem = value; }
        }

       
     

        #endregion

        public void Validate()
        {

            RequestValidator.ValidateRequired("masterobj", this.IES_Vip_Sale);
            RequestValidator.ValidateRequired("DetailObjsRefTables", this.DetailObjsRefTables);
            
        }

   
        //此方法来构造Rest请求时需要的Transaction属性
        //而对于Rest开发来说,最重要的也是处理Transaction的内容
        //processOrder方法对于其他Command来说,对象比较复杂
        public PortalRestRequest GetParameters()
        {
            //构造ProcessOrder对象中的detailobjs
            ProcessOrder_DetailObjs detailobjs = new ProcessOrder_DetailObjs();
            detailobjs.reftables = this.DetailObjsRefTables;
            detailobjs.refobjs.Add(IES_Vip_SaleItem);

            //构造Transaction的param元素
            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("masterobj", this.IES_Vip_Sale);
            parameters.Add("submit", this.IsAutoSubmit);
            parameters.Add("detailobjs", detailobjs);//

            //Transaction对象
            PortalRestRequest request = new PortalRestRequest();
            request.id = this.Id;
            request.command = this.command;
            request.Params = parameters;
            return request;
        }
    }
}
