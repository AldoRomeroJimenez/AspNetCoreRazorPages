using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Model.EstadosMunicipios
{
    public class CMunicipio
    {
        [Key]
        public int MunicipioId { get; set; }
        public int EstadoId { get; set; }
        public int ClaveMunicipio { get; set; }
        public string Descripcion { get; set; }

        public virtual CEstado Estado { get; set; }
    }
}
