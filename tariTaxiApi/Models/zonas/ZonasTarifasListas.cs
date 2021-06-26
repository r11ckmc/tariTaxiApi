using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.zonas
{
    public class ZonasTarifasListas
    {
        public String origen { get; set; }
        public String destino { get; set; }
        public String servicio { get; set; }
        public double? importe { get; set; }

    }
}
