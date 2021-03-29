using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VC.Web.Data.Entities;

namespace VC.Web.Models
{
    public class FindUserViewModel
    {
        public string Document { get; set; }
        public User User { get; set; }
        public IEnumerable<FindUserDetailViewModel> FindUserDetailViewModels { get; set; }
    }
}
