using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Controller.C12_1Perfil
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductorController : ControllerBase
    {
        [HttpPost("vehiculosActualiza")]
        public Models.Result vehiculosActualiza([FromBody] tariTaxiApi.Models.parametres.Conductor parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_conductor_actualiza", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@genero", SqlDbType.Char).Value = parametros.genero;
                        cmd.Parameters.Add("@Telefono", SqlDbType.Char).Value = parametros.Telefono;
                        cmd.Parameters.Add("@email", SqlDbType.Char).Value = parametros.email;
                        cmd.Parameters.Add("@contrasena", SqlDbType.Char).Value = parametros.contrasena;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@Idioma", SqlDbType.Char).Value = parametros.Idioma;


                        {
                            cmd.ExecuteNonQuery();
                            Models.ResultModify res = new Models.ResultModify
                            {
                                IdModify = parametros.idUsuario.ToString(),
                                Result = "update successfull"
                            };
                            result.objectResult = res;
                        }
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
