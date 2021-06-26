using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.Models.C9CancelaciondeViaje;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.C9CancelaciondeViaje
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancelacionViajeController : ControllerBase
    {
        [HttpPost("motivosDeCancelaciones")]
        public Models.Result motivosDeCancelaciones([FromBody] tariTaxiApi.Models.parametres.MotivosCancelaciones parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_motivosDeCancelaciones_consulta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@esParaPasajero", SqlDbType.Int).Value = parametros.esParaPasajero;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@idioma", SqlDbType.Char).Value = parametros.idioma;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<motivosDeCancelacionesConsultaModel> list =
                                new List<motivosDeCancelacionesConsultaModel>();
                            while (r.Read())
                            {




                                list.Add(new motivosDeCancelacionesConsultaModel()
                                {
                                    espanol = r["espanol"].ToString(),
                                    cuotaPenalizacion = verificaNullable.verificarDouble(r["cuotaPenalizacion"].ToString()),


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
        
        [HttpPost("viajeCancelaciones")]
        public Models.Result viajeCancelaciones([FromBody] tariTaxiApi.Models.parametres.ViajeCancelaciones parametros)
        {

            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_viajeCancelaciones_insertar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idViaje", SqlDbType.Int).Value = parametros.idViaje;
                        cmd.Parameters.Add("@idMotivoCancelacion", SqlDbType.Int).Value = parametros.idMotivoCancelacion;
                        cmd.Parameters.Add("@quienCancela", SqlDbType.Int).Value = parametros.quienCancela;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@idioma", SqlDbType.Char).Value = parametros.idioma;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            
                            while (r.Read())
                            {
                                var resultado = new
                                {
                                    idViajeCancelacion =
                                        verificaNullable.verificarInt(r["idViajeCancelacion"].ToString()),
                                        Result="insert successfull"
                                };
                                result.objectResult = resultado;
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
