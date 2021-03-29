using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VC.Common.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [DisplayName("Pais")]
        public string Name { get; set; }
        public ICollection<Department> Departments { get; set; }

        [DisplayName("Nro de departamentos")]
        public int DepartmentsNumber => Departments == null ? 0 : Departments.Count;

    }

}
