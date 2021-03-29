using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VC.Common.Entities;

namespace VC.Web.Data.Entities
{
    public class VaccineRequest
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        public User User { get; set; }

        //public string UserId { get; set; }

        public Vaccine Vaccine { get; set; }

        //public int VaccineId { get; set; }

        public ICollection<VaccineRequestDetail> VaccineRequestDetails { get; set; }

        public int VaccineNumber => VaccineRequestDetails.Where(v => v.RequestStatus.Name == Startup.appliedStatus) == null ? 0 : VaccineRequestDetails.Where(v => v.RequestStatus.Name == Startup.appliedStatus).ToList().Count;

        

    }
}
