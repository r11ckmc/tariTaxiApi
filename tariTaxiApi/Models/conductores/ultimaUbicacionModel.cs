using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.Models.zonasRescursos;

namespace tariTaxiApi.Models.conductores
{
    public class ultimaUbicacionModel
    {

            public int? id { get; set; }
            public string habilitado { get; set; }
            public string activo { get; set; }
            public double? calificacionPromedio { get; set; }
            public string nombre { get; set; }
            public string apelldios { get; set; }
            public string telefono { get; set; }
            public List<latlngModel> coordenadas { get; set; }

    
    }
}
