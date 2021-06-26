using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.parametres
{
    public class ViajeCancelaciones
    {
        public int idViaje { get; set; }
        public int idMotivoCancelacion { get; set; }
        public int quienCancela { get; set; }
        public int idUsuario { get; set; }
        public String idioma { get; set; }

    }
}
