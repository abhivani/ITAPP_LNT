using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    public class DamageDeviceFormsVM
    {
        public RequestFomsVM requestFomsVM { get; set; }
        public DamageDeviceFormProcessBarVM damageDeviceFormProcessBarVM { get; set; }
        public DamageDeviceFormsVM()
        {
            requestFomsVM = new RequestFomsVM();
            damageDeviceFormProcessBarVM = new DamageDeviceFormProcessBarVM();
           
        }
        public long DamageDeviceFormId { get; set; }

        public long RequestFormId { get; set; }

        public string Name { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentHead { get; set; }

        public string OnBehalfof { get; set; }

        public string Location { get; set; }

        public string DamagedDeviceName { get; set; }

        public string DamagedDeviceNo { get; set; }

        public string DamageReason { get; set; }

        public string DHRemarks { get; set; }

        public string ITHelpdeskAdminRemarks { get; set; }

        public string ITHelpdeskRemarks { get; set; }

        public string DStatus { get; set; }

        public bool? IsDeleted { get; set; }

        public bool IsReject { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreateBy { get; set; }

        public DateTime? UpdateOn { get; set; }

        public string UpdateBy { get; set; }
    }
   

    public class DamageDeviceFormProcessBarVM
    {
        public long DamageDeviceFormStatusHistoryId { get; set; }

        public long DamageDeviceFormId { get; set; }

        public string Initiate { get; set; }
        public string DepartmentHeadApprove { get; set; }
        public string ITHelpdeskAdminApprove { get; set; }
        public string ITHelpdeskApprove { get; set; }


    }

}
