using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.parametres
{
    public class ConductorInsert
    {
        public String nombre { get; set; }
        public String apellidos { get; set; }
        public String genero { get; set; }
        public String fechaNac { get; set; }
        public int idPaisTelefono { get; set; }
        public String Telefono { get; set; }
        public String telefonoFijo { get; set; }
        public String Direccion { get; set; }
        public String email { get; set; }
        public String contrasena { get; set; }
        public int idiomaPreferido { get; set; }

        [Range(0,1,ErrorMessage =" solo acepta 0 y 1")]
        public int poseeLicencia { get; set; }
        public String vigenciaL { get; set; }
        public int tipoL { get; set; }

    }
}
