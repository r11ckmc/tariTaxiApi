using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.Models.conductores;

namespace tariTaxiApi.Controller.concesionario
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxistasController : ControllerBase
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
                            List<TaxistasModel> list =
                                new List<TaxistasModel>();
                            while (r.Read())
                            {


                                list.Add(new TaxistasModel()
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
    }
}
