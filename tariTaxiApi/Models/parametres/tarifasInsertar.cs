using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.parametres
{
    public class tarifasInsertar
    {
        public int idOrigen { get; set; }
        public int idDestino { get; set; }
        public float importe { get; set; }
        public int tipoServicio { get; set; }
    
    }
}
