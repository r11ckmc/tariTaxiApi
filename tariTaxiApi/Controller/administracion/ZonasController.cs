using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Controller.administracion
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonasController : ControllerBase
    {
        [HttpPost("zonasInserta")]
        public Models.Result zonasInserta([FromBody] tariTaxiApi.Models.parametres.insertaZona parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_zona_insert", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idzona", SqlDbType.Int).Value = parametros.idzona;
                        cmd.Parameters.Add("@nombre", SqlDbType.Char).Value = parametros.nombre;
                        cmd.Parameters.Add("@estatus", SqlDbType.Int).Value = parametros.estatus;
                        cmd.Parameters.Add("@idPais", SqlDbType.Int).Value = parametros.idPais;
                        cmd.Parameters.Add("@idEntidad", SqlDbType.Int).Value = parametros.idEntidad;
                        cmd.Parameters.Add("@idRegion", SqlDbType.Int).Value = parametros.idRegion;
                        cmd.Parameters.Add("@update_uid", SqlDbType.Int).Value = parametros.update_uid;
                        cmd.Parameters.Add("@coordenadas", SqlDbType.Char).Value = parametros.coordenadas;

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
