using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.Management;
using AspNetCoreIdentity.ViewModels.Account;
using AspNetCoreIdentity.ViewModels.Management;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreIdentity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        
        [BindProperty]
        public Register Model { get; set; }

        private readonly UserManager<IdentityCompanyUser> _userManager;
        private readonly SignInManager<IdentityCompanyUser> _signInManager;
        private readonly RoleManager<CompanyRoles> _roleManager;

        public RegisterModel(UserManager<IdentityCompanyUser> userManager, SignInManager<IdentityCompanyUser> signInManager, RoleManager<CompanyRoles> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(Model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("User", "User Exixting, try with a new Email");
                    return Page();
                }

                
                var NewUser = new IdentityCompanyUser()
                {
                    UserName = Model.Email,
                    Email = Model.Email,
                    Name = Model.Name,
                    LastName = Model.LastName,
                    PhoneNumber = Model.PhoneNumber
                    
                };
                var isCreated = await _userManager.CreateAsync(NewUser, Model.Password);
                if (isCreated.Succeeded)
                {
                    var role = await _roleManager.FindByNameAsync("Usuario");
                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(NewUser, role.Name);
                    }

                    await _signInManager.SignInAsync(NewUser, false);
                    return RedirectToPage("/Index");
                }

                foreach (var error in isCreated.Errors)
                {
                    ModelState.AddModelError("User", error.Description);
                }
            }
            return Page();
        }
    }
}
