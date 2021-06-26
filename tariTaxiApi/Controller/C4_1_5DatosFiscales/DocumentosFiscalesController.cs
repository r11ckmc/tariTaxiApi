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

namespace tariTaxiApi.Controller.C4_1_5DatosFiscales
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosFiscalesController : ControllerBase
    {
        [HttpPost("documentosFiscalesInserta")]
        public Models.Result documentosFiscalesInserta([FromBody] tariTaxiApi.Models.parametres.DtoFiscales parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_documentosFiscales_inserta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@contrasenaCSD", SqlDbType.Char).Value = parametros.contrasenaCSD;
                        cmd.Parameters.Add("@contrasenaFIEL", SqlDbType.Char).Value = parametros.contrasenaFIEL;
                        cmd.Parameters.Add("@esCSD", SqlDbType.Char).Value = parametros.esCSD;
                        cmd.Parameters.Add("@esCertificadoCSD", SqlDbType.Char).Value = parametros.esCertificadoCSD;
                        cmd.Parameters.Add("@esLlaveFIEL", SqlDbType.Char).Value = parametros.esLlaveFIEL;
                        cmd.Parameters.Add("@esCertificadoFIEL", SqlDbType.Char).Value = parametros.esCertificadoFIEL;
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
                                    var res = new { ruta = verificaNullable.verificarInt(r["idregistro"].ToString())};
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
