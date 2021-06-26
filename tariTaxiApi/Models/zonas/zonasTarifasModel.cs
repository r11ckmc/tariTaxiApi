using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.zonas
{
    public class zonasTarifasModel
    {
        public double? importe { get; set;}
        public int? idTipoServicio { get; set; }

        public String TipoServicio { get; set; }
        public int? idZonaOrigen { get; set; }
        public int? idZonaDestino { get; set; }


    }
}
