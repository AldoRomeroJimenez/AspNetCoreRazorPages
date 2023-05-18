using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Model.EstadosMunicipios
{
    public class CEstado
    {
        public CEstado()
        {
            CMunicipios = new HashSet<CMunicipio>();
        }
        [Key]
        public int EstadoId { get; set; }
        public int? ClaveEstado { get; set; }
        public string Descripcion { get; set; }
        public string Abreviacion { get; set; }
        public int PaisId { get; set; }

        public virtual ICollection<CMunicipio> CMunicipios { get; set; }
    }
}
