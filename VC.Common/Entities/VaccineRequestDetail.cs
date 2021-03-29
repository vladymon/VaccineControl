using System;
using System.Collections.Generic;
using System.Text;

namespace VC.Common.Entities
{
    public class VaccineRequestDetail
    {
        public int Id { get; set; }

        public Clinic Clinic { get; set; }

        public Calendar Calendar { get; set; }

        public RequestStatus RequestStatus { get; set; }

    }
}
