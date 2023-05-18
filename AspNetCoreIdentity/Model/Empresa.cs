using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Model
{
    public class Empresa
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string RazonSocial { get; set; }

        [Required]
        [MaxLength(14)]
        [MinLength(12)]
        public string RFC { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int CodigoPostal { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Pais { get; set; }
        [Required]
        public string FechaCreacion { get; set; }
        [Required]
        public string Moneda { get; set; }
        [Required]
        public string Curp { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string FechaNacimiento { get; set; }
        public string CertificadoSAT { get; set; }
        public string KeySAT { get; set; }
        public string ClavePrivadaSAT { get; set; }
        public int Estatus { get; set; }
    }
}
