using System;
using System.Collections.Generic;
using System.Text;

namespace Top.Api
{
    public class TopRequest
    {
        private string _id = "112";

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 可选值:"ObjectCreate"|"ObjectModify"|"ObjectDelete"|"ObjectSubmit"|"ExecuteWebAction"|"ProcessOrder"|"Query"|"Import"
        /// </summary>
        public string command { get; set; }

        protected string Params { get; set; }
    }
}
