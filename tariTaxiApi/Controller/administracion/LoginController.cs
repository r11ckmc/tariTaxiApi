using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.Models;
using tariTaxiApi.Models.error;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.administracion
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("panelLogin")]
        public Models.Result panelLogin([FromBody] tariTaxiApi.Models.parametres.Login.loginModel parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_panel_iniciosesion", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@email", SqlDbType.Char).Value = parametros.email;
                        cmd.Parameters.Add("@contrasena", SqlDbType.Char).Value = parametros.contrasena;
                        cmd.Parameters.Add("@ubicacionGPS", SqlDbType.Char).Value = parametros.ubicacionGPS;
                        cmd.Parameters.Add("@ipOrigen", SqlDbType.Char).Value = parametros.ipOrigen;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            List<loginViewModel> verificacion = new List<loginViewModel>();
                            //List<ErrorStatus> verificacion2 = new List<ErrorStatus>();


                            while (r.Read())
                            {

                                
                                if (r.GetName(1) == "esConductor")
                                {
                                    //int? conductor = verificaNullable.verificarInt(r["esConductor"].ToString());
                                    if (r["esConductor"].ToString() == "False")
                                    {
                                        verificacion.Add(new loginViewModel
                                        {
                                            codigo = 1,
                                            mensaje = r["Nombre"].ToString(),
                                            idusuario = Int32.Parse(r["idUsuario"].ToString()),
                                            Foto= Rutas.getRutaArchivosPersonas()+r["Foto"].ToString()
                                        });
                                    }
                                    else
                                    {
                                        verificacion.Add(new loginViewModel
                                        {
                                            codigo = 2,
                                            mensaje ="Acceso no autorizado",
                                            idusuario = 0,
                                            Foto = ""
                                        });
                                    }

                                }
                                else
                                 if (r.GetName(0) == "codigo")
                                {
                                    if (r["Mensaje"].ToString() == "Error en Contraseña")
                                    {
                                        verificacion.Add(new loginViewModel
                                        {
                                            codigo = 3,
                                            mensaje = "Usuario o contraseña incorrectos",
                                            idusuario = 0,
                                            Foto = ""
                                        });
                                    }
                                    else {
                                        verificacion.Add(new loginViewModel
                                        {
                                            codigo = 2,
                                            mensaje = "Acceso no autorizado",
                                            idusuario = 0,
                                            Foto = ""
                                        });
                                    }
                                }



                                }


                            result.objectResult = verificacion;
                            }


                        }
                        con.Close();
                    }
                    result.Status = 1;
                    result.Message = "OK";

                
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
