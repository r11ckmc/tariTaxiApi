using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.Models.parametres.usuarios;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpPost("ConsultaUsuario")]
        public Models.Result ConsultaUsuario([FromBody] tariTaxiApi.Models.parametres.usuarios.usuariosConsultaModel parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_usuarios_consultarV3", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@nombre", SqlDbType.Char).Value = parametros.nombre;
                        cmd.Parameters.Add("@correoElectronico", SqlDbType.Char).Value = parametros.correoElectronico;
                        cmd.Parameters.Add("@habilitado", SqlDbType.Int).Value = parametros.habilitado;
                        cmd.Parameters.Add("@idRol", SqlDbType.Int).Value = parametros.idRol;
                        cmd.Parameters.Add("@idRegion", SqlDbType.Int).Value = parametros.idRegion;
                        cmd.Parameters.Add("@idUsuarioSesion", SqlDbType.Int).Value = parametros.idUsuarioSesion;
                        cmd.Parameters.Add("@Idioma", SqlDbType.Char).Value = parametros.Idioma;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<usuarioConsultaSalidaModel> resultado = new List<usuarioConsultaSalidaModel>();
                            while (r.Read())
                            {
                                resultado.Add(new usuarioConsultaSalidaModel()
                                {
                                    idUsuario = verificaNullable.verificarInt(r["idUsuario"].ToString()),
                                    Nombre = r["Nombre"].ToString(),
                                    correoElectronico = r["correoElectronico"].ToString(),
                                    habilitado = verificaNullable.verificarInt(r["habilitado"].ToString()),
                                    Roles = r["Roles"].ToString(),
                                    Regiones=r["Regiones"].ToString(),
                                    ultimoAcceso = r["ultimoAcceso"].ToString()


                                }) ;
                            }
                            result.objectResult = resultado;

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
