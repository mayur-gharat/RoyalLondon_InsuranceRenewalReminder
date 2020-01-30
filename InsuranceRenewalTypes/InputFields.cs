using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceRenewalTypes
{
    /// <summary>
    /// This class is response for Reading Input file
    /// ResponseBase is inherited so as to set response code / error code / error message if any.
    /// </summary>
    public class InputFields : ResponseBase
    {
        public InputFields()
        {
            this.InputList = new List<InputField>();
        }

        public List<InputField> InputList;

    }
}
