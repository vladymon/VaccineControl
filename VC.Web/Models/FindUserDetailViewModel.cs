using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VC.Web.Models
{
    public class FindUserDetailViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CalendarId { get; set; }
        public string Clinic { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public int Day { get; set; }
        public int DetailId { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }

    }
}
