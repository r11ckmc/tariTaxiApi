using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace tariTaxiApi.Controller.taximetro
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaximetroController : ControllerBase
    {


        [HttpPost("insertarViajeTximetro")]
        public Models.Result insertarViajeTximetro([FromBody] tariTaxiApi.Models.parametres.taimetroInsertar parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_viajeTaximetroFinal_insertaV2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idViaje", SqlDbType.Int).Value = parametros.idViaje;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                var res = new { idTaximetroFinal = r["idTaximetroFinal"].ToString()};
                                result.objectResult = res;
                            }
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
