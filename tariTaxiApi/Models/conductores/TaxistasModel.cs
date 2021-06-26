using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.conductores
{
    public class TaxistasModel
    {
        public int? idUsuario { get; set; }
        public String? nombre { get; set; }
        public String? apellido { get; set; }
        public String? genero { get; set; }
        public String? rfc { get; set; }
        public String? curp { get; set; }
        public String? email { get; set; }
        public String? fechaNacimiento { get; set; }
        public String? direccion { get; set; }
        public String? Telefono { get; set; }
        public String? familiar { get; set; }
        public String? familiarWhatsApp { get; set; }
        public String? Observaciones { get; set; }
        public String? alergias { get; set; }
        public String? numeroTaxi { get; set; }
        public String? nroGafete { get; set; }
        public int? marca { get; set; }
        public String? modelo { get; set; }
        public int? tipoServicio { get; set; }
        public String? placa { get; set; }
        public int? Habilitado { get; set; }



    }
}
