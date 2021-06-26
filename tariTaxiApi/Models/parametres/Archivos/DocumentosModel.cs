using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.parametres.Archivos
{
    public class DocumentosModel
    {
        public int idTipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string Vigencia { get; set; }
        public IFormFile documento { get; set; }
        public string Observaciones { get; set; }
        public int idUsuario { get; set; }
        public string Idioma { get; set; }
    }

}
