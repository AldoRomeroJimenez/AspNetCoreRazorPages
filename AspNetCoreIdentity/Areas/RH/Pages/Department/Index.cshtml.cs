using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model.RH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentity.Areas.RH.Pages.Department
{
    public class IndexModel : PageModel
    {
        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;
        public IndexModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }
        public IList<Departamento> Departamentos { get; set; }
        public async Task OnGetAsync()
        {
            Departamentos = await _context.Departamento.ToListAsync();
        }
    }
}
