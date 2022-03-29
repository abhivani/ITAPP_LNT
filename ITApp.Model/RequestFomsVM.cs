using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    public class RequestFomsVM
    {
        public long RequestFormId { get; set; }

        public string PsNo { get; set; }

        public string RequestNo { get; set; }

        public DateTime RequestDate { get; set; }

        public string RequestFor { get; set; }

        public string PendingWith { get; set; }

        public string Status { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreateBy { get; set; }
        public string CreateByName { get; set; }

        public DateTime? UpdateOn { get; set; }

        public string UpdateBy { get; set; }
        public string UpdateByName { get; set; }

        public long? USBRequestFormId { get; set; }
        public long? DriveAccessFormId { get; set; }
        public long? DamageDeviceFormId { get; set; }
        public long? NewITRequisitionID { get; set; }

        public long? DeviceShiftingFormId { get; set; }

        public bool IsPending { get; set; }

        public string ReturnRejectWith { get; set; }
        public string LastPendingWith { get; set; }

    }

    public class AllRequestFomsList
    {
        public List<RequestFomsVM> Pending { get; set; }
        public List<RequestFomsVM> MyRequest { get; set; }
        public List<RequestFomsVM> AllRequest { get; set; }


    }
}
