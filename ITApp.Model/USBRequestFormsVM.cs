using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    public class USBRequestFormsVM
    {
        public RequestFomsVM requestFomsVM { get; set; }       
        public USBRequestFormsProcessBarVM uSBRequestFormsProcessBarVM { get; set; }
        public USBRequestFormsVM()
        {
            requestFomsVM = new RequestFomsVM();
            uSBRequestFormsProcessBarVM = new USBRequestFormsProcessBarVM();
        }
        public long USBRequestFormId { get; set; }

        public long RequestFormId { get; set; }

        public string Name { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentHead { get; set; }

        public string Reason { get; set; }

        public string Location { get; set; }

        public string Notes { get; set; }

        public bool IsAccept { get; set; }

        public string DHRemarks { get; set; }

        public string ITDHRemarks { get; set; }

        public string SerialNo { get; set; }

        public string Model { get; set; }

        public string Vendor { get; set; }

        public string Description { get; set; }

        public string OTP { get; set; }

        public string Remarks { get; set; }

        public bool IsReject { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreateBy { get; set; }

        public DateTime? UpdateOn { get; set; }

        public string UpdateBy { get; set; }

    }   

    public class USBRequestFormsProcessBarVM
    {
        public long USBFormStatusHistoryId { get; set; }

        public long USBRequestFormId { get; set; }

        public string Initiate { get; set; }
        public string DepartmentHeadApproval { get; set; }
        public string ITDepartmentHeadApproval { get; set; }
        public string ITHelpdeskApproval { get; set; }        
        public string ITSecurity { get; set; }
        public string ITHelpdeskApprovalLast { get; set; }
    }
}
