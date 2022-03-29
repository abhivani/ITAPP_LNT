using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    [Table("tblUSBRequestForm")]
    public partial class tblUSBRequestForm
    {
        [Key]
        public long USBRequestFormId { get; set; }

        public long RequestFormId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string DepartmentName { get; set; }
        [StringLength(100)]
        public string DepartmentHead { get; set; }

        public string Reason { get; set; }
        [StringLength(100)]
        public string Location { get; set; }

        public string Notes { get; set; }
        public bool IsAccept { get; set; }
        public string DHRemarks { get; set; }

        public string ITDHRemarks { get; set; }
        [StringLength(100)]
        public string SerialNo { get; set; }
        [StringLength(100)]
        public string Model { get; set; }
        [StringLength(100)]
        public string Vendor { get; set; }

        public string Description { get; set; }

        public string Remarks { get; set; }

        public bool IsReject { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime? CreatedOn { get; set; }
        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? UpdateOn { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }

    }
}
