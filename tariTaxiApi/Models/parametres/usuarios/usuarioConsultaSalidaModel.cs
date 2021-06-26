using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.parametres.usuarios
{
    public class usuarioConsultaSalidaModel
    {
        public int? idUsuario { get; set; }
        public String Nombre { get; set; }
        public String correoElectronico { get; set; }
        public int? habilitado { get; set; }
        public String Roles { get; set; }
        public String Regiones { get; set; }
        public String ultimoAcceso { get; set; }
    }
}
