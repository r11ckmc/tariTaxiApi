using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.C10NuevaSolicitud
{
    public class notificacionEntranteConsultaModel
    {
        public String ruta { get; set; }
        public int? idViajes { get; set; }
        public int? idUsuario { get; set; }
        public String puntoOrigen { get; set; }
        public int? idTipoServicio { get; set; }
        public Double? calificacionPromedio { get; set; }
        public String nombre { get; set; }
        public String apellidos { get; set; }
        public String Telefono { get; set; }
        public float? LatitudPasajeroOrigen { get; set; }
        public float? LongitudPasajeroOrigen { get; set; }
        public float? LatitudPasajeroDestino { get; set; }
        public float? LongitudPasajeroDestino { get; set; }
        public float? distancia { get; set; }
        public String tiempo { get; set; }
   

    }
}
