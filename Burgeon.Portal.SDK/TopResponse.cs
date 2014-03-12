using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace Top.Api
{
    [Serializable]
    public abstract class TopResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        //[XmlElement("code")]
        [JsonProperty("code")]
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
  
        [JsonProperty("message")]
        public string ErrMsg { get; set; }

      
        /// <summary>
        /// 响应原始内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// HTTP GET请求的URL
        /// </summary>
        public string ReqUrl { get; set; }

        /// <summary>
        /// 响应结果是否错误
        /// </summary>
        public bool IsError
        {
            get
            {
                return !string.IsNullOrEmpty(this.ErrCode) && this.ErrCode!="0";
            }
        }
    }
}
