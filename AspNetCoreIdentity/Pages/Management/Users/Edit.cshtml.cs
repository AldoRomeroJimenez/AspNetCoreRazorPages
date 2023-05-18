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

namespace AspNetCoreIdentity.Pages.Management.Users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty]
        public EditUserViewModel Model { get; set; }

        private readonly RoleManager<CompanyRoles> roleManager;
        private readonly UserManager<IdentityCompanyUser> userManager;

        public EditModel(RoleManager<CompanyRoles> roleManager, UserManager<IdentityCompanyUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var userToEdit = await userManager.FindByIdAsync(id);
            if (userToEdit == null)
            {
                return NotFound();
            }
            var userClaims = await userManager.GetClaimsAsync(userToEdit);
            var userRoles = await userManager.GetRolesAsync(userToEdit);

            var model = new EditUserViewModel
            {
                Id = userToEdit.Id,
                UserName = userToEdit.UserName,
                Name = userToEdit.Name,
                LastName = userToEdit.LastName,
                PhoneNumber = userToEdit.PhoneNumber,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles.ToList()
                
            };
            Model = model;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var userToEdit = await userManager.FindByIdAsync(Model.Id);
                if (userToEdit == null)
                {
                    ModelState.AddModelError("UserNotFound", "Usuario no encontrado");
                    return Page();
                }

                userToEdit.UserName = Model.UserName;
                userToEdit.Name = Model.Name;
                userToEdit.LastName = Model.LastName;
                userToEdit.PhoneNumber = Model.PhoneNumber;
                var result = await userManager.UpdateAsync(userToEdit);
                if (result.Succeeded)
                {
                    return Redirect("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
            }
            return Page();
        }
    }
}
