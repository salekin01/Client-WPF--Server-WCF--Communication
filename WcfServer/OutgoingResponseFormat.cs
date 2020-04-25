using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace wcfServer
{
    public static class OutgoingResponseFormat
    {
        public static void SetResponseFormat(string format)
        {
            if (string.Equals("json", format.ToLower(), StringComparison.OrdinalIgnoreCase))
                WebOperationContext.Current.OutgoingResponse.Format = WebMessageFormat.Json;
            else
                WebOperationContext.Current.OutgoingResponse.Format = WebMessageFormat.Xml;
        }

        public static string GetFormat()
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request["format"]))
                return "xml";
            else
                return HttpContext.Current.Request["format"].ToString();
        }
    }
}