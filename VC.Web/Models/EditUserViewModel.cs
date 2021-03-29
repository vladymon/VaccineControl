using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VC.Web.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Nro. de Documento")]
        [MaxLength(20)]
        [Required(ErrorMessage = Startup.messageRequired)]
        
        public string Document { get; set; }

        [Display(Name = "Nombre(s)")]
        [MaxLength(200)]
        [Required(ErrorMessage = Startup.messageRequired)]
        public string FirstName { get; set; }

        [Display(Name = "Apellido Paterno")]
        [MaxLength(100)]
        [Required(ErrorMessage = Startup.messageRequired)]
        public string LastName { get; set; }

        [Display(Name = "Apellido Materno")]
        [MaxLength(100)]
        [Required(ErrorMessage = Startup.messageRequired)]
        public string MotherLastName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://vcwebstorage.blob.core.windows.net/users/noimage.png"
            : $"https://vcwebstorage.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Foto")]
        public IFormFile ImageFile { get; set; }

        //[Required]
        //[Display(Name = "Country")]
        //[Range(1, int.MaxValue, ErrorMessage = "You must select a country.")]
        //public int CountryId { get; set; }

        //public IEnumerable<SelectListItem> Countries { get; set; }

        //[Required]
        //[Display(Name = "Department")]
        //[Range(1, int.MaxValue, ErrorMessage = "You must select a department.")]
        //public int DepartmentId { get; set; }

        //public IEnumerable<SelectListItem> Departments { get; set; }

        //[Required]
        //[Display(Name = "City")]
        //[Range(1, int.MaxValue, ErrorMessage = "You must select a city.")]
        //public int CityId { get; set; }

        //public IEnumerable<SelectListItem> Cities { get; set; }
    }

}
