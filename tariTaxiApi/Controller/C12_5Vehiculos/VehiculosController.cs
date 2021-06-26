using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.Models.C12_5_Vehiculos;
using tariTaxiApi.Models.DataBase;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.C12_5_Vehiculos
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        [HttpPost("conductorConsulta")]
        public Models.Result conductorConsulta([FromBody] tariTaxiApi.Models.parametres.usuarioModel parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_conductor_consulta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@idioma", SqlDbType.Char).Value = parametros.idioma;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<ConductorConsultaModel> list =
                                new List<ConductorConsultaModel>();
                            while (r.Read())
                            {




                                list.Add(new ConductorConsultaModel()
                                {
                                    foto = r["foto"].ToString(),
                                    idPersona = verificaNullable.verificarInt(r["idPersona"].ToString()),
                                    nombre = r["nombre"].ToString(),
                                    apellidos = r["apellidos"].ToString(),
                                    genero = r["genero"].ToString(),
                                    CorreoElectronico = r["CorreoElectronico"].ToString(),
                                    rutaLic = r["rutaLic"].ToString(),
                                    numL = verificaNullable.verificarInt(r["numL"].ToString()),
                                    vigenciaLicencia = r["vigenciaLicencia"].ToString(),
                                    rutaAnt = r["rutaAnt"].ToString(),
                                    fechaAnt = r["fechaAnt"].ToString()

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
        
        [HttpPost("consultaDetalle")]
        public Models.Result consultaDetalle([FromBody] tariTaxiApi.Models.parametres.usuarioModel parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_vehiculos_ConsultaDetalle", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@idioma", SqlDbType.Char).Value = parametros.idioma;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<ConsultaDetalleModel> list =
                                new List<ConsultaDetalleModel>();
                            while (r.Read())
                            {




                                list.Add(new ConsultaDetalleModel()
                                {
                                    idMarcaVehiculo = verificaNullable.verificarInt(r["idMarcaVehiculo"].ToString()),
                                    idTipoServicio = verificaNullable.verificarInt(r["idTipoServicio"].ToString()),
                                    linea = r["linea"].ToString(),
                                    numFactura = r["numFactura"].ToString(),
                                    Modelo = r["Modelo"].ToString(),
                                    Color = r["Color"].ToString(),
                                    Cilindros = verificaNullable.verificarInt(r["Cilindros"].ToString()),
                                    numTarjetaCirculacion = r["numTarjetaCirculacion"].ToString(),
                                    Placa = r["Placa"].ToString(),
                                    numMotor = r["numMotor"].ToString(),
                                    Propietario = r["Propietario"].ToString(),
                                    polizaSeguro = r["polizaSeguro"].ToString(),
                                    Aseguradora = r["Aseguradora"].ToString(),
                                    Vigencia = r["Vigencia"].ToString(),
                                    tipoCobertura = r["tipoCobertura"].ToString(),
                                    nombreAsegurado = r["nombreAsegurado"].ToString(),
                                    capacidadPasajero = verificaNullable.verificarInt(r["capacidadPasajero"].ToString())


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
        
        [HttpPost("vehiculosActualiza")]
        public Models.Result vehiculosActualiza([FromBody] tariTaxiApi.Models.parametres.vehiculo parametros)
        {

            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_vehiculos_actualiza", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idMarcarVehiculo", SqlDbType.Int).Value = parametros.idMarcarVehiculo;
                        cmd.Parameters.Add("@idTipoServicio", SqlDbType.Int).Value = parametros.idTipoServicio;
                        cmd.Parameters.Add("@linea", SqlDbType.Char).Value = parametros.linea;
                        cmd.Parameters.Add("@NumFactura", SqlDbType.Char).Value = parametros.NumFactura;
                        cmd.Parameters.Add("@Modelo", SqlDbType.Char).Value = parametros.Modelo;
                        cmd.Parameters.Add("@color", SqlDbType.Char).Value = parametros.color;
                        cmd.Parameters.Add("@cilindro", SqlDbType.Int).Value = parametros.cilindro;
                        cmd.Parameters.Add("@numTarjetaC", SqlDbType.Char).Value = parametros.numTarjetaC;
                        cmd.Parameters.Add("@placa", SqlDbType.Char).Value = parametros.placa;
                        cmd.Parameters.Add("@numMotor", SqlDbType.Char).Value = parametros.numMotor;
                        cmd.Parameters.Add("@propietario", SqlDbType.Char).Value = parametros.propietario;
                        cmd.Parameters.Add("@polizaSeguro", SqlDbType.Char).Value = parametros.polizaSeguro;
                        cmd.Parameters.Add("@aseguradora", SqlDbType.Char).Value = parametros.aseguradora;
                        cmd.Parameters.Add("@vigencia", SqlDbType.Char).Value = parametros.vigencia;
                        cmd.Parameters.Add("@tipocovertura", SqlDbType.Char).Value = parametros.tipocovertura;
                        cmd.Parameters.Add("@nombreAsegurado", SqlDbType.Char).Value = parametros.nombreAsegurado;
                        cmd.Parameters.Add("@capacidad", SqlDbType.Int).Value = parametros.capacidad;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@Idioma", SqlDbType.Char).Value = parametros.Idioma;

                        {
                            cmd.ExecuteNonQuery();
                            Models.ResultModify res = new Models.ResultModify
                            {
                                IdModify = parametros.numTarjetaC.ToString(),
                                Result = "OK"
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
