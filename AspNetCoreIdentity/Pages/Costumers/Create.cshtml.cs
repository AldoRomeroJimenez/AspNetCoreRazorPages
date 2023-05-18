using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetCoreIdentity.Model;
using AspNetCoreIdentity.Model.EstadosMunicipios;

namespace AspNetCoreIdentity.Pages.Costumers
{
    public class CreateModel : PageModel
    {
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

        public IActionResult OnGet()
        {
            Paises = new SelectList(_context.CPais.Where(p => p.c_Pais.Contains("MEX")).ToList(), nameof(CPais.id), nameof(CPais.Descripción));


            return Page();
        }

        [BindProperty]
        public Cliente Cliente { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (PaisId == 0 || EstadoId == 0 || MunicipioId == 0)
            {
                ModelState.AddModelError("Valores Direccion","Selecciona un municipio valido");
                return Page();
            }

            Cliente.Estatus = 1;
            Cliente.Estado = _context.CEstados.Where(e => e.EstadoId.Equals(EstadoId)).Select(e => e.Descripcion).FirstOrDefault();
            Cliente.Municipio = _context.CMunicipios.Where(e => e.MunicipioId.Equals(MunicipioId)).Select(e => e.Descripcion).FirstOrDefault();
            Cliente.Pais = _context.CPais.Where(p => p.id.Equals(PaisId)).Select(p => p.c_Pais).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            

            _context.Cliente.Add(Cliente);
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
