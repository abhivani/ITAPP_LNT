using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    [Table("tblDriveAccessForm")]
    public partial class tblDriveAccessForm
    {
        [Key]
        public long DriveAccessFormId { get; set; }

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

        public string Reason { get; set; }

        public string Path { get; set; }

        public string DHRemarks { get; set; }

        public string ITDHRemarks { get; set; }

        public string SARemarks { get; set; }

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
