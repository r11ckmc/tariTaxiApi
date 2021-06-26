using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.parametres
{
    public class VehiculosInserta
    {
        public int idMarcarVehiculo { get; set; }
        public int idTipoServicio { get; set; }
        public String linea { get; set; }
        public String NumFactura { get; set; }
        public String Modelo { get; set; }
        public String color { get; set; }
        public int cilindro { get; set; }
        public String numTarjetaC { get; set; }
        public String placa { get; set; }
        public String numMotor { get; set; }
        public String propietario { get; set; }
        public String polizaSeguro { get; set; }
        public String aseguradora { get; set; } 
        public String vigencia { get; set; }
        public String tipocobertura { get; set; }
        public String nombreAsegurado { get; set; }
        public int capacidad { get; set; }
        public int idConductor { get; set; }
        public int idUsuario { get; set; }
        public String Idioma { get; set; }
    }
}
