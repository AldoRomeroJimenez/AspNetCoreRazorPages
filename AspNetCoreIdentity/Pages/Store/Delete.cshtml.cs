using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AspNetCoreIdentity.Model;

namespace AspNetCoreIdentity.Pages.Store
{
    public class DeleteModel : PageModel
    {
        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;

        public DeleteModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tienda Tienda { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tienda = await _context.Tienda.FirstOrDefaultAsync(m => m.Id == id);

            if (Tienda == null)
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

            Tienda = await _context.Tienda.FindAsync(id);

            if (Tienda != null)
            {
                _context.Tienda.Remove(Tienda);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
