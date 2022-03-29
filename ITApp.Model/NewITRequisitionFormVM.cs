using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
   public class NewITRequisitionFormVM
    {
        public RequestFomsVM requestFomsVM { get; set; }
        public List<NewITRequisitionStatusHistoryVM> NewITRequisitionStatusHistoryVM { get; set; }

        public NewITRequisitionProcessBarVM NewITRequisitionProcessBarVM { get; set; }

        public NewITRequisitionFormVM()
        {
            requestFomsVM = new RequestFomsVM();
            NewITRequisitionStatusHistoryVM = new List<NewITRequisitionStatusHistoryVM>();

            NewITRequisitionProcessBarVM = new NewITRequisitionProcessBarVM();


        }
        public long NewITRequisitionID { get; set; }

        public long RequestFormId { get; set; }

        public string RequestNo { get; set; }

        public string PSNo { get; set; }

        public string Department { get; set; }

        public string Name { get; set; }

        public string DepartmentHead { get; set; }

        public string OnBehalfof { get; set; }

        public string Device { get; set; }

        public string DHRemarks { get; set; }

        public string ITHDAdminRemark { get; set; }

        public string Location { get; set; }

        public string Reason { get; set; }

        public string ITDHRemark { get; set; }

        public string AssentID { get; set; }

        public string Mark { get; set; }

        public string Model { get; set; }

        public string Family { get; set; }

        public string OperationSystem { get; set; }

        public string MonitorSize { get; set; }

        public string SerialNO { get; set; }

        public string MACAddress { get; set; }

        public string InstallationRemark { get; set; }

        public string HDD { get; set; }

        public string RAM { get; set; }

        public string DeliveryBY { get; set; }

        public string AntivirusInstalled { get; set; }

        public string SoftwareInstalled { get; set; }

        public string DomainRegistion { get; set; }

        public string LocationByDelivery { get; set; }

        public string Area { get; set; }

        public bool? IsDeleted { get; set; }

        public bool IsReject { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreateBy { get; set; }

        public DateTime? UpdateOn { get; set; }

        public string UpdateBy { get; set; }
        public string OTP { get; set; }

        public string OneDriveConfigured { get; set; }
         public string BitlockerConfigured { get; set; }
    }

    public class NewITRequisitionStatusHistoryVM 
    {
        public long NewITRequisitionStatusHistoryId { get; set; }

        public long NewITRequisitionID { get; set; }

        public string StatusName { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreateBy { get; set; }

        public string CreateByName { get; set; }

        public DateTime? UpdateOn { get; set; }

        public string UpdateBy { get; set; }

        public string UpdateByName { get; set; }
    }

    public class NewITRequisitionProcessBarVM
    {
        public long NewITRequisitionID { get; set; }

        public long RequestFormId { get; set; }

        public string Initiate { get; set; }
        public string DepartmentHeadApproval { get; set; }
        public string ITDepartmentHeadApproval { get; set; }
        public string ITHelpdeskAdminApprove { get; set; }
        public string ITHelpdeskApproval { get; set; }        
        public string Compelte { get; set; }
    }

}
