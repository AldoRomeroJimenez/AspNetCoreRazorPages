using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Model;
using AspNetCoreIdentity.Model.Management;
using AspNetCoreIdentity.Model.EstadosMunicipios;
using AspNetCoreIdentity.Model.RH;

namespace AspNetCoreIdentity.Model
{
    public class AccountDbContext : IdentityDbContext<IdentityCompanyUser>
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {

        }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Tienda> Tienda { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<CompanyRoles> CompanyRol { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<CPais> CPais { get; set; }
        public virtual DbSet<CEstado> CEstados { get; set; }
        public virtual DbSet<CMunicipio> CMunicipios { get; set; }

        //RH
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<SubDepartamento> SubDepartamento { get; set; }
        public virtual DbSet<Puesto> Puesto { get; set; }

    }
}
