using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VC.Web.Models
{
    public class AddClinicViewModel
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe contener menos de {1} carateres ")]
        [Required]
        [Display(Name = "Centro de Salud")]
        public string Name { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe contener menos de {1} carateres ")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Atenciones por día")]
        public int AttentionPerDay { get; set; }

        [Required]
        [Display(Name = "Departmento")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione el departamento")]
        public int DepartmentId { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; }

        [Required]
        [Display(Name = "Provincia")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione la provincia")]
        public int ProvinceId { get; set; }

        public IEnumerable<SelectListItem> Provinces { get; set; }

        [Required]
        [Display(Name = "Distrito")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione el distrito")]
        public int DistrictId { get; set; }

        public IEnumerable<SelectListItem> Districts { get; set; }
    }
}
