using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.zonas.region
{
    public class regionZonaModel
    {
        public int? idRegion { get; set; }
        public int? idEntidad { get; set; }
        public string descripcion { get; set; }
        public String habilitado { get; set; }
    }
}
