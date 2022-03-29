using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    [Table("tblDamageDeviceForm")]
    public partial class tblDamageDeviceForm
    {
        [Key]
        public long DamageDeviceFormId { get; set; }

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
        public string DamagedDeviceName { get; set; }
        [StringLength(100)]
        public string DamagedDeviceNo { get; set; }

        public string DamageReason { get; set; }

        public string DHRemarks { get; set; }

        public string ITHelpdeskAdminRemarks { get; set; }

        public string ITHelpdeskRemarks { get; set; }
        [StringLength(100)]
        public string DStatus { get; set; }

        public bool? IsDeleted { get; set; }

        public bool IsReject { get; set; }

        public DateTime? CreatedOn { get; set; }
        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? UpdateOn { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }

    }
}
