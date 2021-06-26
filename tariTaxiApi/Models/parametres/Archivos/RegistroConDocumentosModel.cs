using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.parametres.Archivos
{
   
    public class RegistroConDocumentosModel
    {
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string contrasena { get; set; }
        public string genero { get; set; }
        public int idPaisTelefono { get; set; }
        public string Telefono { get; set; }
        public bool habilitado { get; set; }
        public bool recuperarClave { get; set; }
        public int idiomaPreferido { get; set; }
        public int idrol { get; set; }
        public string foto { get; set; }
    }
}
