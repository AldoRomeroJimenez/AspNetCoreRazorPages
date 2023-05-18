using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AspNetCoreIdentity.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorPModel : PageModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }
        public int Error { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;
       

        public ErrorPModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;            
        }
        public void OnGet(int e)
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

                case 401:
                    Error = 401;
                    Message = "Usuario sin acceso, favor de iniciar sesion";
                    break;
            }
        }
    }
}
