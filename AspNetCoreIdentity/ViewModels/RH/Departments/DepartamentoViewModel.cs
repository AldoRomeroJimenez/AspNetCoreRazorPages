using AspNetCoreIdentity.Model.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.ViewModels.RH.Departments
{
    public class DepartamentoViewModel
    {
        public Departamento Departamento { get; set; }
        public string Encargado { get; set; }
    }
    public class SubDepartamentoViewModel
    {
        public SubDepartamento Departamento { get; set; }
        public string Encargado { get; set; }
    }
}
