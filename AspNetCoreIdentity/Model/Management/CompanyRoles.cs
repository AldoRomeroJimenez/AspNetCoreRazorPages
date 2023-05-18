using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Model.Management
{
    public class CompanyRoles : IdentityRole
    {
        public CompanyRoles(string name)
        : base(name)
        { 
        }

        public CompanyRoles()
        { 
        }
        public int Estatus { get; set; }
        public string Description { get; set; }
    }
}
