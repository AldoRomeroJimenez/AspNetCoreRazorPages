using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.Management;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreIdentity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityCompanyUser> _signInManager;
        private readonly UserManager<IdentityCompanyUser> _userManager;
        public LogoutModel(SignInManager<IdentityCompanyUser> signInManager, UserManager<IdentityCompanyUser> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("Login");
        }
        public IActionResult OnPostDontLogoutAsync()
        {
            return RedirectToPage("/Index");
        }
    }
}
