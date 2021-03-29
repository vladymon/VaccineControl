using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VC.Web.Data;
using VC.Web.Data.Entities;

namespace VC.Web.Helpers
{
    public class VaccineRequestHelper : IVaccineRequestHelper
    {
        private readonly DataContext _context;

        public VaccineRequestHelper(DataContext context)
        {
            _context = context;

        }

        public IEnumerable<VaccineRequest> GetVaccineRequest()
        {
            List<VaccineRequest> list = new List<VaccineRequest>();
            

            return list;
        }
    }
}
