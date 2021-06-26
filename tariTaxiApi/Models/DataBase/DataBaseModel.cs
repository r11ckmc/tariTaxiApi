using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models
{
    public class DataBaseModel
    {
        public IConfiguration _configuration { get; set; }
        public string connecion { get; set; }
        public string user { get; set; }
        public string password { get; set; }
    }
}
