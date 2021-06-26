using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.parametres
{
    public class seguroVehiculo
    {
        public int idVehiculo { get; set; }
        public String numeroPoliza { get; set; }
        public String vigenciaPoliza { get; set; }
        public String tipoCobertura { get; set; }
        public String nombreAsegurado { get; set; }
        public int idUsuario { get; set; }
        public String idioma { get; set; }
    }
}
