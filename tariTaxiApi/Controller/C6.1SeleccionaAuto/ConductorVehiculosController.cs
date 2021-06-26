using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.Models.C6._1SeleccionaAuto;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.C6._1SeleccionaAuto
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductorVehiculosController : ControllerBase
    {
        [HttpPost("conductorVehiculosConsulta")]
        public Models.Result conductorVehiculosConsulta([FromBody] tariTaxiApi.Models.parametres.usuarioModel parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_conductorVehiculos_consulta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idConductor", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@idioma", SqlDbType.Char).Value = parametros.idioma;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<conductorVehiculosConsultaModel> list =
                                new List<conductorVehiculosConsultaModel>();
                            while (r.Read())
                            {



                                list.Add(new conductorVehiculosConsultaModel()
                                {
                                    idConductor = verificaNullable.verificarInt(r["idConductor"].ToString()),
                                   descripcion = r["descripcion"].ToString(),
                                    modelo = r["modelo"].ToString(),
                                    placa = r["placa"].ToString()


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
