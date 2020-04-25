using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServer
{
    [ServiceContract]
    public interface IWcfServer
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ConvertNumberToWord/{number}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string ConvertNumberToWord(string number);
    }
}
