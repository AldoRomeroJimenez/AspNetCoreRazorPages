using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Model.EstadosMunicipios
{
    public class CPais
    {
		public int id { get; set; }
		public string c_Pais { get; set; }
		public string Descripción { get; set; }
		public string Formato_de_código_postal { get; set; }
		public string Formato_de_Registro_de_Identidad_Tributaria { get; set; }
		public string Validación_del_Registro_de_Identidad_Tributaria { get; set; }
		public string Agrupaciones { get; set; }		
	}
}
