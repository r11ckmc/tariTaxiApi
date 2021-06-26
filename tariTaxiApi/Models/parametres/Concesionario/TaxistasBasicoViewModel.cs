using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.parametres.Concesionario
{
    public class TaxistasBasicoViewModel
    {
        public String Nombre { get; set; }
        public String nroGafete { get; set; }
        public String Apellidos { get; set; }
        public String RFC { get; set; }
        public String CURP { get; set; }
        public String correoElectronico { get; set; }
        public String fechaNacimiento { get; set; }
        public String Direccion { get; set; }
        public String Telefono { get; set; }
        public String familiarWhatsApp { get; set; }
        public String familiar { get; set; }
        public int idGrupoSanguineo { get; set; }
        public String Genero { get; set; }
        public String alergias { get; set; }
        public String Observaciones { get; set; }
        public String NOTaxi { get; set; }
        public int marca { get; set; }
 
        public String modelo { get; set; }

        public int TipoServicio { get; set; }
        public String Placa { get; set; }
        public int Habilitado { get; set; }
    }
}
