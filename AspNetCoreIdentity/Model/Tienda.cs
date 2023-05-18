using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Model
{
    public class Tienda
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Contacto { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int CodigoPostal { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Pais { get; set; }
        [Required]

        public string Telefono1 { get; set; }
        [Required]
        public string Telefono2 { get; set; }
        [Required]
        public string Almacen { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public int Estatus { get; set; }
        [Required]
        public string RazonSocial { get; set; }
    }
}