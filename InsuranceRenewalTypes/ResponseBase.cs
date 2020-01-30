using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceRenewalTypes
{
    /// <summary>
    /// This is Custom Type created that will be base class for Response class
    /// This will be helpful to define logic on reteived response
    /// </summary>
    public class ResponseBase
    {
        public int ReturnCode;
        public string ReturnMessage;
    }
}
