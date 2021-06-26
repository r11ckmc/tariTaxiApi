using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models.DataBase
{
    public class ConnectionDBModel
    {
        public string GetConnection()
        {
            try
            {
                string connection = Startup.Configuration.GetConnectionString("ConnectionDB");

                return connection;

            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
