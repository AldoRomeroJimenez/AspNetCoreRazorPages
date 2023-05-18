using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.Management;
using AspNetCoreIdentity.ViewModels.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreIdentity.Pages.Management.Roles
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public RoleViewModel Model { get; set; }

        private readonly RoleManager<CompanyRoles> _roleManager;

        public CreateModel(RoleManager<CompanyRoles> roleManager)
        {
            this._roleManager = roleManager;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var roleExisting = await _roleManager.FindByNameAsync(Model.RoleName);
                if (roleExisting != null)
                {
                    ModelState.AddModelError("User", "Rol Existente");
                    return Page();
                }

                var newRole = new CompanyRoles
                {
                    Name = Model.RoleName,
                    Description = Model.Description,
                    Estatus = 1                    
                };

                var isCreated = await _roleManager.CreateAsync(newRole);
                if (isCreated.Succeeded)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    foreach (var error in isCreated.Errors)
                    {
                        ModelState.AddModelError("User", error.Description);
                    }
                }
            }
            return Page();
        }
    }
}
