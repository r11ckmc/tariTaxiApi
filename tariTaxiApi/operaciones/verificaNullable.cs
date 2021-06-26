using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tariTaxiApi.operaciones
{
    public static class verificaNullable
    {
        //public static verificaNullable() { }
        public static int? verificarInt(String valor)
        {

            if (valor != String.Empty)
                return Int32.Parse(valor);
            else
                return 0;

        }

        public static double? verificarDouble(String valor)
        {

            if (valor != String.Empty)
                return Double.Parse(valor);
            else
                return 0;

        }
        public static float? verificarFloat(String valor)
        {

            if (valor != String.Empty)
                return float.Parse(valor);
            else
                return 0;

        }
    }
}
