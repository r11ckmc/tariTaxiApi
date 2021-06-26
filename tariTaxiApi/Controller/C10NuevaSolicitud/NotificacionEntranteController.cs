using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.Models.C10NuevaSolicitud;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.C10NuevaSolicitud
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionEntranteController : ControllerBase
    {
        [HttpPost("notificacionEntranteConsulta")]
        public Models.Result notificacionEntranteConsulta([FromBody] tariTaxiApi.Models.parametres.UbicacionConductor parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_notificacionEntrante_Consulta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idConductor", SqlDbType.Int).Value = parametros.idConductor;
                        cmd.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = parametros.TipoServicio;
                        cmd.Parameters.Add("@ubicacionConductor", SqlDbType.Char).Value = parametros.ubicacionConductor;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<notificacionEntranteConsultaModel> list =
                                new List<notificacionEntranteConsultaModel>();
                            while (r.Read())
                            {




                                list.Add(new notificacionEntranteConsultaModel()
                                {
                                    ruta = r["ruta"].ToString(),
                                    idViajes = verificaNullable.verificarInt(r["idViajes"].ToString()),
                                    idUsuario = verificaNullable.verificarInt(r["idUsuario"].ToString()),
                                    puntoOrigen = r["puntoOrigen"].ToString(),
                                    idTipoServicio = verificaNullable.verificarInt(r["idTipoServicio"].ToString()),
                                    calificacionPromedio = verificaNullable.verificarDouble(r["calificacionPromedio"].ToString()),
                                    nombre = r["nombre"].ToString(),
                                    apellidos = r["apellidos"].ToString(),
                                    Telefono = r["Telefono"].ToString(),
                                    LatitudPasajeroOrigen = verificaNullable.verificarFloat(r["LatitudPasajeroOrigen"].ToString()),
                                    LongitudPasajeroOrigen = verificaNullable.verificarFloat(r["LongitudPasajeroOrigen"].ToString()),
                                    LatitudPasajeroDestino = verificaNullable.verificarFloat(r["LatitudPasajeroDestino"].ToString()),
                                    LongitudPasajeroDestino = verificaNullable.verificarFloat(r["LongitudPasajeroDestino"].ToString()),
                                    distancia = verificaNullable.verificarFloat(r["distancia"].ToString()),
                                    tiempo = r["tiempo"].ToString(),


                                });


                            }

                            result.objectResult = list;
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
