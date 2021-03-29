using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VC.Common.Entities
{
    public class Province
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [DisplayName("Provincia")]
        public string Name { get; set; }

        public ICollection<District> Districts { get; set; }

        [DisplayName("Nro de distritos")]
        public int DistrictsNumber => Districts == null ? 0 : Districts.Count;

        [JsonIgnore]
        [NotMapped]
        public int IdDepartment { get; set; }
    }
}
