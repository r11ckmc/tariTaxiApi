using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.Models.zonasRescursos;

namespace tariTaxiApi.Models.zonas
{
    public class zonasmapaModel
    {
        public int? idZona { get; set; }
        public String nombre { get; set; }

        public List<latlngModel> coordenadas { get; set; }
    }
}
