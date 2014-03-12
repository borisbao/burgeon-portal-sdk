using System;
using System.Collections.Generic;
using System.Text;

namespace Top.Api.Domain
{
    /// <summary>
    /// IES唯品会出库单
    /// 新增时Id=-1
    /// </summary>
    public class IES_VIP_SALE : TopObject
    {
        /// <summary>
        /// IES出库单号
        /// </summary>
        public string EXTORDERID { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime ORDERDATETIME { get; set; }

        /// <summary>
        /// 发货店仓
        /// </summary>
        public string SENDNAME { get; set; }

        /// <summary>
        /// 发货店仓code
        /// </summary>
        public string SENDCODE { get; set; }

        /// <summary>
        /// 收货店仓
        /// </summary>
        public string RECEIVENAME { get; set; }

        /// <summary>
        /// 收货店仓code
        /// </summary>
        public string RECEIVECODE { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string RECEIVEADDRESS { get; set; }

        /// <summary>
        /// 头表id 或名称，或快捷码
        /// </summary>
        public string table { get; set; }

    
    }
}
