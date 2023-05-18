using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreIdentity.Pages.Management.Roles
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly RoleManager<CompanyRoles> roleManager;

        public IndexModel(RoleManager<CompanyRoles> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IList<CompanyRoles> Roles { get; set; }
        public void OnGet()
        {
            Roles = roleManager.Roles.Where(r => r.Estatus.Equals(1)).ToList();
        }
    }
}
