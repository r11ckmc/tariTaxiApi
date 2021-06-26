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
using tariTaxiApi.Models.zonas;
using tariTaxiApi.operaciones;

namespace tariTaxiApi.Controller.ubicacionZona
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionController : ControllerBase
    {
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

        [HttpPost("zonaDestino")]
        public Models.Result zonaDestino([FromBody] tariTaxiApi.Models.parametres.ubicacion parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_zonaDestino", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@origenGPS", SqlDbType.Char).Value = parametros.coordenadas;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
                            {

                                if (r.GetName(0) == "idZonaDestino")
                                {
                                    var respuesta = new { idZonaDestino = verificaNullable.verificarInt(r["idZonaDestino"].ToString()) };
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

        [HttpPost("zonaTarifas")]
        public Models.Result zonaTarifas([FromBody] tariTaxiApi.Models.parametres.Tarifas parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_zonaTarifas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idZonaOrigen", SqlDbType.Int).Value = parametros.zonaOrigen;
                        cmd.Parameters.Add("@idZonaDestino", SqlDbType.Int).Value = parametros.zonaDestino;


                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
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
                                            TipoServicio = r["Descripcion"].ToString()
                                        });
                                        veriificar = true;

                                    }
                                    if (veriificar)
                                        result.objectResult = zonas;
                                    else
                                        result.objectResult = error;

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

        [HttpPost("zonaOrigen2")]
        public Models.Result zonaOrigen2([FromBody] tariTaxiApi.Models.parametres.ubicacion parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_zona_nombre", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@origenGPS", SqlDbType.Char).Value = parametros.coordenadas;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
                            {

                                if (r.GetName(0) == "nombre")
                                {
                                    var respuesta = new { idZonaOrigen = r["nombre"].ToString() };
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

        [HttpPost("borrarZona")]
        public Models.Result borrarZona([FromBody] tariTaxiApi.Models.parametres.Quitarzona parametros)
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


                                

                                result.objectResult = list;
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

    }
}
