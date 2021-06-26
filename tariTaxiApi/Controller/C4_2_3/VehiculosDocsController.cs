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

namespace tariTaxiApi.Controller.C4_2_3
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosDocsController : ControllerBase
    {
        [HttpPost("vehiculosDocumentosInserta")]
        public Models.Result vehiculosDocumentosInserta([FromBody] tariTaxiApi.Models.parametres.vehiculoDoc parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_vehiculosDocumentos_inserta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idTipoDocumento", SqlDbType.Int).Value = parametros.idTipoDocumento;
                        cmd.Parameters.Add("@numeroDocumento", SqlDbType.Char).Value = parametros.numeroDocumento;
                        cmd.Parameters.Add("@Vigencia", SqlDbType.Char).Value = parametros.Vigencia;
                        cmd.Parameters.Add("@mimeType", SqlDbType.Char).Value = parametros.mimeType;
                        cmd.Parameters.Add("@Observaciones", SqlDbType.Char).Value = parametros.Observaciones;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@idioma", SqlDbType.Char).Value = parametros.idioma;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
                            {


                                if (r.GetName(0) == "codigo")
                                {
                                    List<ErrorStatus> list =
                                    new List<ErrorStatus>();
                                    list.Add(new ErrorStatus()
                                    {
                                        codigo = verificaNullable.verificarInt(r["codigo"].ToString()),
                                        mensaje = r["mensaje"].ToString(),


                                    });

                                    result.objectResult = list;
                                }

                                else
                                {
                                    var res =new { ruta = r["ruta"].ToString()};
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
        
        [HttpPost("vehiculosSegurosInserta")]
        public Models.Result vehiculosSegurosInserta([FromBody] tariTaxiApi.Models.parametres.seguroVehiculo parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_vehiculosSeguros_inserta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idVehiculo", SqlDbType.Int).Value = parametros.idVehiculo;
                        cmd.Parameters.Add("@numeroPoliza", SqlDbType.Char).Value = parametros.numeroPoliza;
                        cmd.Parameters.Add("@vigenciaPoliza", SqlDbType.Char).Value = parametros.vigenciaPoliza;
                        cmd.Parameters.Add("@tipoCobertura", SqlDbType.Char).Value = parametros.tipoCobertura;
                        cmd.Parameters.Add("@nombreAsegurado", SqlDbType.Char).Value = parametros.nombreAsegurado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@idioma", SqlDbType.Char).Value = parametros.idioma;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
                            {


                                if (r.GetName(0) == "codigo")
                                {
                                    List<ErrorStatus> list =
                                    new List<ErrorStatus>();
                                    list.Add(new ErrorStatus()
                                    {
                                        codigo = verificaNullable.verificarInt(r["codigo"].ToString()),
                                        mensaje = r["mensaje"].ToString(),


                                    });

                                    result.objectResult = list;
                                }

                                else
                                {
                                    var res = new { ruta = verificaNullable.verificarInt(r["idRegistro"].ToString()) };
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
