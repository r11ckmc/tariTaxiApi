using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Controller.P17_7PoliticadePrivacidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class politicasController : ControllerBase
    {
        [HttpPost("politicasConsulta")]
        public Models.Result politicasConsulta([FromBody] tariTaxiApi.Models.parametres.usuarioModel parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_politicas_consulta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@idioma", SqlDbType.Char).Value = parametros.idioma;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                if (parametros.idioma.Equals("esp"))
                                {
                                    var resultado = new
                                    {
                                        ruta = r["espanol"].ToString()
                                    };
                                    result.objectResult = resultado;
                                }
                                else
                                {
                                    var resultado = new
                                    {
                                        ruta = r["ingles"].ToString()
                                    };
                                    result.objectResult = resultado;
                                }


                            }


                        }

                    }
                    con.Close();
                }
                result.Status = 1;
                result.Message = "OK";


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
