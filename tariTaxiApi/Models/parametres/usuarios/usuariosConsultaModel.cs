using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.parametres.usuarios
{
    public class usuariosConsultaModel
    {
        public int idUsuario { get; set; }
        public String nombre { get; set; }
        public String correoElectronico { get; set; }
        public int habilitado { get; set; }
        public int idRol { get; set; }
        public int idRegion { get; set; }
        public int idUsuarioSesion { get; set; }
         public String Idioma {get; set;}

    }
}
