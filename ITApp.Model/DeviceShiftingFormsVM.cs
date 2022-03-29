using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    public class DeviceShiftingFormsVM
    {
        public RequestFomsVM requestFomsVM { get; set; }
        public DeviceShiftingFormProcessBarVM deviceShiftingFormProcessBarVM { get; set; }
       
        public DeviceShiftingFormsVM()
        {
            requestFomsVM = new RequestFomsVM();
            deviceShiftingFormProcessBarVM = new DeviceShiftingFormProcessBarVM();
        }
        public long DeviceShiftingFormId { get; set; }

        public long RequestFormId { get; set; }

        public string Name { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentHead { get; set; }

        public string OnBehalfof { get; set; }

        public string Location { get; set; }

        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string ShiftingType { get; set; }

        public string DeviceNo { get; set; }

        public string FromLocation { get; set; }

        public string ToLocation { get; set; }

        public string ShiftingReason { get; set; }
        public string OwnerRemarks { get; set; }

        public string DHRemarks { get; set; }

        public string ITHelpdeskAdminRemarks { get; set; }

        public string ITHelpdeskRemarks { get; set; }

        public string UpdatedinSampatti { get; set; }

        public bool? IsDeleted { get; set; }

        public bool IsReject { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreateBy { get; set; }

        public DateTime? UpdateOn { get; set; }

        public string UpdateBy { get; set; }
    }
 
    public class DeviceShiftingFormProcessBarVM
    {
        public long DeviceShiftingFormStatusHistoryId { get; set; }

        public long DeviceShiftingFormId { get; set; }

        public string Initiate { get; set; }
        public string Owner { get; set; }
        public string DepartmentHeadApprove { get; set; }
        public string ITHelpdeskAdminApprove { get; set; }
        public string ITHelpdeskApprove { get; set; }





    }
}
