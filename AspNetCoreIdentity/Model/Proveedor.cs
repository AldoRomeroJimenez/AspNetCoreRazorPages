using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Model
{
    public class Proveedor
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string RFC { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }        
        public string Estado { get; set; }
        public string Municipio { get; set; }
        [Required]
        public int Cp { get; set; }        
        public string Pais { get; set; }
        [Required]
        public string Contacto { get; set; }
        
        public int Estatus { get; set; }
    }
}