using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreIdentity.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AspNetCoreIdentity.Pages.Management.Company
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public IFormFile Cert { get; set; }
        [BindProperty]
        public IFormFile Key { get; set; }
        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;
        private readonly IHostingEnvironment _environment;
        [BindProperty]
        public string ClavePrivSAT { get; set; }

        public EditModel(AspNetCoreIdentity.Model.AccountDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Empresa Empresa { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empresa = await _context.Empresa.FirstOrDefaultAsync(m => m.Id == id);

            if (Empresa == null)
            {
                return NotFound();
            }
            ClavePrivSAT = (Empresa.ClavePrivadaSAT);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Cert != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "Compartidos");
                string IdFile = Guid.NewGuid() + "_" + Cert.FileName;
                string filepath = Path.Combine(uploadFolder, IdFile);
                if (!(Path.GetExtension(filepath).ToLower() == ".cer"))
                {
                    ModelState.AddModelError("Cert File Extencion", "Ingresa el archivo .cer proporcionado por el SAT");
                    return Page();
                }                
                using (var fileStream2 = new FileStream(filepath, FileMode.Create))
                {
                    Cert.CopyTo(fileStream2);
                }
                Empresa.CertificadoSAT = filepath;                
            }
            if (Key != null)
            {
                string uploadFolder2 = Path.Combine(_environment.WebRootPath, "Compartidos");
                string IdFile2 = Guid.NewGuid() + "_" + Key.FileName;
                string filepath2 = Path.Combine(uploadFolder2, IdFile2);
                if (!(Path.GetExtension(filepath2).ToLower() == ".key"))
                {
                    ModelState.AddModelError("Cert File Extencion", "Ingresa el archivo .key proporcionado por el SAT");
                    return Page();
                }
                using (var fileStream2 = new FileStream(filepath2, FileMode.Create))
                {
                    Key.CopyTo(fileStream2);
                }
                Empresa.KeySAT = filepath2;
            }
            Empresa.ClavePrivadaSAT = ClavePrivSAT;
            _context.Attach(Empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(Empresa.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmpresaExists(Guid id)
        {
            return _context.Empresa.Any(e => e.Id == id);
        }
    }
}
