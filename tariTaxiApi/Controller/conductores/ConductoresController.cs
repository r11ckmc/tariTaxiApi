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
using tariTaxiApi.Models.conductores;
using tariTaxiApi.Models.error;
using tariTaxiApi.Models.rutas;
using tariTaxiApi.Models.zonas;
using tariTaxiApi.Models.zonasRescursos;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.conductores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductoresController : ControllerBase
    {
        [HttpPost("ConductoresCeranos")]
        public Models.Result ConductoresCeranos([FromBody] tariTaxiApi.Models.parametres.conductoresCercanos parametros)
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
                        cmd.Parameters.Add("@latPasajero", SqlDbType.Char).Value = parametros.latPasajero;
                        cmd.Parameters.Add("@lonPasajero", SqlDbType.Char).Value = parametros.lonPasajero;
                        cmd.Parameters.Add("@idZonaOrigen", SqlDbType.Int).Value = parametros.idZonaOrigen;
                        cmd.Parameters.Add("@idZonaDestino", SqlDbType.Int).Value = parametros.idZonaDestino;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<conductoresCercanosModel> zonas = new List<conductoresCercanosModel>();
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
                                    zonas.Add(new conductoresCercanosModel()
                                    {
                                        idConductor = verificaNullable.verificarInt(r["idConductor"].ToString()),
                                        idVehiculo = verificaNullable.verificarInt(r["idVehiculo"].ToString()),
                                        idUsuario = verificaNullable.verificarInt(r["idUsuario"].ToString()),
                                        Habilitado = Boolean.Parse(r["Habilitado"].ToString()),
                                        foto = r["foto"].ToString(),
                                        nombre = r["nombre"].ToString(),
                                        Telefono = r["Telefono"].ToString(),
                                        calificacionPromedio = verificaNullable.verificarDouble(r["calificacionPromedio"].ToString()),
                                        linea = r["linea"].ToString(),
                                        Modelo = r["Modelo"].ToString(),
                                        Placa = r["Placa"].ToString(),
                                        Descripcion = r["Descripcion"].ToString(),
                                        Servicio = r["Servicio"].ToString(),
                                        costoinicial = verificaNullable.verificarDouble(r["costoinicial"].ToString()),
                                        UbicacionConductor = r["UbicacionConductor"].ToString()

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
        
        [HttpPost("conductoresCercaTotal")]
        public Models.Result conductoresCercaTotal([FromBody] tariTaxiApi.Models.parametres.CercaTotalModel parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_condCercaTotal_consultaV2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = parametros.idUsuario;
                        cmd.Parameters.Add("@posicion", SqlDbType.Char).Value = parametros.posicion;
                        cmd.Parameters.Add("@zonas", SqlDbType.Char).Value = parametros.zonas;
                        cmd.Parameters.Add("@idZonaOrigen", SqlDbType.Int).Value = parametros.idZonaOrigen;
                        cmd.Parameters.Add("@idZonaDestino", SqlDbType.Int).Value = parametros.idZonaDestino;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<cercanosTotal> zonas = new List<cercanosTotal>();
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
                                    zonas.Add(new cercanosTotal()
                                    {
                                        idConductor = verificaNullable.verificarInt(r["idConductor"].ToString()),
                                        Latitud = r["Latitud"].ToString(),
                                        Longitud = r["Longitud"].ToString()
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


        [HttpGet("ultmaUbicacion")]
        public Models.Result ultmaUbicacion()
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_ultimaubicacion", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<ultimaUbicacionModel> res =
                                 new List<ultimaUbicacionModel>();
                            while (r.Read())
                            {

                                res.Add(new ultimaUbicacionModel()
                                {
                                    id = verificaNullable.verificarInt(r["id"].ToString()),
                                    habilitado = r["Habilitado"].ToString(),
                                    activo = r["Activo"].ToString(),
                                    calificacionPromedio = verificaNullable.verificarDouble(r["calificacionPromedio"].ToString()),
                                    nombre = r["Nombre"].ToString(),
                                    apelldios = r["Apellidos"].ToString(),
                                    telefono = r["Telefono"].ToString(),
                                    coordenadas = obtenerCordenadas(
                                        r["latitud"].ToString(),
                                        r["longitud"].ToString()
                                        )
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

        [HttpGet("rutasConductores")]
        public Models.Result rutasConductores()
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_viajes_Actuales_ruta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<rutaModel> res =
                                 new List<rutaModel>();
                            while (r.Read())
                            {

                                res.Add(new rutaModel()
                                {
                                    idViaje = verificaNullable.verificarInt(r["idViajes"].ToString()),
                                    costoviaje = verificaNullable.verificarFloat(r["costoviaje"].ToString()),
                                    fechaHoraServicio = r["fechaHoraServicio"].ToString(),

                                    origen = obtenerCordenadas(
                                        r["latitudOrigen"].ToString(),
                                        r["longitudOrigen"].ToString()
                                        ),
                                    destino = obtenerCordenadas(
                                        r["latitudDestino"].ToString(),
                                        r["longitudDestino"].ToString()
                                        )
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
        [HttpGet("testTabla")]
        public Models.Result testTabla() {

            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_top10_stTable", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<borrartestTabla> res = new List<borrartestTabla>();

                            while (r.Read())
                            {
                                res.Add(new borrartestTabla()
                                {
                                    texto = r["texto"].ToString(),
                                    texto2 = r["texto2"].ToString(),
                                    fecha = Convert.ToDateTime(r["fecha"].ToString())
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
        [HttpGet("consultaGrid")]

        public Models.Result consultaGrid()
        {

            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_consultaGrid", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            List<consultaGrildModel> res = new List<consultaGrildModel>();

                            while (r.Read())
                            {
                                res.Add(new consultaGrildModel()
                                {
                                    idViaje=verificaNullable.verificarInt(r["idViaje"].ToString()),
                                    Servicio = r["Servicio"].ToString(),
                                    Importe = verificaNullable.verificarDouble(r["Importe"].ToString()),
                                    Taxista = verificaNullable.verificarInt(r["Taxista"].ToString()),
                                    Transferencia = verificaNullable.verificarDouble(r["Transferencia"].ToString()),
                                    ComisionBancaria = verificaNullable.verificarDouble(r["ComisionBancaria"].ToString()),
                                    Nombre = r["Nombre"].ToString(),
                                    Gafete = r["Gafete"].ToString()

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

        private List<latlngModel> obtenerCordenadas(String x,string y)
        {



            List<latlngModel> lst = new List<latlngModel>();
   

                    lst.Add(new latlngModel
                    {
                        lat = float.Parse(y),
                        lng = float.Parse(x)
                    });
            
            return lst;
        }




    }
}
