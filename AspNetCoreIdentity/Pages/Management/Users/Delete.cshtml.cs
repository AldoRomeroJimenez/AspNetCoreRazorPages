using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.Management;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreIdentity.Pages.Management.Users
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public IdentityCompanyUser Model { get; set; }

        public bool AutoDelete { get; set; }

        private readonly RoleManager<CompanyRoles> roleManager;
        private readonly UserManager<IdentityCompanyUser> userManager;

        public DeleteModel(RoleManager<CompanyRoles> roleManager, UserManager<IdentityCompanyUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var userToDelete = await userManager.FindByIdAsync(id);
            if (userToDelete == null)
            {
                return NotFound();
            }
            Model = userToDelete;

            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == userToDelete)
            {
                AutoDelete = false;
            }
            else
            {
                AutoDelete = true;
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var userDisabled = await userManager.FindByIdAsync(Model.Id);
            if (userDisabled == null)
            {
                return NotFound();
            }

            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == userDisabled)
            {
                return RedirectToPage("Index");
            }

            userDisabled.Estatus = 0;
            var result = await userManager.UpdateAsync(userDisabled);
            if (result.Succeeded)
            {
                return RedirectToPage("Index");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.Code, item.Description);
            }
            return Page();

        }
    }
}
