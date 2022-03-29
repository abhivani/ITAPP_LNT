using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    public class DriveAccessFormsMV
    {
        public RequestFomsVM requestFomsVM { get; set; }
        public DriveAccessFormsProcessBarVM driveAccessFormsProcessBarVM { get; set; }
        public DriveAccessFormsMV()
        {
            requestFomsVM = new RequestFomsVM();
            driveAccessFormsProcessBarVM = new DriveAccessFormsProcessBarVM();
        }
        public long DriveAccessFormId { get; set; }
        public long RequestFormId { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentHead { get; set; }
        public string OnBehalfof { get; set; }
        public string Location { get; set; }
        public string Reason { get; set; }
        public string Path { get; set; }
        public string DHRemarks { get; set; }
        public string ITDHRemarks { get; set; }
        public string SARemarks { get; set; }
        public bool? IsDeleted { get; set; }
        public bool IsReject { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string UpdateBy { get; set; }
    }
    
    public class DriveAccessFormsProcessBarVM
    {
        public long DriveAccessFormStatusHistoryId { get; set; }

        public long DriveAccessFormId { get; set; }

        public string Initiate { get; set; }
        public string DepartmentHeadApprove { get; set; }
        public string InfraHeadApprove { get; set; }
        public string ServerAdmin { get; set; }

      
    }
}
