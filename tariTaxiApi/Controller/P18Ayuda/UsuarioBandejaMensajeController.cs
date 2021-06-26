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
using tariTaxiApi.Models.P18Ayuda;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.P18Ayuda
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioBandejaMensajeController : ControllerBase
    {
        [HttpPost("inserta")]
        public Models.Result inserta([FromBody] tariTaxiApi.Models.parametres.UsuarioBandeja parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_usuariosBandejaMensajes_inserta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@Idioma", SqlDbType.Char).Value = parametros.Idioma;
                        cmd.Parameters.Add("@texto", SqlDbType.Char).Value = parametros.texto;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
                            {
                                String test = r.GetName(0).ToString();
                                if (r.GetName(0) == "Fecha")
                                {
                                    List<usuariosBandejaMensajesModel> list =
                                     new List<usuariosBandejaMensajesModel>();
                                    list.Add(new usuariosBandejaMensajesModel()
                                    {
                                        fecha = r["fecha"].ToString(),
                                        mensaje = r["mensaje"].ToString()


                                    });
                                    result.objectResult = list;

                                }
                                else
                                {
                                    List<ErrorStatus> list =
                                    new List<ErrorStatus>();
                                    list.Add(new ErrorStatus()
                                    {
                                        codigo = verificaNullable.verificarInt(r["codigo"].ToString()),
                                        mensaje = r["mensaje"].ToString()


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
