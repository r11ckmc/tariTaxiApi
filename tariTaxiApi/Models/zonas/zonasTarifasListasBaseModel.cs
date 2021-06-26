using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.zonas
{
    public class zonasTarifasListasBaseModel
    {
        public String origen { get; set; }
        public String destino { get; set; }
        public String servicio { get; set; }
        public double? importe { get; set; }
        public double? importeConAumento { get; set; }

    }
}
