using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Top.Api.Parser;
using Top.Api.Request;
using Top.Api.Util;

namespace Top.Api
{
    /// <summary>
    /// 基于REST的TOP客户端。
    /// </summary>
    public class DefaultTopClient : ITopClient
    {
        public const string APP_KEY = "sip_appkey";
        public const string METHOD = "method";
        public const string TIMESTAMP = "sip_timestamp";
        public const string SIGN = "sip_sign";
        public const string PARTNER_ID = "partner_id";
        public const string PORTALTRANSACTION = "transactions";

        private string serverUrl;
        private string appKey;
        private string appSecret;

        private WebUtils webUtils;
        private ITopLogger topLogger;
        private bool disableParser = false; // 禁用响应结果解释
        private bool disableTrace = false; // 禁用日志调试功能
        private IDictionary<string, string> systemParameters; // 设置所有请求共享的系统级参数

        #region DefaultTopClient Constructors

        public DefaultTopClient(string serverUrl, string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverUrl = serverUrl;
            this.webUtils = new WebUtils();
            this.topLogger = new DefaultTopLogger();
        }

        #endregion


        public void SetTopLogger(ITopLogger topLogger)
        {
            this.topLogger = topLogger;
        }

        public void SetTimeout(int timeout)
        {
            this.webUtils.Timeout = timeout;
        }

        public void SetDisableParser(bool disableParser)
        {
            this.disableParser = disableParser;
        }

        public void SetDisableTrace(bool disableTrace)
        {
            this.disableTrace = disableTrace;
        }

        public void SetSystemParameters(IDictionary<string, string> systemParameters)
        {
            this.systemParameters = systemParameters;
        }

        #region ITopClient Members

        public T Execute<T>(ITopRequest<T> request) where T : TopResponse
        {
            return Execute<T>(request, null);
        }

        public T Execute<T>(ITopRequest<T> request, string session) where T : TopResponse
        {
            return Execute<T>(request, session, DateTime.Now);
        }

        public T Execute<T>(ITopRequest<T> request, string session, DateTime timestamp) where T : TopResponse
        {
            if (disableTrace)
            {
                return DoExecute<T>(request, session, timestamp);
            }
            else
            {
                try
                {
                    return DoExecute<T>(request, session, timestamp);
                }
                catch (Exception e)
                {
                    topLogger.Error(this.serverUrl + "\r\n" + e.StackTrace);
                    throw e;
                }
            }
        }

        #endregion

        private T DoExecute<T>(ITopRequest<T> request, string session, DateTime timestamp) where T : TopResponse
        {
            // 提前检查业务参数
           
            try
            {
                request.Validate();
            }
            catch (TopException e)
            {
                return createErrorResponse<T>(e.ErrorCode, e.ErrorMsg);
            }
            string sip_timestamp = timestamp.ToString("yyyy-MM-dd HH:mm:ss.000");
            // 添加协议级请求参数
            TopDictionary txtParams = new TopDictionary();
            PortalRestRequest portalRestRequest= request.GetParameters();
            //Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            //Jayrock.Json.Conversion.JsonConvert.Export(paramDict, writer);
            //txtParams.Add(PORTALTRANSACTION, "[" + Jayrock.Json.Conversion.JsonConvert.ExportToString(portalRestRequest) + "]");
            txtParams.Add(PORTALTRANSACTION, "[" + Newtonsoft.Json.JsonConvert.SerializeObject(portalRestRequest) + "]");
            txtParams.Add(APP_KEY, appKey);        
            txtParams.Add(TIMESTAMP, sip_timestamp);
            txtParams.AddAll(this.systemParameters);

            // 添加签名参数
            txtParams.Add(SIGN, TopUtils.SignBurgeonPortalRequest(appKey,appSecret,sip_timestamp));

            // 是否需要上传文件
            string body;
            if (request is ITopUploadRequest<T>)
            {
                ITopUploadRequest<T> uRequest = (ITopUploadRequest<T>)request;
                IDictionary<string, FileItem> fileParams = TopUtils.CleanupDictionary(uRequest.GetFileParameters());
                body = webUtils.DoPost(this.serverUrl, txtParams, fileParams);
            }
            else
            {
                body = webUtils.DoPost(this.serverUrl, txtParams);
            }

            // 解释响应结果
            T rsp;
            if (disableParser)
            {
                rsp = Activator.CreateInstance<T>();
                rsp.Body = body;
            }
            else
            {
                ITopParser tp = new TopJsonParser();
                //去掉body[]
                body = body.Substring(1, body.LastIndexOf(']')-1);

                rsp = tp.Parse<T>(body);

            }

            // 追踪错误的请求
            if (!disableTrace)
            {
                rsp.Body = body;
                rsp.ReqUrl = webUtils.BuildGetUrl(this.serverUrl, txtParams);
                if (rsp.IsError)
                {
                    topLogger.Warn(rsp.ReqUrl + "\r\n" + rsp.Body);
                }
            }

            return rsp;
        }

        private T createErrorResponse<T>(string errCode, string errMsg) where T : TopResponse
        {
            T rsp = Activator.CreateInstance<T>();
            rsp.ErrCode = errCode;
            rsp.ErrMsg = errMsg;


            IDictionary<string, object> errObj = new Dictionary<string, object>();
            errObj.Add("code", errCode);
            errObj.Add("msg", errMsg);
            IDictionary<string, object> root = new Dictionary<string, object>();
            root.Add("error_response", errObj);

            string body =Newtonsoft.Json.JsonConvert.SerializeObject(root);
            rsp.Body = body;

            return rsp;
        }
    }
}
