using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MvcCoreProject_Iqbal.Models;

namespace MvcCoreProject_Iqbal.ViewModels
{
    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            Cliams = new List<UserClaim>();
        }

        public string UserId { get; set; }
        public List<UserClaim> Cliams { get; set; }
    }
}
