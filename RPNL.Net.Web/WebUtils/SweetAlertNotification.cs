using Microsoft.AspNetCore.Mvc;
using System;

namespace RPNL.Net.Utilities.WebUtils
{
    public static class SweetAlert 
    {
        public static void AddSweetAlert(this Controller controller, string message, String notificationType)
        {
            var msg = "swal('" + notificationType.ToString().ToUpper() + "', '" + message + "','" + notificationType + "')" + "";
            controller.TempData["notification"] = msg;
        }
    }
}
