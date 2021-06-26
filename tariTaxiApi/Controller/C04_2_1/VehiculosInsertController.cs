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

namespace tariTaxiApi.Controller.C04_2_1
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosInsertController : ControllerBase
    {
        [HttpPost("vehiculosInserta")]
        public Models.Result vehiculosInserta([FromBody] tariTaxiApi.Models.parametres.VehiculosInserta parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_vehiculos_inserta", con))
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
                        cmd.Parameters.Add("@tipocobertura", SqlDbType.Char).Value = parametros.tipocobertura;
                        cmd.Parameters.Add("@nombreAsegurado", SqlDbType.Char).Value = parametros.nombreAsegurado;
                        cmd.Parameters.Add("@capacidad", SqlDbType.Int).Value = parametros.capacidad;
                        cmd.Parameters.Add("@idConductor", SqlDbType.Int).Value = parametros.idConductor;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@Idioma", SqlDbType.Char).Value = parametros.Idioma;
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
