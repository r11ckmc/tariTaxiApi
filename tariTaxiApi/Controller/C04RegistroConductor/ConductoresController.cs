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

namespace tariTaxiApi.Controller.C04RegistroConductor
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductoresController : ControllerBase
    {
        [HttpPost("ConductoresInerta")]
        public Models.Result ConductoresInerta([FromBody] tariTaxiApi.Models.parametres.ConductorInsert parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_conductores_inserta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@nombre", SqlDbType.Char).Value = parametros.nombre;
                        cmd.Parameters.Add("@apellidos", SqlDbType.Char).Value = parametros.apellidos;
                        cmd.Parameters.Add("@genero", SqlDbType.Char).Value = parametros.genero;
                        cmd.Parameters.Add("@fechaNac", SqlDbType.Char).Value = parametros.fechaNac;
                        cmd.Parameters.Add("@idPaisTelefono", SqlDbType.Int).Value = parametros.idPaisTelefono;
                        cmd.Parameters.Add("@Telefono", SqlDbType.Char).Value = parametros.Telefono;
                        cmd.Parameters.Add("@telefonoFijo", SqlDbType.Char).Value = parametros.telefonoFijo;
                        cmd.Parameters.Add("@Direccion", SqlDbType.Char).Value = parametros.Direccion;
                        cmd.Parameters.Add("@email", SqlDbType.Char).Value = parametros.email;
                        cmd.Parameters.Add("@contrasena", SqlDbType.Char).Value = parametros.contrasena;
                        cmd.Parameters.Add("@idiomaPreferido", SqlDbType.Int).Value = parametros.idiomaPreferido;
                        cmd.Parameters.Add("@poseeLicencia", SqlDbType.Int).Value = parametros.poseeLicencia;
                        cmd.Parameters.Add("@vigenciaL", SqlDbType.Char).Value = parametros.vigenciaL;
                        cmd.Parameters.Add("@tipoL", SqlDbType.Char).Value = parametros.tipoL;

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
                                    var res = new { ruta = verificaNullable.verificarInt(r["idUsuario"].ToString()) };
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
