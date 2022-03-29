using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    public class ResponseModel
    {
        public string Result { get; set; }
        public long PrimeryKeyId { get; set; }
        public string Message { get; set; }

        //How to Used Other Parameter
        //new { EmpName = "Employee 1",EmpCode="EMP0001"}
        //Get : ResponseModel.OtherParameter.EmpName , ResponseModel.OtherParameter.EmpCode
        public object OtherParameter { get; set; }
    }
}
