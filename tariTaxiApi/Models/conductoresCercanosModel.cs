using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models
{
    public class conductoresCercanosModel
    {
        public int? idConductor { get; set; }
        public int? idVehiculo { get; set; }
        public int? idUsuario { get; set; }
        public Boolean? Habilitado { get; set; }
        public string foto { get; set; }
        public string nombre { get; set; }
        public string Telefono { get; set; }
        public double? calificacionPromedio { get; set; }
        public string linea { get; set; }
        public String Modelo { get; set; }
        public String Placa { get; set; }
        public String Descripcion { get; set; }
        public String Servicio { get; set; }
        public double? costoinicial { get; set; }
        public String UbicacionConductor { get; set; }



    }
}
