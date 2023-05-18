using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetCoreIdentity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AspNetCoreIdentity.Pages.Management.Company
{
    public class CreateModel : PageModel
    {
        

        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;
        private readonly IHostingEnvironment environment;

        public CreateModel(AspNetCoreIdentity.Model.AccountDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            this.environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Empresa Empresa { get; set; }

        [BindProperty]
        public IFormFile Cert { get; set; }
        [BindProperty]
        public IFormFile Key { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Cert == null || Key == null)
            {
                ModelState.AddModelError("DatosSAT", "Ingresa un Certificado Y Key Proporcionado por el SAT");
                return Page();                
            }

            //string c,k;
            //using (var ms = new MemoryStream())
            //{
            //    Cert.CopyTo(ms);
            //    var fileBytes = ms.ToArray();
            //     c = Convert.ToBase64String(fileBytes);
            //}
            //using (var ms = new MemoryStream())
            //{
            //    Key.CopyTo(ms);
            //    var fileBytes = ms.ToArray();
            //    k = Convert.ToBase64String(fileBytes);
            //}
            
            string uploadFolder = Path.Combine(environment.WebRootPath, "Compartidos");
            
            string IdFile = Guid.NewGuid() + "_" +Cert.FileName;
            string IdFile2 = Guid.NewGuid() + "_" +Key.FileName;
            
            string filepath = Path.Combine(uploadFolder, IdFile);
            string filepath2 = Path.Combine(uploadFolder, IdFile2);

            if (!(Path.GetExtension(filepath).ToLower() == ".cer"))
            {
                ModelState.AddModelError("Cert File Extencion", "Ingresa el archivo .cer proporcionado por el SAT");
                return Page();
            }
            if (!(Path.GetExtension(filepath2).ToLower() == ".key"))
            {
                ModelState.AddModelError("Cert File Extencion", "Ingresa el archivo .key proporcionado por el SAT");
                return Page();
            }

            using (var fileStream2 = new FileStream(filepath, FileMode.Create))
            {
                Cert.CopyTo(fileStream2);
            }
            using (var fileStream2 = new FileStream(filepath2, FileMode.Create))
            {
                Key.CopyTo(fileStream2);
            }

            Empresa.CertificadoSAT = filepath;
            Empresa.KeySAT = filepath2;
            Empresa.Estatus = 1;

            _context.Empresa.Add(Empresa);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
