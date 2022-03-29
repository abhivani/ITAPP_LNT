using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    [Table("tblDeviceShiftingForm")]
    public partial class tblDeviceShiftingForm
    {
        [Key]
        public long DeviceShiftingFormId { get; set; }
        public long RequestFormId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string DepartmentName { get; set; }
        [StringLength(100)]
        public string DepartmentHead { get; set; }
        [StringLength(500)]
        public string OnBehalfof { get; set; }
        [StringLength(100)]
        public string Location { get; set; }
        [StringLength(100)]
        public string DeviceName { get; set; }
        [StringLength(100)]
        public string DeviceNo { get; set; }
        [StringLength(100)]
        public string FromLocation { get; set; }
        [StringLength(100)]
        public string ToLocation { get; set; }
        public string ShiftingReason { get; set; }
        public string OwnerRemarks { get; set; }
        public string DHRemarks { get; set; }
        public string ITHelpdeskAdminRemarks { get; set; }
        public string ITHelpdeskRemarks { get; set; }
        [StringLength(100)]
        public string UpdatedinSampatti { get; set; }
        public bool? IsDeleted { get; set; }
        public bool IsReject { get; set; }
        public DateTime? CreatedOn { get; set; }
        [StringLength(50)]
        public string CreateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }
        public string DeviceType { get; set; }
        public string ShiftingType { get; set; }
    }
}