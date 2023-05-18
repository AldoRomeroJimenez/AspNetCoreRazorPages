using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Model.Management
{
    public class IdentityCompanyUser:IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Estatus { get; set; }
    }
}
