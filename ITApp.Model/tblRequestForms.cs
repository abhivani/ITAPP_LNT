using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    [Table("tblRequestForms")]
    public partial class tblRequestForms
    {
        [Key]
        public long RequestFormId { get; set; }
        [StringLength(50)]

        public string PsNo { get; set; }
        [StringLength(50)]
        public string RequestNo { get; set; }

        public DateTime RequestDate { get; set; }
        [StringLength(100)]
        public string RequestFor { get; set; }
        [StringLength(100)]
        public string PendingWith { get; set; }
        [StringLength(100)]
        public string Status { get; set; }
        [StringLength(100)]
        public string DepartmentHeadId { get; set; }
        [StringLength(100)]
        public string ReturnRejectWith { get; set; }
        [StringLength(50)]
        public string ReturnRejectBy { get; set; }
        [StringLength(100)]
        public string ReturnRejectName { get; set; }
     
        public DateTime? ReturnRejectDate { get; set; }
        [StringLength(100)]
        public string LastPendingWith { get; set; }
        
        public bool? IsDeleted { get; set; }

        public DateTime? CreatedOn { get; set; }
        [StringLength(50)]
        public string CreateBy { get; set; }
        [StringLength(100)]
        public string CreatedByName { get; set; }

        public DateTime? UpdateOn { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }
        [StringLength(100)]
        public string UpdateByName { get; set; }

    }
}
