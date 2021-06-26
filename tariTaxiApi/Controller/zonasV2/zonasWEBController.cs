using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Controller.zonasV2
{
    [Route("api/[controller]")]
    [ApiController]
    public class zonasWEBController : ControllerBase
    {
        [HttpPost("modificaZonas")]
        public Models.Result modificaZonas([FromBody] tariTaxiApi.Models.parametres.zonasAddModel parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_zonas_actualizar_Temp", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idzona", SqlDbType.Int).Value = parametros.idzona;
                        cmd.Parameters.Add("@nombre", SqlDbType.Char).Value = parametros.nombre;
                        cmd.Parameters.Add("@ubicacion", SqlDbType.Char).Value = parametros.ubicacion;

                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                    result.Status = 1;
                    result.Message = "OK";

                }
            }
            catch (Exception e)
            {
                result.Status = 0;
                result.Message = e.Message;
            }
            return result;
        }

    }
}
