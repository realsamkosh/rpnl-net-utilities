using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RPNL.Net.Utilities.LoggingUtils
{
    public static class ConsolePrintHelper
    {
        /// <summary>
        /// Print error in console
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="tag"></param>
        public static void PrintInConsole(object statement, string tag = default, params object[] data)
        {
            WrapWithTag(tag: tag, () =>
            {
                if (!(statement is null))
                    Console.WriteLine(statement);

                if (!(data is null) && data.Any())
                    WrapWithTag(tag: "Extra data", () => Console.WriteLine(JsonSerializer.Serialize(data)));
            });
        }

        private static void WrapWithTag(string tag, Action operaton)
        {
            PrintCmdTagLine($"{tag} starts here".ToUpper());
            operaton();
            PrintCmdTagLine($"{tag} ends here".ToUpper());
        }

        private static void PrintCmdTagLine(string tag)
            => Console.WriteLine($"======== {(tag ?? "").Trim()} =========");

        private static void SafePrintObjectInConsole(dynamic statement)
        {
            try
            {
                if (statement.GetType() == typeof(string))
                    Console.WriteLine(statement);
                else
                    Console.WriteLine(JsonSerializer.Serialize(statement));
            }
            catch (Exception)
            {
                Console.WriteLine(statement);
            }
        }
    }
}
