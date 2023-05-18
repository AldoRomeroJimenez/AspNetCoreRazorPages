using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AspNetCoreIdentity.Model;

namespace AspNetCoreIdentity.Pages.Costumers
{
    public class IndexModel : PageModel
    {
        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;

        public IndexModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }

        public IList<Cliente> Cliente { get;set; }

        public async Task OnGetAsync()
        {
            Cliente = await _context.Cliente.ToListAsync();
        }
    }
}
