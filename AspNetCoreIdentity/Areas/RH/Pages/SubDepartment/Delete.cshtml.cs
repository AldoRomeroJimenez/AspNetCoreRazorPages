using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.RH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentity.Areas.RH.Pages.SubDepartment
{
    public class DeleteModel : PageModel
    {
        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;
        public DeleteModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }
        public SubDepartamento SubDepartamento { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubDepartamento = await _context.SubDepartamento.FirstOrDefaultAsync(m => m.Id == id);

            if (SubDepartamento == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubDepartamento = await _context.SubDepartamento.FindAsync(id);

            if (SubDepartamento != null)
            {
                SubDepartamento.Estatus = 0;

                _context.SubDepartamento.Update(SubDepartamento);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
