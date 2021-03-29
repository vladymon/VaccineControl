using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VC.Web.Models
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Contraseña Actual")]
        [Required(ErrorMessage = Startup.messageRequired)]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} field must contain between {2} and {1} characters.")]
        public string OldPassword { get; set; }

        [Display(Name = "Nueva Contraseña")]
        [Required(ErrorMessage = Startup.messageRequired)]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = Startup.messagePassword)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [Required(ErrorMessage = Startup.messageRequired)]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = Startup.messagePassword)]
        [Compare("NewPassword")]
        public string Confirm { get; set; }
    }

}
