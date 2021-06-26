using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.Models.error;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.C2_1Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class conductorLogInController : ControllerBase
    {
        [HttpPost("conductorIniciaSeccion")]
        public Models.Result conductorIniciaSeccion([FromBody] tariTaxiApi.Models.parametres.ConductorLogIn parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_conductor_iniciaSeccion", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@tipoConexion", SqlDbType.Char).Value = "local";
                        cmd.Parameters.Add("@email", SqlDbType.Char).Value = parametros.email;
                        cmd.Parameters.Add("@pass", SqlDbType.Char).Value = parametros.pass;
                        cmd.Parameters.Add("@ubicacionGPS", SqlDbType.Char).Value = parametros.ubicacionGPS;
                        cmd.Parameters.Add("@ipOrigen", SqlDbType.Char).Value = parametros.ipOrigen;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                                List<ErrorStatus> list = new List<ErrorStatus>();
                                while (r.Read())
                                 {
                                String firstcol = r.GetName(0);

                               if (firstcol == "codigo")
                                {
                                    list.Add(new ErrorStatus()
                                        {
                                            codigo = verificaNullable.verificarInt(r["codigo"].ToString()),
                                            mensaje = r["mensaje"].ToString(),

                                        });
                                    result.objectResult = list;

                                }

                            }


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
