using System;
using System.Collections.Generic;
using System.Text;

namespace Top.Api.Request
{
    public class BaseQueryRequest : TopRequest
    {
        public BaseQueryRequest()
        {
            command = "Query";
        }

        private string _table;

        public string Table
        {
            get { return _table; }
            set { _table = value; }
        }

        private string[] _columns;

        public string[] Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        private int _start = 0;

        public int Start
        {
            get { return _start; }
            set { _start = value; }
        }

        private int _range = 100;

        public int Range
        {
            get { return _range; }
            set { _range = value; }
        }

        private bool _count = false;

        public bool count
        {
            get { return _count; }
            set { _count = value; }
        }

        public PortalRestRequest GetParameters()
        {
            //TopDictionary mainparameters = new TopDictionary();
            //mainparameters.Add("id", this.Id);
            //mainparameters.Add("command", this.command);
            //mainparameters.Add("table", this.Table);
            PortalRestRequest request = new PortalRestRequest();
            request.id = this.Id;
            request.command = this.command;

            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("table", this.Table);
            if (Columns != null && Columns.Length > 0)
                parameters.Add("columns", this.Columns);
            if (Start != 0)
                parameters.Add("start", this.Start);
            if (Range != 100)
                parameters.Add("range", this.Range);
            if (count != false)
                parameters.Add("count", this.count);
            request.Params = parameters;
            return request;
            //mainparameters.Add("params", Jayrock.Json.Conversion.JsonConvert.ExportToString(paramDict));
            //todo:orderby,params参数
            //return mainparameters;
        }


    }
}
