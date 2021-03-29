using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VC.Common.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [DisplayName("Departamento")]
        public string Name { get; set; }

        public ICollection<Province> Provinces { get; set; }

        [DisplayName("Nro de provincias")]
        public int ProvincesNumber => Provinces == null ? 0 : Provinces.Count;

        [JsonIgnore]
        [NotMapped]
        public int IdCountry { get; set; }
    }

}
