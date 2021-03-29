using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VC.Web.Data.Entities
{
    public class AttentionPerDayCalendar
    {
        public int AttentionPerDay { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CalendarId { get; set; }
    }
}
