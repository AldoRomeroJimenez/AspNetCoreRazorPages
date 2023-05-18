using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Model.RH
{
    public class Empleado
    {
        
        [Key]
        public int Id { get; set; }
        public int Nomina { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; }
        [Required]
        [Display(Name = "Apellido Materno")]
        public string ApellidoMaterno { get; set; }
        [Required]
        public string FechaNacimiento { get; set; }
        [Required]
        [MaxLength(14, ErrorMessage = "Campo con 14 caracteres como maximo")]
        [MinLength(12, ErrorMessage = "Campo con 12 caracteres como minimo")]
        public string RFC { get; set; }
        [Required]
        [MaxLength(18, ErrorMessage = "Campo con 18 caracteres como maximo")]
        [MinLength(18, ErrorMessage = "Campo con 18 caracteres como minimo")]
        public string CURP { get; set; }
        [Required]
        [MaxLength(15,ErrorMessage ="Maximo de caracteres (15)")]
        public string Telefono { get; set; }

        [Required]
        public int NSS { get; set; }
        public string FechaIngreso { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="El formato debe ser de correo electronico")]
        [Display(Name ="Correo Personal")]
        public string CorreoPersonal { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "El formato debe ser de correo electronico")]
        [Display(Name = "Correo Institucional")]
        public string CorreoInstitucion { get; set; }
        public int PuestoId { get; set; }

        public string Direccion { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string CodigoPostal { get; set; }
        public int Estatus { get; set; }

    }
}
