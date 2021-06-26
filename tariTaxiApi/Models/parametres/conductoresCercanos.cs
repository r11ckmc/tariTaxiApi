using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.parametres
{
    public class conductoresCercanos
    {
        public int idtipoServicio { get; set; }
        public int idUsuario { get; set; }
        public String latPasajero { get; set; }
        public String lonPasajero { get; set; }
        public int idZonaOrigen { get; set; }
        public int idZonaDestino { get; set; }

    }
}
