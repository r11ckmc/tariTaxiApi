﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.parametres
{
    public class vehiculoDoc
    {
        public int idTipoDocumento { get; set; }
        public String numeroDocumento { get; set; }
        public String Vigencia { get; set; }
        public String mimeType { get; set; }
        public String Observaciones { get; set; }
        public int idUsuario { get; set; }
        public String idioma { get; set; }



    }
}
