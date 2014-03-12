using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;


namespace Top.Api.Parser
{
    /// <summary>
    /// TOP JSON响应通用解释器。
    /// </summary>
    public class TopJsonParser : ITopParser
    {
        private static readonly Dictionary<string, Dictionary<string, TopAttribute>> attrs = new Dictionary<string, Dictionary<string, TopAttribute>>();

   

        public T Parse<T>(string body) where T : TopResponse
        {
            //return (T)JsonConvert.Import(typeof(T), body);

            return (T)Newtonsoft.Json.JsonConvert.DeserializeObject<T>(body);
        }

    }
}
