using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AspNetCoreIdentity.Model;

namespace AspNetCoreIdentity.Pages.Management.Company
{
    public class IndexModel : PageModel
    {
        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;

        public IndexModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }

        public IList<Empresa> Empresa { get;set; }

        public async Task OnGetAsync()
        {
            Empresa = await _context.Empresa.Where(e=> e.Estatus.Equals(1)).ToListAsync();
        }
    }
}
