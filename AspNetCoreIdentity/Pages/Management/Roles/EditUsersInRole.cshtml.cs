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
    public class EditUsersInRoleModel : PageModel
    {
        [BindProperty]
        public List<UserRolViewModel> Model { get; set; }

        [BindProperty]
        public string RolId { get; set; }
        [BindProperty]
        public string RolName { get; set; }

        private readonly RoleManager<CompanyRoles> roleManager;
        private readonly UserManager<IdentityCompanyUser> userManager;

        public EditUsersInRoleModel(RoleManager<CompanyRoles> roleManager, UserManager<IdentityCompanyUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var model = new List<UserRolViewModel>();
            foreach (var user in userManager.Users.ToList())
            {
                var userRoleVM = new UserRolViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleVM.IsSelected = true;
                }
                else
                {
                    userRoleVM.IsSelected = false;

                } 
                model.Add(userRoleVM);
            }
            RolId = role.Id;RolName = role.Name;Model = model;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var role = await roleManager.FindByIdAsync(RolId);
            if (role == null)
            {
                return NotFound();
            }

            for (int i = 0; i < Model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(Model[i].UserId);
                IdentityResult result = null;

                if (Model[i].IsSelected && !(await userManager.IsInRoleAsync(user,role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!Model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (Model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToPage("Edit", new {id = RolId });
                    }
                }
            }
            return RedirectToPage("Edit", new { id = RolId });
        }
    }
}
