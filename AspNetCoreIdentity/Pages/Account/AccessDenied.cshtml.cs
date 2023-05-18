using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.Management;
using AspNetCoreIdentity.ViewModels.Management;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreIdentity.Pages.Account
{
    public class AccessDeniedModel : PageModel
    {
        private readonly SignInManager<IdentityCompanyUser> singInManager;

        public AccessDeniedModel(SignInManager<IdentityCompanyUser> singInManager)
        {
            this.singInManager = singInManager;
        }
        public string Model { get; set; }
        public IActionResult OnGet(string ReturnUrl)
        {
            Model = ReturnUrl;
            if (!singInManager.IsSignedIn(User))
            {
                return RedirectToPage("/ErrorP", new { e = 401 });
            }
            return Page();
        }
    }
}
