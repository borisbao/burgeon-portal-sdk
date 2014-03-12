using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    public class C_PROVINCE_Query_Response : TopResponse
    {
        [XmlElement("count")]
        public int Count { get; set; }

        private IList<string[]> _Rows;
        [XmlElement("rows")]
        public IList<string[]> Rows
        {
            get { return _Rows; }
            set { _Rows = value; }
        }
    }
}
