﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreIdentity.Model;
using AspNetCoreIdentity.Model.EstadosMunicipios;

namespace AspNetCoreIdentity.Pages.Vendors
{
    public class EditModel : PageModel
    {
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
        public Proveedor Proveedor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Proveedor = await _context.Proveedor.FirstOrDefaultAsync(m => m.Id == id);

            if (Proveedor == null)
            {
                return NotFound();
            }

            Paises = new SelectList(_context.CPais.Where(p => p.c_Pais.Contains("MEX")).ToList(), nameof(CPais.id), nameof(CPais.Descripción));
            PaisId = _context.CPais.Where(p => p.c_Pais.Equals(Proveedor.Pais)).Select(p => p.id).FirstOrDefault();


            Edos = new SelectList(_context.CEstados.Where(p => p.PaisId.Equals(PaisId)).ToList(), nameof(CEstado.EstadoId), nameof(CEstado.Descripcion));
            EstadoId = _context.CEstados.Where(e => e.Descripcion.Equals(Proveedor.Estado)).Select(e => e.EstadoId).FirstOrDefault();

            Municipios = new SelectList(_context.CMunicipios.Where(p => p.EstadoId.Equals(EstadoId)).ToList(), nameof(CMunicipio.MunicipioId), nameof(CMunicipio.Descripcion));
            MunicipioId = _context.CMunicipios.Where(m => m.Descripcion.Equals(Proveedor.Municipio)).Select(m => m.MunicipioId).FirstOrDefault();

            EstatusId = Proveedor.Estatus;

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
            if (EstatusId == 1)
            {
                Proveedor.Estatus = EstatusId;
            }

            Proveedor.Estado = _context.CEstados.Where(e => e.EstadoId.Equals(EstadoId)).Select(e => e.Descripcion).FirstOrDefault();
            Proveedor.Municipio = _context.CMunicipios.Where(e => e.MunicipioId.Equals(MunicipioId)).Select(e => e.Descripcion).FirstOrDefault();
            Proveedor.Pais = _context.CPais.Where(p => p.id.Equals(PaisId)).Select(p => p.c_Pais).FirstOrDefault();

            _context.Attach(Proveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(Proveedor.Id))
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

        private bool ProveedorExists(int id)
        {
            return _context.Proveedor.Any(e => e.Id == id);
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
