using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.Models
{
    public class Result
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Object objectResult { get; set; }

        #region Constructor y Destructor.
        /// <summary>
        /// Constructor.
        /// </summary>
        public Result()
        {
            Status = 0;
            objectResult = null;
            Message = string.Empty;
        }

        /// <summary>
        /// Destructor.
        /// Se invoca al Garbage Collector para liberar los recursos de la memoria.
        /// </summary>
        ~Result()
        {
            GC.Collect();
        }
        #endregion

    }

}
