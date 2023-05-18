using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.RH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreIdentity.Areas.RH.Pages.Department
{
    public class CreateModel : PageModel
    {
        public SelectList Empleados { get; set; }

        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;
        public CreateModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Departamento Departamento { get; set; }
        public void OnGet()
        {
            Empleados = new SelectList(_context.Empleado.ToList(), nameof(Empleado.Id), nameof(Empleado.CorreoInstitucion));
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ClaveExists(Departamento.ClaveDepartamento))
            {
                ModelState.AddModelError("Clave Existente","Clave Existente en otro departamento");
                return Page();
            }
            Departamento.Estatus = 1;

            _context.Departamento.Add(Departamento);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
            
        }
        private bool ClaveExists(string claveDepto)
        {
            return _context.Departamento.Any(e => e.ClaveDepartamento == claveDepto);
        }
    }
}
