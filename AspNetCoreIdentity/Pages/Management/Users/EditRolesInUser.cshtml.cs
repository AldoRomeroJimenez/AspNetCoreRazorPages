using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.Management;
using AspNetCoreIdentity.ViewModels.Management;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreIdentity.Pages.Management.Users
{
    public class EditRolesInUserModel : PageModel
    {
        [BindProperty]
        public List<EditRolesUserViewModel> Model { get; set; }
        public string UserName { get; set; }

        private readonly UserManager<IdentityCompanyUser> userManager;
        private readonly RoleManager<CompanyRoles> roleManager;

        public EditRolesInUserModel(UserManager<IdentityCompanyUser> userManager,RoleManager<CompanyRoles> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var userCurrent = await userManager.FindByIdAsync(id);            
            if (userCurrent == null)
            {
                return NotFound();
            }
            UserName = userCurrent.UserName;
            var model2 = new List<EditRolesUserViewModel>();
            foreach (var role in roleManager.Roles.Where(r => r.Estatus.Equals(1)).ToList())
            {
                var userRoles = new EditRolesUserViewModel
                {
                    UserId = userCurrent.Id,
                    RoleId = role.Id,
                    RoleName = role.Name                    
                };

                if (await userManager.IsInRoleAsync(userCurrent, role.Name))
                    userRoles.IsSelected = true;
                else
                    userRoles.IsSelected = false;

                model2.Add(userRoles);

            }
            Model = model2;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var userCurrent = await userManager.FindByIdAsync(id);
            if (userCurrent == null)
            {
                return NotFound();
            }

            var roles = await userManager.GetRolesAsync(userCurrent);
            var result = await userManager.RemoveFromRolesAsync(userCurrent, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "No se puede remover role a usuario");
                return Page();
            }
            result = await userManager.AddToRolesAsync(userCurrent, Model.Where(x => x.IsSelected).Select(r => r.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "No se puede agregar roles a usuario");
                return Page();
            }

            return RedirectToPage("Edit",new { id = Model[0].UserId });
        }
    }
}
