using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AspNetCoreIdentity.Model;

namespace AspNetCoreIdentity.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;

        public IndexModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }

        public IList<Producto> Producto { get;set; }

        public async Task OnGetAsync()
        {
            Producto = await _context.Producto.ToListAsync();
        }
    }
}
