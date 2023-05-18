using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreIdentity.Pages.Management.Users
{
    [Authorize(Roles = "Admin")]

    public class IndexModel : PageModel
    {

        private readonly UserManager<IdentityCompanyUser> _userManager;
        
        public IndexModel(UserManager<IdentityCompanyUser> userManager)
        {
            this._userManager = userManager;
        }

        public IList<IdentityCompanyUser> Usuarios { get; set; }
        public void OnGet()
        {
            Usuarios = _userManager.Users.Where(u=>u.Estatus.Equals(1)).ToList();
        }
        
    }
}
