using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace System
{
    /// <summary>
    /// Clase de herramientas.
    /// </summary>
   
    public static class Helpers
    {

        /// <summary>
        /// Convierte un string a Intero si es null envia 0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int StringToInt(this string str)
        {
            int result;
            if (int.TryParse(str, out result))
                return result;
            return 0;
        }
        /// <summary>
        /// Convierte un int? a int si es null envia 0
        /// </summary>
        /// <returns></returns>
        public static int NullToInt(this int? @int) => (@int == null) ? 0 : @int.Value;
    }
}
