using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Top.Api
{
    /// <summary>
    /// Rest接口的Transaction对象
    /// </summary>
    public class PortalRestRequest
    {
        private string _id;

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _command;
      
        public string command
        {
            get { return _command; }
            set { _command = value; }
        }
        [JsonProperty("params")]
        public IDictionary<string, object> Params { get; set; }
    }
}
