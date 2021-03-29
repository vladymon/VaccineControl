using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VC.Common.Entities;

namespace VC.Web.Models
{
    public class ListClinicViewModel
    {
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

        public IEnumerable<Clinic> Clinics { get; set; }
    }
}
