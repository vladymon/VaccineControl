using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VC.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = Startup.messageRequired)]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string Username { get; set; }

        [Required(ErrorMessage = Startup.messageRequired)]
        [MinLength(8)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recuerdeme")]
        public bool RememberMe { get; set; }
    }

}
