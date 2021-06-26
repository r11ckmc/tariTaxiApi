using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.Models.zonasRescursos;

namespace tariTaxiApi.Models.rutas
{
    public class rutaModel
    {
        public int? idViaje { get; set; }
        public float? costoviaje { get; set; }
        public string fechaHoraServicio { get; set; }
        public List<latlngModel> origen { get; set; }
        public List<latlngModel> destino { get; set; }

    }
}
