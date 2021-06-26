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
using tariTaxiApi.Models.P17._6._1Objetosolvidados;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.P17._6._1Objetosolvidados
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetosOlvidadosController : ControllerBase
    {
        [HttpPost("inserta")]
        public Models.Result inserta([FromBody] tariTaxiApi.Models.parametres.ObjetosOlvidados parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_viajesObjetosOlvidados_inserta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idViaje", SqlDbType.Int).Value = parametros.idViaje;
                        cmd.Parameters.Add("@descripcion", SqlDbType.Char).Value = parametros.descripcion;
                        cmd.Parameters.Add("@numeroContanto", SqlDbType.Char).Value = parametros.numeroContanto;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = parametros.id_usuario;
                        cmd.Parameters.Add("@id_idioma", SqlDbType.Char).Value = parametros.id_idioma;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<ErrorStatus> list =
                                new List<ErrorStatus>();
                            while (r.Read())
                            {

                                if (r.GetName(0) == "codigo")
                                {

                                    list.Add(new ErrorStatus()
                                    {
                                        codigo = verificaNullable.verificarInt(r["codigo"].ToString()),
                                        mensaje = r["mensaje"].ToString(),


                                    });
                                    result.objectResult = list;
                                }
                                else
                                {
                                    var res = new { idObjetoOlvidado = verificaNullable.verificarInt(r["idObjetoOlvidado"].ToString()) };
                                    result.objectResult = res;
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

        [HttpPost("viajesConsulta")]
        public Models.Result viajesConsulta([FromBody] tariTaxiApi.Models.parametres.usuarioModel parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_viajes_Consulta_10", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@idioma", SqlDbType.Char).Value = parametros.idioma;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<viajeConsultaModel> list =
                                new List<viajeConsultaModel>();
                            while (r.Read())
                            {

                                if (r.GetName(0) == "idEstatusViaje")
                                {
                                    list.Add(new viajeConsultaModel()
                                    {
                                        idEstatusViaje = verificaNullable.verificarInt(r["idEstatusViaje"].ToString()),
                                        fechaHoraServicio = r["fechaHoraServicio"].ToString(),
                                        Latitud = r["Latitud"].ToString(),
                                        Longitud = r["Longitud"].ToString()

                                    });
                                    result.objectResult = list;

                                }
                                else
                                {
                                    List<ErrorStatus> res = new List<ErrorStatus>();
                                    res.Add(
                                        new ErrorStatus()
                                        {
                                            codigo = verificaNullable.verificarInt(r["codigo"].ToString()),
                                            mensaje = r["mensaje"].ToString()
                                        }
                                        );
                                    result.objectResult = res;

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
