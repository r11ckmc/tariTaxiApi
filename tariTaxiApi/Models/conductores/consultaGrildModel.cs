using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.conductores
{
    public class consultaGrildModel
    {
        public int? idViaje { set; get; }
        public String Servicio { set; get; }
        public double? Importe { set; get; }
        public int? Taxista { set; get; }
        public double? Transferencia { set; get; }
        public double? ComisionBancaria { set; get; }
        public String Nombre { set; get; }
        public String Gafete { set; get; }


    }
}
