using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspNetCoreIdentity.Model.RH;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentity.Areas.RH.Pages.Employee
{
    public class IndexModel : PageModel
    {
        private readonly AspNetCoreIdentity.Model.AccountDbContext _context;
        public IndexModel(AspNetCoreIdentity.Model.AccountDbContext context)
        {
            _context = context;
        }
        public IList<Empleado> Empleado { get; set; }
        public async Task OnGetAsync()
        {
            Empleado = await _context.Empleado.ToListAsync();
        }
    }
}
