using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.Models.C12_3_3Facturas;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.C12_3_3Facturas
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        [HttpPost("viajeTaximetroFinalConsulta")]
        public Models.Result viajeTaximetroFinalConsulta([FromBody] Models.parametres.Viaje parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_viajeTaximetroFinal_Consulta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idViaje", SqlDbType.Int).Value = parametros.idViaje;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<viajeTaximetroFinalConsultaModel> list =
                                new List<viajeTaximetroFinalConsultaModel>();
                            //int? idPersonaInt, numLint;
                            while (r.Read())
                            {




                                list.Add(new viajeTaximetroFinalConsultaModel()
                                {
                                    tarifabase = verificaNullable.verificarDouble(r["tarifabase"].ToString()),
                                    distancia = verificaNullable.verificarDouble(r["distancia"].ToString()),
                                    tiempo = verificaNullable.verificarDouble(r["tiempo"].ToString()),
                                    total = verificaNullable.verificarDouble(r["total"].ToString()),


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
