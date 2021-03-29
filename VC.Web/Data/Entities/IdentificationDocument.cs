using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VC.Web.Data.Entities
{
    public class IdentificationDocument
    {
        public string dni { get; set; }
        public string cui { get; set; }
        public string nombres { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }

    }
}
