using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.servicio
{
    public class tipoServicio
    {
        public int? idTipoServicio { get; set; }
        public String descripcion { get; set; }
        public String icono { get; set; }
        public double? costoInicial { get; set; }

    }
}
