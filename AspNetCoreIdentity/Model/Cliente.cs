using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Model
{
    public class Cliente
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [MinLength(12)]
        [MaxLength(14)]
        public string RFC { get; set; }        
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int CodigoPostal { get; set; }        
        public string Estado { get; set; }        
        public string Municipio { get; set; }        
        public string Pais { get; set; }
        [Required]
        [MaxLength(15)]
        public string Telefono1 { get; set; }
        [Required]
        [MaxLength(15)]
        public string Telefono2 { get; set; }        
        [Required]
        public int Estatus { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
