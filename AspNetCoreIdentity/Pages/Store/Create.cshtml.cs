using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetCoreIdentity.Model;

namespace AspNetCoreIdentity.Pages.Store
{
    public class CreateModel : PageModel
    {
        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;

        public CreateModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Tienda Tienda { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tienda.Add(Tienda);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
