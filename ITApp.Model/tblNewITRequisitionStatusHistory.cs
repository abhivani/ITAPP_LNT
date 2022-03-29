using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    [Table("tblNewITRequisitionStatusHistory")]
    public class tblNewITRequisitionStatusHistory
    {
        [Key]
        public long NewITRequisitionStatusHistoryId { get; set; }

        public long NewITRequisitionID { get; set; }

        public string StatusName { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreateBy { get; set; }

        public string CreateByName { get; set; }

        public DateTime? UpdateOn { get; set; }

        public string UpdateBy { get; set; }

        public string UpdateByName { get; set; }
    }
}
