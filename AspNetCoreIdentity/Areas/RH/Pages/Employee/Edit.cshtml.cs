using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.EstadosMunicipios;
using AspNetCoreIdentity.Model.RH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentity.Areas.RH.Pages.Employee
{
    public class EditModel : PageModel
    {
        public SelectList Puestos { get; set; }
        public SelectList Paises { get; set; }
        public SelectList Edos { get; set; }
        public SelectList Municipios { get; set; }


        [BindProperty(SupportsGet = true)]
        public int PaisId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int EstadoId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int MunicipioId { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int EstatusId { get; set; }

        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;
        public EditModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Empleado Empleado { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empleado = _context.Empleado.FirstOrDefault(m => m.Id == id);

            if (Empleado == null)
            {
                return NotFound();
            }

            Paises = new SelectList(_context.CPais.Where(p => p.c_Pais.Contains("MEX")).ToList(), nameof(CPais.id), nameof(CPais.Descripción));
            PaisId = _context.CPais.Where(p => p.c_Pais.Equals(Empleado.Pais)).Select(p => p.id).FirstOrDefault();


            Edos = new SelectList(_context.CEstados.Where(p => p.PaisId.Equals(PaisId)).ToList(), nameof(CEstado.EstadoId), nameof(CEstado.Descripcion));
            EstadoId = _context.CEstados.Where(e => e.Descripcion.Equals(Empleado.Estado)).Select(e => e.EstadoId).FirstOrDefault();

            Municipios = new SelectList(_context.CMunicipios.Where(p => p.EstadoId.Equals(EstadoId)).ToList(), nameof(CMunicipio.MunicipioId), nameof(CMunicipio.Descripcion));
            MunicipioId = _context.CMunicipios.Where(m => m.Descripcion.Equals(Empleado.Municipio)).Select(m => m.MunicipioId).FirstOrDefault();

            EstatusId = Empleado.Estatus;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (EstatusId == 1)
            {
                Empleado.Estatus = EstatusId;
            }

            Empleado.Estado = _context.CEstados.Where(e => e.EstadoId.Equals(EstadoId)).Select(e => e.Descripcion).FirstOrDefault();
            Empleado.Municipio = _context.CMunicipios.Where(e => e.MunicipioId.Equals(MunicipioId)).Select(e => e.Descripcion).FirstOrDefault();
            Empleado.Pais = _context.CPais.Where(p => p.id.Equals(PaisId)).Select(p => p.c_Pais).FirstOrDefault();

            _context.Attach(Empleado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(Empleado.Id))
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

        private bool EmpleadoExists(int id)
        {
            return _context.Empleado.Any(e => e.Id == id);
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
