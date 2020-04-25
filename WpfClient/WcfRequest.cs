using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WpfClient
{
    public class WcfRequest
    {
        public static string SendWcfRequest(string url)
        {
            string result = string.Empty;
            try
            {
                
                WebClient client = new WebClient();
                string str = client.DownloadString(url);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                dynamic obj = serializer.Deserialize<dynamic>(str);
                if (obj == null)
                    return "Server has encountered a problem.";
                foreach (var item in obj)
                {
                    result = item.Value;
                }
            }
            catch(Exception ex)
            {
                return "Error: " + ex.Message.ToString();
            }
            return result;
        }  
    }
}
