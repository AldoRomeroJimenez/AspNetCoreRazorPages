using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.RH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentity.Areas.RH.Pages.SubDepartment
{
    public class EditModel : PageModel
    {
        public SelectList Empleados { get; set; }
        public SelectList Departamentos { get; set; }

        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;
        public EditModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public string ClaveSub { get; set; }

        [BindProperty]
        public SubDepartamento SubDepartamento { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SubDepartamento = await _context.SubDepartamento.FirstOrDefaultAsync(s => s.Id == id);
            if (SubDepartamento == null)
            {
                return NotFound();
            }

            Empleados = new SelectList(await _context.Empleado.ToListAsync(), nameof(Empleado.Id), nameof(Empleado.CorreoInstitucion));
            Departamentos = new SelectList(await _context.Departamento.ToListAsync(), nameof(Departamento.Id), nameof(Departamento.Nombre));
            ClaveSub = SubDepartamento.ClaveSubDepartamento;

            return Page();
        }
        public async Task<IActionResult> OnpostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!(SubDepartamento.ClaveSubDepartamento == ClaveSub))
            {
                if (await ClaveExists(SubDepartamento.ClaveSubDepartamento))
                {
                    ModelState.AddModelError("Clave Existente", "Clave Existente en otro departamento");
                    return Page();
                }
            }

            _context.Attach(SubDepartamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! (await SubDepartamentoExists(SubDepartamento.Id)))
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
        private async Task<bool> ClaveExists(string claveDepto)
        {
            return await  _context.SubDepartamento.AnyAsync(e => e.ClaveSubDepartamento == claveDepto);
        }
        private async Task<bool> SubDepartamentoExists(int id)
        {
            return await _context.SubDepartamento.AnyAsync(e => e.Id == id);
        }
    }
}
