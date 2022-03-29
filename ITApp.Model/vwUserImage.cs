using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    [Table("vwUserImage")]
    public class vwUserImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ROWID { get; set; }

        [StringLength(30)]
        public string PSNo { get; set; }

        public byte[] ImageData { get; set; }
    }
}
