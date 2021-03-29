using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VC.Common.Entities
{
    public class District
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [DisplayName("Distrito")]
        public string Name { get; set; }

        public ICollection<Clinic> Clinics { get; set; }

        [DisplayName("Nro de centros de salud")]
        public int ClinicsNumber => Clinics == null ? 0 : Clinics.Count;

        [JsonIgnore]
        [NotMapped]
        public int IdProvince { get; set; }

    }

}
