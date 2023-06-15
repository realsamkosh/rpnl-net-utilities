using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNL.Net.Utilities.LoggingUtils
{
    public static class ConsoleExtensions
    {
        /// <summary>
        /// Print error in console
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="tag"></param>
        public static void PrintInConsole(this object @object, string tag = default, params object[] data)
            => ConsolePrintHelper.PrintInConsole(statement: @object, tag: tag, data: data);

        /// <summary>
        /// Print error in console
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="tag"></param>
        public static void PrintInConsole(this string text, string tag = default, params object[] data)
            => ConsolePrintHelper.PrintInConsole(statement: text, tag: tag, data: data);
    }
}
