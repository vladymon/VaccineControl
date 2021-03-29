using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VC.Common.Entities
{
    public class RequestStatus
    {
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "El campo {0} debe contener menos de {1} carateres ")]
        [DisplayName("Estado")]
        public string Name { get; set; }
    }
}
