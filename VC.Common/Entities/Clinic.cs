using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VC.Common.Entities
{
    public class Clinic
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

        public int DistrictId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int IdDistrict { get; set; }

    }
}
