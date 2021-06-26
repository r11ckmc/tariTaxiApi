using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using tariTaxiApi.Models.error;
using tariTaxiApi.Models.zonas;
using tariTaxiApi.Models.zonas.entidad;
using tariTaxiApi.Models.zonas.pais;
using tariTaxiApi.Models.zonas.region;
using tariTaxiApi.Models.zonasRescursos;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonasController : ControllerBase
    {
        [HttpPost("zonasTarifasGps")]
        public Models.Result zonasTarifasGps([FromBody] tariTaxiApi.Models.parametres.ZonaTarifa parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_Zonas_tarifas_gps", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@origen", SqlDbType.Char).Value = parametros.origen;
                        cmd.Parameters.Add("@destino", SqlDbType.Char).Value = parametros.destino;
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<zonasTarifasModel> zonas = new List<zonasTarifasModel>();
                            List<ErrorStatus> error =
                                        new List<ErrorStatus>();
                            Boolean veriificar;
                            while (r.Read())
                            {
                                if (r.GetName(0) == "codigo")
                                {

                                    error.Add(new ErrorStatus()
                                    {
                                        codigo = verificaNullable.verificarInt(r["codigo"].ToString()),
                                        mensaje = r["mensaje"].ToString()
                                    });
                                    veriificar = false;
                                }
                                else
                                {
                                    zonas.Add(new zonasTarifasModel()
                                    {
                                        importe = verificaNullable.verificarDouble(r["importe"].ToString()),
                                        idTipoServicio = verificaNullable.verificarInt(r["idTipoServicio"].ToString()),
                                        TipoServicio = r["Descripcion"].ToString(),
                                        idZonaOrigen = verificaNullable.verificarInt(r["idZonaOrigen"].ToString()),
                                        idZonaDestino = verificaNullable.verificarInt(r["idZonaDestino"].ToString())

                                    });
                                    veriificar = true;

                                }
                                if (veriificar)
                                    result.objectResult = zonas;
                                else
                                    result.objectResult = error;


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

        public Models.Result zonasTarifasGps([FromBody] tariTaxiApi.Models.parametres.conductoresCercanos parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_conductores_cercanosV2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idtipoServicio", SqlDbType.Int).Value = parametros.idtipoServicio;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@latPasajero", SqlDbType.Int).Value = parametros.latPasajero;
                        cmd.Parameters.Add("@lonPasajero", SqlDbType.Int).Value = parametros.lonPasajero;
                        cmd.Parameters.Add("@idZonaOrigen", SqlDbType.Int).Value = parametros.idZonaOrigen;
                        cmd.Parameters.Add("@idZonaDestino", SqlDbType.Int).Value = parametros.idZonaDestino;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<zonasTarifasModel> zonas = new List<zonasTarifasModel>();
                            List<ErrorStatus> error =
                                        new List<ErrorStatus>();
                            Boolean veriificar;
                            while (r.Read())
                            {
                                if (r.GetName(0) == "codigo")
                                {

                                    error.Add(new ErrorStatus()
                                    {
                                        codigo = verificaNullable.verificarInt(r["codigo"].ToString()),
                                        mensaje = r["mensaje"].ToString()
                                    });
                                    veriificar = false;
                                }
                                else
                                {
                                    zonas.Add(new zonasTarifasModel()
                                    {
                                        importe = verificaNullable.verificarDouble(r["importe"].ToString()),
                                        idTipoServicio = verificaNullable.verificarInt(r["idTipoServicio"].ToString()),
                                        TipoServicio = r["Descripcion"].ToString(),
                                        idZonaOrigen = verificaNullable.verificarInt(r["idZonaOrigen"].ToString()),
                                        idZonaDestino = verificaNullable.verificarInt(r["idZonaDestino"].ToString())

                                    });
                                    veriificar = true;

                                }
                                if (veriificar)
                                    result.objectResult = zonas;
                                else
                                    result.objectResult = error;


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

        [HttpPost("zonaOrigen")]
        public Models.Result zonaOrigen([FromBody] tariTaxiApi.Models.parametres.ubicacion parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_zonaOrigen", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@origenGPS", SqlDbType.Char).Value = parametros.coordenadas;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
                            {

                                if (r.GetName(0) == "idZonaOrigen")
                                {
                                    var respuesta = new { idZonaOrigen = verificaNullable.verificarInt(r["idZonaOrigen"].ToString()) };
                                    result.objectResult = respuesta;

                                }
                                else
                                {
                                    List<ErrorStatus> res = new List<ErrorStatus>();
                                    res.Add(
                                        new ErrorStatus()
                                        {
                                            codigo = verificaNullable.verificarInt(r["codigo"].ToString()),
                                            mensaje = r["mensaje"].ToString()
                                        }
                                        );
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

        [HttpPost("testParametrer")]
        public Models.Result testParametrer([FromBody] tariTaxiApi.Models.parametres.ubicacion parametros)
        {
            if (parametros is null)
            {
                throw new ArgumentNullException(nameof(parametros));
            }

            Models.Result result = new Models.Result();
            var res = new
            {
                dto1 = parametros.coordenadas
            };
            result.Message = "dto envido";

            //result.objectResult = res;
            result = MborrarZona(Int32.Parse(parametros.coordenadas));

            return result;
        }

        [HttpPost("deleteZone")]
        public Models.Result deleteZone([FromBody] tariTaxiApi.Models.parametres.Quitarzona parametros)
        {
            if (parametros is null)
            {
                throw new ArgumentNullException(nameof(parametros));
            }

            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_quitar_zona", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idzona", SqlDbType.Int).Value = parametros.id;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<ErrorStatus> list =
                                new List<ErrorStatus>();
                            while (r.Read())
                            {

                                list.Add(new ErrorStatus()
                                {
                                    codigo = verificaNullable.verificarInt(r["codigo"].ToString()),
                                    mensaje = r["mensaje"].ToString()


                                });


                                var test = new { mensaje = parametros.id };

                                result.objectResult = test;
                            }
                            con.Close();
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


        [HttpPost("deleteZoneV2")]

        public Models.Result deleteZoneV2([FromBody] tariTaxiApi.Models.parametres.Quitarzona parametros)
        {
            if (parametros is null)
            {
                throw new ArgumentNullException(nameof(parametros));
            }

            Models.Result result = new Models.Result();

            //result=MborrarZona(Int32.Parse(parametros.id));
            var test = new { mensaje = parametros.id };

            result.objectResult = test;
            return result;
        }

        [HttpGet("obtenerUltimaZona")]
        public Models.Result obtenerUltimaZona()
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_lasIDZona", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            
                            while (r.Read())
                            {

                                var res = new {
                                    idZona = verificaNullable.verificarInt(r["idZona"].ToString())
                                };

                                result.objectResult = res;

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

        [HttpGet("nombreZonas")]
        public Models.Result nombreZonas()
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_zona_nombre_consuta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<zonaidnombreModel> res =
                                 new List<zonaidnombreModel>();
                            while (r.Read())
                            {

                                res.Add(new zonaidnombreModel()
                                {
                                    idZona = verificaNullable.verificarInt(r["idZona"].ToString()),
                                    nombre = r["nombre"].ToString()
                                });


                            }
                            result.objectResult = res;

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


        private Models.Result MborrarZona(int id)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_quitar_zona", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idzona", SqlDbType.Int).Value = id;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<ErrorStatus> list =
                                new List<ErrorStatus>();
                            while (r.Read())
                            {

                                list.Add(new ErrorStatus()
                                {
                                    codigo = verificaNullable.verificarInt(r["codigo"].ToString()),
                                    mensaje = r["mensaje"].ToString()


                                });


                                var test = new { mensaje = id };

                                result.objectResult = test;
                            }
                            con.Close();
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

        [HttpGet("todasLasZonas")]
        public Models.Result todasLasZonas()
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_zonas_totales_consulta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<zonasmapaModel> res =
                                 new List<zonasmapaModel>();
                            while (r.Read())
                            {

                                res.Add(new zonasmapaModel()
                                {
                                    idZona = verificaNullable.verificarInt(r["idZona"].ToString()),
                                    nombre = r["nombre"].ToString(),
                                    coordenadas= obtenerCordenadas(r["coordenadas"].ToString())
                                });


                            }
                            result.objectResult = res;

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

        [HttpGet("todasLasZonasRegion")]
        public Models.Result todasLasZonasRegion()
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_zona_nombre_region_consulta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<zonasConRegionModel> res =
                                 new List<zonasConRegionModel>();
                            while (r.Read())
                            {

                                res.Add(new zonasConRegionModel()
                                {
                                    idZona = verificaNullable.verificarInt(r["idZona"].ToString()),
                                    nombre = r["nombre"].ToString(),
                                    descripcion = r["descripcion"].ToString()
                                });


                            }
                            result.objectResult = res;

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

        [HttpGet("todosLosPaises")]
        public Models.Result todosLosPaises()
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_paises_consulta_total", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<paisZonaModel> res =
                                 new List<paisZonaModel>();
                            while (r.Read())
                            {

                                res.Add(new paisZonaModel()
                                {
                                    idPais = verificaNullable.verificarInt(r["idPais"].ToString()),
                                    esp = r["esp"].ToString(),
                                });


                            }
                            result.objectResult = res;

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

        [HttpGet("todasLasEntidades")]
        public Models.Result todasLasEntidades()
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_entidades_consulta_total", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<entidadZonaModel> res =
                                 new List<entidadZonaModel>();
                            while (r.Read())
                            {

                                res.Add(new entidadZonaModel()
                                {
                                    idEntidad = verificaNullable.verificarInt(r["idEntidad"].ToString()),
                                    idPais = verificaNullable.verificarInt(r["idPais"].ToString()),
                                    descripcion= r["descripcion"].ToString(),
                                    habilitado = r["habilitado"].ToString()
                                });


                            }
                            result.objectResult = res;

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

        [HttpGet("todasLasRegiones")]
        public Models.Result todasLasRegiones()
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_regiones_consulta_total", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<regionZonaModel> res =
                                 new List<regionZonaModel>();
                            while (r.Read())
                            {

                                res.Add(new regionZonaModel()
                                {
                                    idRegion = verificaNullable.verificarInt(r["idRegion"].ToString()),
                                    idEntidad = verificaNullable.verificarInt(r["idEntidad"].ToString()),
                                    descripcion = r["descripcion"].ToString(),
                                    habilitado = r["habilitado"].ToString()
                                });


                            }
                            result.objectResult = res;

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


        [HttpPost("zonasSinTarifas")]

        public Models.Result zonasSinTarifas([FromBody] tariTaxiApi.Models.parametres.Viaje parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_zonas_sinTarifa_nombre", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idZonaOrigen", SqlDbType.Int).Value = parametros.idViaje;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<zonasNombreRegionModel> resultado = new List<zonasNombreRegionModel>();
                            while (r.Read())
                            {
                                resultado.Add(new zonasNombreRegionModel()
                                {
                                    idZona = verificaNullable.verificarInt(r["idZona"].ToString()),
                                    idRegion = verificaNullable.verificarInt(r["idRegion"].ToString()),
                                    nombre = r["nombre"].ToString()
                                });
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





        private List<latlngModel> obtenerCordenadas(String coordenadas)
        {

            String coordinates;
            String[] elimiarparentesis = coordenadas.Split("((".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            coordinates = elimiarparentesis[1];
            elimiarparentesis = coordinates.Split("))".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            String[] latlng = elimiarparentesis[0].Split(',');

            List<latlngModel> lst = new List<latlngModel>();
            for (int i = 0; i < latlng.Length; i++)
            {
                String[] coor = latlng[i].Split(' ');
                if(coor.Length>1)
                lst.Add(new latlngModel
                {
                    lat = float.Parse(coor[1]),
                    lng = float.Parse(coor[0])
                });
            }
            return lst;
        }

    }

    
}
