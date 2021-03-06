using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.C8ConfirmacionServicio
{//por modificar
    [Route("api/[controller]")]
    [ApiController]
    public class ViajeConductorConfirmadoController : ControllerBase
    {
        public Models.Result viajeCancelaciones([FromBody] tariTaxiApi.Models.parametres.ViajeConductorConfirmado parametros)
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
                        cmd.Parameters.Add("@idConductor", SqlDbType.Int).Value = parametros.idConductor;
                        cmd.Parameters.Add("@idVehiculo", SqlDbType.Int).Value = parametros.idVehiculo;
                        cmd.Parameters.Add("@idioma", SqlDbType.Char).Value = parametros.idioma;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
                            {
                                var resultado = new
                                {
                                    idViajeCancelacion =
                                        verificaNullable.verificarInt(r["idViajeCancelacion"].ToString()),
                                    Result = "insert successfull"
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
