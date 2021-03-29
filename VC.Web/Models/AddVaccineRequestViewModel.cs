using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VC.Common.Entities;
using VC.Web.Data.Entities;

namespace VC.Web.Models
{
    public class AddVaccineRequestViewModel
    {
        public string CurrentMonth { get; set; }
        public string CurrentYear { get; set; }
        public VaccineRequest VaccineRequest { get; set; }
        public IEnumerable<Calendar> Calendars { get; set; }
    }
}
