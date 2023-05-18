using AspNetCoreIdentity.Model.Management;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }
        public int Error { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;
        private readonly SignInManager<IdentityCompanyUser> singInManager;

        public ErrorModel(ILogger<ErrorModel> logger, SignInManager<IdentityCompanyUser> singInManager)
        {
            _logger = logger;
            this.singInManager = singInManager;
        }

        public IActionResult OnGet(int e)
        {

            if (singInManager.IsSignedIn(User))
            {
                switch (e)
                {
                    case 404:
                        Message = "El proceso que ha solicitado no ha sido encontrado.";
                        Error = 404;
                        break;

                    case 500:
                        Error = 500;
                        Message = "Ha ocurrido un error de servidor, favor de intentarlo mas tarde!";
                        break;
                }
                return Page();
            }
            return RedirectToPage("ErrorP",new { e=e});
        }
    }
}
