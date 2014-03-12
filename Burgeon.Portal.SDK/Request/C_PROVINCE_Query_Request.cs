using System;
using System.Collections.Generic;
using System.Text;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    public class C_PROVINCE_Query_Request:BaseQueryRequest,ITopRequest<C_PROVINCE_Query_Response>
    {


        public void Validate()
        {
            RequestValidator.ValidateRequired("Table", this.Table);
        }

 

    }
}
