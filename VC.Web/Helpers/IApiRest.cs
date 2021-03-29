using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VC.Web.Data.Entities;

namespace VC.Web.Helpers
{
    public interface IApiRest
    {
        Task<IdentificationDocument> GetDni(string dni);
    }
}
