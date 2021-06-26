using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Controller.concesionario
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcesionarioController : ControllerBase
    {
        [HttpPost("RegistroTaxistasInsertarModificar")]
        public Models.Result RegistroTaxistasInsertarModificar([FromBody] tariTaxiApi.Models.parametres.Concesionario.TaxistasBasicoViewModel parametros)
        {
            Models.Result result = new Models.Result();
            try
            {
                string conString = Startup.Configuration.GetConnectionString("ConnectionDB");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("st_Concesionario_RegistroTaxistas_InsertarModificar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Nombre", SqlDbType.Char).Value = parametros.Nombre;
                        cmd.Parameters.Add("@nroGafete", SqlDbType.Char).Value = parametros.nroGafete;
                        cmd.Parameters.Add("@Apellidos", SqlDbType.Char).Value = parametros.Apellidos;
                        cmd.Parameters.Add("@RFC", SqlDbType.Char).Value = parametros.RFC;
                        cmd.Parameters.Add("@CURP", SqlDbType.Char).Value = parametros.CURP;
                        cmd.Parameters.Add("@correoElectronico", SqlDbType.Char).Value = parametros.correoElectronico;
                        cmd.Parameters.Add("@fechaNacimiento", SqlDbType.Char).Value = parametros.fechaNacimiento;
                        cmd.Parameters.Add("@Direccion", SqlDbType.Char).Value = parametros.Direccion;
                        cmd.Parameters.Add("@Telefono", SqlDbType.Char).Value = parametros.Telefono;
                        cmd.Parameters.Add("@familiarWhatsApp", SqlDbType.Char).Value = parametros.familiarWhatsApp;
                        cmd.Parameters.Add("@familiar", SqlDbType.Char).Value = parametros.familiar;
                        cmd.Parameters.Add("@idGrupoSanguineo", SqlDbType.Int).Value = parametros.idGrupoSanguineo;
                        cmd.Parameters.Add("@Genero", SqlDbType.Char).Value = parametros.Genero;
                        cmd.Parameters.Add("@alergias", SqlDbType.Char).Value = parametros.alergias;
                        cmd.Parameters.Add("@Observaciones", SqlDbType.Char).Value = parametros.Observaciones;
                        cmd.Parameters.Add("@NOTaxi", SqlDbType.Char).Value = parametros.NOTaxi;
                        cmd.Parameters.Add("@marca", SqlDbType.Int).Value = parametros.marca;
                        cmd.Parameters.Add("@modelo", SqlDbType.Char).Value = parametros.modelo;
                        cmd.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = parametros.TipoServicio;
                        cmd.Parameters.Add("@Placa", SqlDbType.Char).Value = parametros.Placa;
                        cmd.Parameters.Add("@Habilitado", SqlDbType.Int).Value = parametros.Habilitado;

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
                            {
                                var resultado = new
                                {
                                    codigo = 1,
                                    mensaje = "datos insertados",
                                    idusuario = r["idusuario"].ToString()
                                };
                                result.objectResult = resultado;

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
            
            //result.objectResult = parametros;

        }


    }
}
