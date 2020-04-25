using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServer
{
     public class WcfServer : IWcfServer
    {
        public string ConvertNumberToWord(string number)
        {
            return Conversion.NumberToWord(number);
        }
    }
}
