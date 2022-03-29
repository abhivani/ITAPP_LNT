using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITApp.Model
{
    public class LoginModel
    {
        [Required]
        public string UserPsno { get; set; }

        [Required]
        public string Password { get; set; }
        public bool agreecheckbox { get; set; }
    }
}
