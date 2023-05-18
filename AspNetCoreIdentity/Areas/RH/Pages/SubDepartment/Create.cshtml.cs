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
    public class CreateModel : PageModel
    {
        public SelectList Departamentos { get; set; }
        public SelectList Empleados { get; set; }

        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;
        public CreateModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public SubDepartamento SubDepartamento { get; set; }
        public async Task OnGetAsync()
        {
            Departamentos = new SelectList(await _context.Departamento.ToListAsync(), nameof(Departamento.Id), nameof(Departamento.Nombre));
            Empleados = new SelectList(await _context.Empleado.ToListAsync(), nameof(Empleado.Id), nameof(Empleado.CorreoInstitucion));
        }
        public async Task<IActionResult> OnpostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (await ClaveExists(SubDepartamento.ClaveSubDepartamento))
            {
                ModelState.AddModelError("Clave Existente", "Clave Existente en otro Sub departamento");
                return Page();
            }
            SubDepartamento.Estatus = 1;

            await _context.SubDepartamento.AddAsync(SubDepartamento);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");            
        }
        private async Task<bool> ClaveExists(string claveDepto)
        {
            return await _context.SubDepartamento.AnyAsync(e => e.ClaveSubDepartamento == claveDepto);
        }
    }
}
