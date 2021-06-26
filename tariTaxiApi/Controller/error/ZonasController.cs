using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.Models.zonas;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.error
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonasController : ControllerBase
    {
        [HttpPost("insertarErrorzona")]
        public Models.Result insertarErrorzona([FromBody] tariTaxiApi.Models.parametres.errorzona parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_insertar_error_zona", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idusuario", SqlDbType.Int).Value = parametros.idusuario;
                        cmd.Parameters.Add("@coordenada", SqlDbType.Char).Value = parametros.coordenada;
                        cmd.Parameters.Add("@titulo", SqlDbType.Char).Value = parametros.titulo;
                        cmd.Parameters.Add("@errorMensaje", SqlDbType.Char).Value = parametros.errorMensaje;

                        cmd.ExecuteNonQuery();
                        var res = new { mensaje = "dato insertado satisfactoriamente" };
                        result.objectResult = res;
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

        [HttpPost("listasTarifas")]

        public Models.Result listasTarifas([FromBody] tariTaxiApi.Models.parametres.Viaje parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_zonas_tarifas_listas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idZonaOrigen", SqlDbType.Int).Value = parametros.idViaje;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<ZonasTarifasListas> resultado = new List<ZonasTarifasListas>();
                            while (r.Read())
                            {
                                resultado.Add(new ZonasTarifasListas()
                                {
                                    origen = r["origen"].ToString(),
                                    destino = r["destino"].ToString(),
                                    servicio = r["servicio"].ToString(),
                                    importe = verificaNullable.verificarDouble(r["importe"].ToString())

                                });
                            }
                            result.objectResult = resultado;

                            con.Close();
                        }
                        result.Status = 1;
                        result.Message = "OK";
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = 0;
                result.Message = e.Message;
            }

            return result;
        }


        [HttpPost("listasTarifasBase")]

        public Models.Result listasTarifasBase([FromBody] tariTaxiApi.Models.parametres.Viaje parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_zonas_tarifas_importeOriginal_listas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idZonaOrigen", SqlDbType.Int).Value = parametros.idViaje;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<zonasTarifasListasBaseModel> resultado = new List<zonasTarifasListasBaseModel>();
                            while (r.Read())
                            {
                                resultado.Add(new zonasTarifasListasBaseModel()
                                {
                                    origen = r["origen"].ToString(),
                                    destino = r["destino"].ToString(),
                                    servicio = r["servicio"].ToString(),
                                    importe = verificaNullable.verificarDouble(r["importe"].ToString()),
                                    importeConAumento = verificaNullable.verificarDouble(r["importeConAumento"].ToString())


                                });
                            }
                            result.objectResult = resultado;

                            con.Close();
                        }
                        result.Status = 1;
                        result.Message = "OK";
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = 0;
                result.Message = e.Message;
            }

            return result;
        }

        [HttpPost("valoracionTarifas")]

        public Models.Result valoracionTarifas([FromBody] tariTaxiApi.Models.parametres.zonasvaloracionInsertar parametros)
        {
            Models.Result result = new Models.Result();

            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_zonaValoracion_inserta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idzona", SqlDbType.Int).Value = parametros.idzona;
                        cmd.Parameters.Add("@voto", SqlDbType.Int).Value = parametros.voto;
                        cmd.Parameters.Add("@coordenada", SqlDbType.Char).Value = parametros.coordenada;
                        cmd.Parameters.Add("@idusuario", SqlDbType.Int).Value = parametros.idusuario;

                        cmd.ExecuteNonQuery();
                        result.Status = 1;
                        result.Message = "OK";
                        var resultado = new
                        {

                            mensaje = "datos guardados exitosamente"
                        };
                        result.objectResult = resultado;

                    }
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
