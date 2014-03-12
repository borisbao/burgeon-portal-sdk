using System;
using System.Collections.Generic;
using System.Text;

namespace Top.Api.Request
{
    public class BaseProcessOrderRequest : TopRequest
    {
        public BaseProcessOrderRequest()
        {
            command = "ProcessOrder";
        }


        private bool _IsAutoSubmit = false;
        /// <summary>
        /// 是否自动提交 boolean	
        /// 若为true，在最后执行提交动作，单据必须支持提交动作，等同于在界面上点击“提交”按钮
        /// </summary>
        public bool IsAutoSubmit
        {
            get { return _IsAutoSubmit; }
            set { _IsAutoSubmit = value; }
        }


        private int[] _DetailObjsRefTables;
        /// <summary>
        /// 元素reftableid为关联表id， 
        /// 从rest帮助的关联表位置获取。
        /// 界面上虽然有多个关联标签，
        /// 但并非所有标签对应的关联表都需要设置内容。
        /// </summary>
        public int[] DetailObjsRefTables
        {
            get { return _DetailObjsRefTables; }
            set { _DetailObjsRefTables = value; }
        }



    }

    /// <summary>
    /// ProcessOrder 命令中Request时 明细表记录的
    /// </summary>
    public class ProcessOrder_Detailobjs_Refobjs
    {
        public string table { get; set; }

        public  object addList { get; set; }
    }
    /// <summary>
    /// 明细表记录对象，包含多个标签页的内容，每个标签页的内容可以是单一对象，也可能是列表
    /// </summary>
    public class ProcessOrder_DetailObjs 
    {
        public int[] reftables { get; set; }

        private IList<ProcessOrder_Detailobjs_Refobjs> _refobjs=new List<ProcessOrder_Detailobjs_Refobjs>();
        
        public IList<ProcessOrder_Detailobjs_Refobjs> refobjs
        {
            get { return _refobjs; }
            set { _refobjs = value; }
        }
       
    }
}