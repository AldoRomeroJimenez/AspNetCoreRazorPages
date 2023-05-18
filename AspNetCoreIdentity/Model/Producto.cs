using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Model
{
    public class Producto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string UM { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string CveProdServSAT { get; set; }
        [Required]
        public string CveUnidadSAT { get; set; }
        [Required]
        public string ProveedorId { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Costo { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Margen { get; set; }
        [Required]
        public string Codebar { get; set; }
        [Required]
        public string Contenido { get; set; }
        [Required]
        public int Estatus { get; set; }

    }
}


