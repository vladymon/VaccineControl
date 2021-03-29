using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VC.Common.Entities
{
    public class Vaccine
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe contener menos de {1} carateres ")]
        [Required]
        [DisplayName("Vacuna")]
        public string Name { get; set; }

        [DisplayName("Cantidad de dosis")]
        public int DosesNumber { get; set; }

    }
}
