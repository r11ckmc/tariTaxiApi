using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.parametres
{
    [Route("api/[controller]")]
    [ApiController]
    public class insertaZona : ControllerBase
    {
        public int idzona { get; set; }
        public String nombre { get; set; }
        public int estatus { get; set; }
        public int idPais { get; set; }
        public int idEntidad { get; set; }
        public int idRegion { get; set; }
        public int update_uid { get; set; }
        public String coordenadas { get; set; }


    }
}
