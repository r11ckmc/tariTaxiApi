using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.Archivos
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        [HttpPost("documentosInsertar")]
        public Models.Result documentosInsertar([FromForm] tariTaxiApi.Models.parametres.Archivos.DocumentosModel parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                String fileExtension = parametros.documento.FileName;
                String globalRoute = Rutas.getRutaArchivosPersonas();


                var extension = Path.GetExtension(fileExtension);
                extension = extension.Remove(0, 1);

                Models.Archivos.DocumnetosStoreModel dto =
                    new Models.Archivos.DocumnetosStoreModel()
                    {
                        idTipoDocumento = parametros.idTipoDocumento,
                        numeroDocumento = parametros.numeroDocumento,
                        Vigencia = parametros.Vigencia,
                        mimeType = extension,
                        Observaciones = parametros.Observaciones,
                        idUsuario = parametros.idUsuario,
                        Idioma = parametros.Idioma
                    };




                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_personasDocumentos_insertarV2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idTipoDocumento", SqlDbType.Int).Value = dto.idTipoDocumento;
                        cmd.Parameters.Add("@numeroDocumento", SqlDbType.Char).Value = dto.numeroDocumento;
                        cmd.Parameters.Add("@Vigencia", SqlDbType.Char).Value = dto.Vigencia;
                        cmd.Parameters.Add("@mimeType", SqlDbType.Char).Value = dto.mimeType;
                        cmd.Parameters.Add("@Observaciones", SqlDbType.Char).Value = dto.Observaciones;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = dto.idUsuario;
                        cmd.Parameters.Add("@Idioma", SqlDbType.Char).Value = dto.Idioma;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
                            {
                                String ruta = r["ruta"].ToString();
                                globalRoute += ruta;
                                //result.objectResult =new {rutaa= globalRoute};
                                using (var stream=System.IO.File.Create(globalRoute))
                                {
                                    parametros.documento.CopyTo(stream);
                                }
                                result.objectResult = new {codigo=1,mensaje="documento insertado" };
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

            //result.objectResult = parametros;

        }

        [HttpPost("documentosInsertarV2")]
        public Models.Result documentosInsertarV2([FromBody] tariTaxiApi.Models.parametres.Archivos.DocumentosV2Model parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                //String fileExtension = parametros.documento.FileName;
                String globalRoute = Rutas.getRutaArchivosPersonas(); //"D:/www/Pruebas/PanelTariTaxiDocumentos";


               // var extension = Path.GetExtension(fileExtension);
               //extension = extension.Remove(0, 1);
                String extension = "jpg";

                Models.Archivos.DocumnetosStoreModel dto =
                    new Models.Archivos.DocumnetosStoreModel()
                    {
                        idTipoDocumento = parametros.idTipoDocumento,
                        numeroDocumento = parametros.numeroDocumento,
                        Vigencia = parametros.Vigencia,
                        mimeType = extension,
                        Observaciones = parametros.Observaciones,
                        idUsuario = parametros.idUsuario,
                        Idioma = parametros.Idioma
                    };




                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_personasDocumentos_insertarV2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idTipoDocumento", SqlDbType.Int).Value = dto.idTipoDocumento;
                        cmd.Parameters.Add("@numeroDocumento", SqlDbType.Char).Value = dto.numeroDocumento;
                        cmd.Parameters.Add("@Vigencia", SqlDbType.Char).Value = dto.Vigencia;
                        cmd.Parameters.Add("@mimeType", SqlDbType.Char).Value = dto.mimeType;
                        cmd.Parameters.Add("@Observaciones", SqlDbType.Char).Value = dto.Observaciones;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = dto.idUsuario;
                        cmd.Parameters.Add("@Idioma", SqlDbType.Char).Value = dto.Idioma;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
                            {
                                String ruta = r["ruta"].ToString();
                                globalRoute += ruta;
                                //result.objectResult =new {rutaa= globalRoute};
                                //using (var stream = System.IO.File.Create(globalRoute))
                                {
                                    //parametros.documento.CopyTo(stream);
                                    System.IO.File.WriteAllBytes(globalRoute, Convert.FromBase64String(parametros.documento));
                                }
                                result.objectResult = new { codigo = 1, mensaje = "documento insertado" };
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

            //result.objectResult = parametros;

        }

        [HttpPost("InsertarUsuarioV5")]

        public Models.Result InsertarUsuarioV5([FromBody] tariTaxiApi.Models.parametres.Archivos.RegistroConDocumentosModel parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
          

                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {

                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_usuarios_insertaV5", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@nombre", SqlDbType.Char).Value = parametros.nombre;
                        cmd.Parameters.Add("@apellidos", SqlDbType.Char).Value = parametros.apellidos;
                        cmd.Parameters.Add("@email", SqlDbType.Char).Value = parametros.email;
                        cmd.Parameters.Add("@contrasena", SqlDbType.Char).Value = parametros.contrasena;
                        cmd.Parameters.Add("@genero", SqlDbType.Char).Value = parametros.genero;
                        cmd.Parameters.Add("@idPaisTelefono", SqlDbType.Int).Value = parametros.idPaisTelefono;
                        cmd.Parameters.Add("@Telefono", SqlDbType.Char).Value = parametros.Telefono;
                        //cmd.Parameters.Add("@habilitado", SqlDbType.Int).Value = habilitado;
                        //cmd.Parameters.Add("@recuperarClave", SqlDbType.Int).Value = recuperarClave;
                        cmd.Parameters.Add("@idiomaPreferido", SqlDbType.Int).Value = parametros.idiomaPreferido;
                        cmd.Parameters.Add("@idrol", SqlDbType.Int).Value = parametros.idrol;
                        //cmd.Parameters.Add("@foto", SqlDbType.Char).Value = parametros.foto;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
                            {
                                int idusuario = Int32.Parse(r["idUsuario"].ToString());

                                if (idusuario > 0)
                                {
                                    Models.parametres.Archivos.DocumentosV2Model dto =
                                    new Models.parametres.Archivos.DocumentosV2Model()
                                    {
                                        idTipoDocumento = 11,
                                        numeroDocumento = "11",
                                        Vigencia = "",
                                        documento = parametros.foto,
                                        Observaciones = "",
                                        idUsuario = idusuario,
                                        Idioma = "esp"
                                    };

                                    Task.Run(async () =>
                                    {
                                        Task<Models.Result> Tesult = documentosInsertarV3(dto);
                                        result = await Tesult;
                                    }).GetAwaiter().GetResult();
                                }
                                else
                                result.objectResult = new { codigo = 0, mensaje = "El usuario ya esxiste" };
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

            //result.objectResult = parametros;

        }


        private async Task<Models.Result> documentosInsertarV3(tariTaxiApi.Models.parametres.Archivos.DocumentosV2Model parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                //String fileExtension = parametros.documento.FileName;
                String globalRoute = Rutas.getRutaArchivosPersonas();


                // var extension = Path.GetExtension(fileExtension);
                //extension = extension.Remove(0, 1);
                String extension = "jpg";

                Models.Archivos.DocumnetosStoreModel dto =
                    new Models.Archivos.DocumnetosStoreModel()
                    {
                        idTipoDocumento = parametros.idTipoDocumento,
                        numeroDocumento = parametros.numeroDocumento,
                        Vigencia = parametros.Vigencia,
                        mimeType = extension,
                        Observaciones = parametros.Observaciones,
                        idUsuario = parametros.idUsuario,
                        Idioma = parametros.Idioma
                    };




                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_personasDocumentos_insertarV2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idTipoDocumento", SqlDbType.Int).Value = dto.idTipoDocumento;
                        cmd.Parameters.Add("@numeroDocumento", SqlDbType.Char).Value = dto.numeroDocumento;
                        cmd.Parameters.Add("@Vigencia", SqlDbType.Char).Value = dto.Vigencia;
                        cmd.Parameters.Add("@mimeType", SqlDbType.Char).Value = dto.mimeType;
                        cmd.Parameters.Add("@Observaciones", SqlDbType.Char).Value = dto.Observaciones;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = dto.idUsuario;
                        cmd.Parameters.Add("@Idioma", SqlDbType.Char).Value = dto.Idioma;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
                            {
                                
                                String ruta = r["ruta"].ToString();
                                globalRoute += ruta;
                                //result.objectResult =new {rutaa= globalRoute};
                                //using (var stream = System.IO.File.Create(globalRoute))
                                {
                                    //parametros.documento.CopyTo(stream);
                                    System.IO.File.WriteAllBytes(globalRoute, Convert.FromBase64String(parametros.documento));
                                }
                                 result.objectResult = new { codigo = 1, mensaje = "usuario insertado",idusuario=parametros.idUsuario };
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

            //result.objectResult =;

        }

    }
}
