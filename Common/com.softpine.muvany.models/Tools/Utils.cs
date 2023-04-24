using System.Text.RegularExpressions;

namespace com.softpine.muvany.models.Tools
{
    /// <summary>
    /// Clases para los procesos genericos
    /// </summary>
    public class Utils
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");

        /// <summary>
        /// Funcionalidad para reemplazar los espacios en blanco ó por el parametro que mandemos como el remplazo 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string ReplaceWhitespace(string input, string replacement = "")
        {
            return sWhitespace.Replace(input, replacement);
        }
    }
}
