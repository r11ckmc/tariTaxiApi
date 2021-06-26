using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using tariTaxiApi.Models;
using tariTaxiApi.Models.servicio;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.servicioTarifas
{
    [Route("api/[controller]")]
    [ApiController]
    public class servicosController : ControllerBase
    {
        [HttpGet("tipoServicioConsulta")]
        public Models.Result tipoServicioConsulta()
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_tipoServicios_consulta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                       

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<tipoServicio> list =
                                new List<tipoServicio>();
                            while (r.Read())
                            {


                                    list.Add(new tipoServicio()
                                    {
                                        idTipoServicio = verificaNullable.verificarInt(r["idTipoServicio"].ToString()),
                                        descripcion = r["descripcion"].ToString(),
                                        icono = r["icono"].ToString(),
                                        costoInicial = verificaNullable.verificarDouble(r["costoInicial"].ToString())
                                        

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
        [HttpPost("insertarTarifa")]
        public Models.Result insertarTarifa([FromBody] tariTaxiApi.Models.parametres.tarifasInsertar parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_insertarZonaTarifa", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idOrigen", SqlDbType.Int).Value = parametros.idOrigen;
                        cmd.Parameters.Add("@idDestino", SqlDbType.Int).Value = parametros.idDestino;
                        cmd.Parameters.Add("@importe", SqlDbType.Decimal).Value = parametros.importe;
                        cmd.Parameters.Add("@tipoServicio", SqlDbType.Int).Value = parametros.tipoServicio;

                        cmd.ExecuteNonQuery();

                        {
                            var res = new
                            {
                                resultado = "Insert data Successfull"
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
        
        [HttpPost("insertarActualizarTarifa")]
        public Models.Result insertarActualizarTarifa([FromBody] tariTaxiApi.Models.parametres.tarifasInsertar parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_tarifa_InsertarV2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idOrigen", SqlDbType.Int).Value = parametros.idOrigen;
                        cmd.Parameters.Add("@idDestino", SqlDbType.Int).Value = parametros.idDestino;
                        cmd.Parameters.Add("@importe", SqlDbType.Decimal).Value = parametros.importe;
                        cmd.Parameters.Add("@tipoServicio", SqlDbType.Int).Value = parametros.tipoServicio;

                        cmd.ExecuteNonQuery();

                        {
                            var res = new
                            {
                                resultado = "Insert data Successfull"
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

        [HttpPost("insertarActualizarTarifaMasivo")]

        public Models.Result insertarActualizarTarifaMasivo([FromBody] List<tariTaxiApi.Models.parametres.tarifasInsertar> parametros)
        {
            Models.Result result = new Models.Result();
            String datos="";
            try
            {

                foreach (var lista in parametros)
                {
                    
                    datos += lista.idOrigen + ",";
                    datos += lista.idDestino + ",";
                    datos += lista.importe + ",";
                    datos += lista.tipoServicio;
                    datos += "*";

                }

                var resultado = new {
                masivo= datos};
                result.objectResult = resultado;
                return result;
            }
            catch (Exception e)
            {

            }
            return result;
        }


        [HttpPost("ViajeTarifasConsulta")]
        public Models.Result ViajeTarifasConsulta([FromBody] tariTaxiApi.Models.parametres.taimetroInsertar parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_viajeTarifas_ConsultaV2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idViaje", SqlDbType.Int).Value = parametros.idViaje;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<viajeTarifasModel> resultado = new List<viajeTarifasModel>();
                            while (r.Read())
                            {
                                resultado.Add(new viajeTarifasModel()
                                {
                                    tarifabase = verificaNullable.verificarDouble(r["tarifabase"].ToString()),
                                    valorIncremento = verificaNullable.verificarDouble(r["valorIncremento"].ToString()),
                                    Propina = verificaNullable.verificarDouble(r["Propina"].ToString())

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


    }
}
