using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    [Table("tblRoleMaster")]
    public partial class tblRoleMaster
    {
        [Key]
        public long RoleId { get; set; }

        [StringLength(50)]
        public string PsNo { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Role { get; set; }
        [StringLength(500)]
        public string Location { get; set; }

        [StringLength(100)]
        public string Email { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }
        [StringLength(100)]
        public string CreateByName { get; set; }
        public DateTime? UpdateOn { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }
        [StringLength(100)]
        public string UpdateByName { get; set; }




    }
}
