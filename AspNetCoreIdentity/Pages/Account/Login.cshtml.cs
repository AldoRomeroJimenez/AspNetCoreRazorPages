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
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login Model { get; set; }
        private readonly SignInManager<IdentityCompanyUser> _signInManager;
        private readonly UserManager<IdentityCompanyUser> _userManager;
        public LoginModel(SignInManager<IdentityCompanyUser> signInManager,UserManager<IdentityCompanyUser> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await _signInManager.PasswordSignInAsync(Model.Email, Model.Password, Model.RememberMe, false);
                if (identityResult.Succeeded)
                {
                    if (returnUrl == null || returnUrl == "/")
                    {
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                ModelState.AddModelError("Login Failed", "User or Password Is Incorrect");
            }
            return Page();
        }

        //public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existingUser = await _userManager.FindByEmailAsync(Model.Email);
        //        if (existingUser == null)
        //        {
        //            ModelState.AddModelError("Login Failed", "Username not found, check the email address");
        //            return Page();
        //        }
        //        var isCorrect = await _userManager.CheckPasswordAsync(existingUser,Model.Password);
        //        if (isCorrect)
        //        {
        //             await _signInManager.SignInAsync(existingUser, Model.RememberMe);
        //            if (returnUrl == null || returnUrl == "/")
        //            {
        //                return RedirectToPage("/Index");
        //            }
        //            else
        //            {
        //                return RedirectToPage(returnUrl);
        //            }
        //        }
                
        //        ModelState.AddModelError("Login Failed", "Password Incorrect");
        //    }
        //    return Page();
        //}

    }
}
