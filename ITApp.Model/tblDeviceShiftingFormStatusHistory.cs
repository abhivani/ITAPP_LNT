using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    [Table("tblDeviceShiftingFormStatusHistory")]
    public partial class tblDeviceShiftingFormStatusHistory
    {
        [Key]
        public long DeviceShiftingFormStatusHistoryId { get; set; }

        public long DeviceShiftingFormId { get; set; }
        [StringLength(100)]
        public string StatusName { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }
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
