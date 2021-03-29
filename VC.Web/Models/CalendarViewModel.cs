using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VC.Common.Entities;

namespace VC.Web.Models
{
    public class CalendarViewModel
    {
        public string CurrentMonth { get; set; }

        public string CurrentYear { get; set; }

        public IEnumerable<Calendar> Calendars { get; set; }

    }
}
