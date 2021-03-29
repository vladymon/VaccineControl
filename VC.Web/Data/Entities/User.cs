using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VC.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(20)]
        [Required]
        public string Document { get; set; }

        [Display(Name = "Nombre(s)")]
        [MaxLength(200)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Apellido Paterno")]
        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Apellido Materno")]
        [MaxLength(100)]
        [Required]
        public string MotherLastName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [Display(Name = "Imagen")]
        public Guid Image { get; set; }

        //TODO: Pending to put the correct paths
        [Display(Name = "Foto")]
        public string ImageFullPath => Image == Guid.Empty
            ? $"https://vcwebstorage.blob.core.windows.net/users/noimage.png"
            : $"https://vcwebstorage.blob.core.windows.net/users/{Image}";

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName => $"{FirstName} {LastName} {MotherLastName}";

        [Display(Name = "User")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }

}
