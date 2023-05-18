using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.RH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentity.Areas.RH.Pages.Department
{
    public class EditModel : PageModel
    {
        public SelectList Empleados { get; set; }

        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;
        public EditModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public string Clave { get; set; }

        [BindProperty]
        public Departamento Departamento { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Empleados = new SelectList(_context.Empleado.ToList(), nameof(Empleado.Id), nameof(Empleado.CorreoInstitucion));
            Departamento = await _context.Departamento.FirstOrDefaultAsync(m => m.Id == id);
            if (Departamento == null)
            {
                return NotFound();
            }
            Clave = Departamento.ClaveDepartamento;
            return Page();
        }
        public async Task<IActionResult> OnpostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!(Departamento.ClaveDepartamento == Clave))
            {
                if (ClaveExists(Departamento.ClaveDepartamento))
                {
                    ModelState.AddModelError("Clave Existente", "Clave Existente en otro departamento");                    
                    return Page();
                }
            }

            _context.Attach(Departamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(Departamento.Id))
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
        private bool ClaveExists(string claveDepto)
        {
            return _context.Departamento.Any(e => e.ClaveDepartamento == claveDepto);
        }
        private bool DepartamentoExists(int id)
        {
            return _context.Departamento.Any(e => e.Id == id);
        }
    }
}
