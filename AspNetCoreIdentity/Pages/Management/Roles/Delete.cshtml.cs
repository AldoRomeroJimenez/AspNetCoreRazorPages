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
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public RoleViewModel Model { get; set; }
        [BindProperty]
        public string Id { get; set; }

        private readonly RoleManager<CompanyRoles> roleManager;
        private readonly UserManager<IdentityCompanyUser> userManager;
        public DeleteModel(RoleManager<CompanyRoles> roleManager, UserManager<IdentityCompanyUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await roleManager.FindByIdAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            var x = new RoleViewModel
            {
                RoleName = rol.Name,
                Description = rol.Description

            };

            var u = userManager.Users.ToList();
            foreach (var user in u)
            {
                if (await userManager.IsInRoleAsync(user, rol.Name))
                {
                    x.Users.Add(user.UserName);
                }
            }

            Model = x;
            Id = id;
            return Page();            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var rol = await roleManager.FindByIdAsync(Id);
            if (rol == null)
            {
                return NotFound();
            }

            rol.Estatus = 0;
            var result = await roleManager.UpdateAsync(rol);
            if (result.Succeeded)
            {
                return RedirectToPage("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("User", error.Description);
                }
            }

            return Page();
        }
    }
}
