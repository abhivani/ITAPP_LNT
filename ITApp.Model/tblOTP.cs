using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    [Table("tblOTP")]
    public partial class tblOTP
    {
        [Key]
        public long OTPId { get; set; }

        public long RequestFormId { get; set; }
        [StringLength(100)]
        public string RequestFor { get; set; }
        [StringLength(50)]
        public string OTP { get; set; }
        [StringLength(100)]
        public string EmailId { get; set; }
        [StringLength(50)]
        public string PsNo { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedOn { get; set; }
        [StringLength(50)]
        public string CreateBy { get; set; }
    }
}
