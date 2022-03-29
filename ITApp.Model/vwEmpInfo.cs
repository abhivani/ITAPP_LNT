using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    [Table("vwEmpInfo")]
    public class vwEmpInfo
    {
        public string t_psno { get; set; }
        public string t_name { get; set; }
        public string t_iasp { get; set; }
        public string t_depc { get; set; }
        public string t_loca { get; set; }
        public string t_mail { get; set; }
        public string t_init { get; set; }
        public string t_ccdr { get; set; }
        public string CcdrDesc { get; set; }
        public string t_dsca { get; set; }
        public string bu { get; set; }
        public string t_telm { get; set; }
        public string EmpAddress { get; set; }
        public string t_dhps { get; set; }
        [Key]
        public long ROWID { get; set; }
    }
}
