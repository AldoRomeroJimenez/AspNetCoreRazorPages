using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Model.RH
{
    public class Departamento
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El Campo Nombre es necesario")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "El Campo Descripcion es necesario")]
        public string Descripcion { get; set; }
        
        [Required(ErrorMessage ="El Campo Clave Departamento es necesario")]
        [Display(Name = "Clave Departamento")]
        public string ClaveDepartamento { get; set; }

        [Display(Name = "Encargado")]
        public string EmpleadoId { get; set; }
        public string Fecha { get; set; }
        public List<Empleado> Encargado { get; set; }
        public int Estatus { get; set; }

    }
    public class SubDepartamento
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int DepartamentoPadreId { get; set; }
        
        [Required(ErrorMessage = "El Campo Nombre es necesario")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "El Campo Descripcion es necesario")]
        public string Descripcion { get; set; }
        
        [Required(ErrorMessage = "El Campo Clave Sub es necesario")]
        [Display(Name = "Clave Sub Departamento")]
        public string ClaveSubDepartamento { get; set; }

        [Display(Name = "Encargado")]
        public string EmpleadoId { get; set; }
        public string Fecha { get; set; }        
        public int Estatus { get; set; }
    }
}
