using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.EstadosMunicipios;
using AspNetCoreIdentity.Model.RH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreIdentity.Areas.RH.Pages.Employee
{
    public class CreateModel : PageModel
    {
        public SelectList Puestos { get; set; }
        public SelectList Paises { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PaisId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int EstadoId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int MunicipioId { get; set; }
        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;
        public CreateModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Empleado Empleado { get; set; }
        public IActionResult OnGet()
        {
            Paises = new SelectList(_context.CPais.Where(p => p.c_Pais.Contains("MEX")).ToList(), nameof(CPais.id), nameof(CPais.Descripción));
            Puestos = new SelectList(_context.Puesto.ToList(), nameof(Puesto.Id), nameof(Puesto.Descripcion));
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (PaisId == 0 || EstadoId == 0 || MunicipioId == 0)
            {
                ModelState.AddModelError("Valores Direccion", "Selecciona un municipio valido");
                return Page();
            }

            Empleado.Estatus = 1;
            Empleado.Estado = _context.CEstados.Where(e => e.EstadoId.Equals(EstadoId)).Select(e => e.Descripcion).FirstOrDefault();
            Empleado.Municipio = _context.CMunicipios.Where(e => e.MunicipioId.Equals(MunicipioId)).Select(e => e.Descripcion).FirstOrDefault();
            Empleado.Pais = _context.CPais.Where(p => p.id.Equals(PaisId)).Select(p => p.c_Pais).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Empleado.Add(Empleado);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public JsonResult OnGetEstados()
        {
            var elements = _context.CEstados.Where(e => e.PaisId.Equals(PaisId) && !e.EstadoId.Equals(33)).ToList();
            return new JsonResult(elements);
        }
        public JsonResult OnGetMunicipios()
        {
            var elements = _context.CMunicipios.Where(e => e.EstadoId.Equals(EstadoId)).ToList();
            return new JsonResult(elements);
        }
    }
}
