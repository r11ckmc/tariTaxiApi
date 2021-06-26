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

namespace tariTaxiApi.Controller.Vehiculos
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {

        [HttpGet("VehiculosMarcasConsulta")]
        public Models.Result VehiculosMarcasConsulta()
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_VehiculosMarcas_Consulta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<Models.vehiculos.VehiculosMarcasViewModel> resultados = 
                                    new List<Models.vehiculos.VehiculosMarcasViewModel>();

                            while (r.Read())
                            {

                                 resultados.Add(new Models.vehiculos.VehiculosMarcasViewModel
                                 {
                                     idMarcaVehiculo = verificaNullable.verificarInt(r["idMarcaVehiculo"].ToString()),
                                     descripcion=r["descripcion"].ToString(),
                                     habilitado = r["habilitado"].ToString()

                                 });

                                //result.objectResult = x;
                            }
                            result.objectResult = resultados;

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
