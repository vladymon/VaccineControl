using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VC.Web.Data.Entities
{
    public class UserType
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [DisplayName("Tipo de Usuario")]
        public string Name { get; set; }
    }
}
