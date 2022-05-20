using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNL.Net.Utilities.ResponseUtil
{
    public class ResponseModel
    {
        public object data { get; set; }
        public string message { get; set; }
        public ErrorCodes code { get; set; }
        public bool success { get; set; }
        public static ResponseModel Result(string message, object data, ErrorCodes errorCodes)
        {
            return new ResponseModel
            {
                code = errorCodes,
                data = data,
                message = message,
                success = errorCodes == ErrorCodes.Successful ? true : false
            };
        }
        public static ResponseModel Failed(string message, object data)
        {
            return new ResponseModel
            {
                code = ErrorCodes.Failed,
                data = data,
                message = message,
                success = false
            };
        }
        public static ResponseModel Successful(string message, object data)
        {
            return new ResponseModel
            {
                code = ErrorCodes.Successful,
                data = data,
                message = message,
                success = true
            };
        }

        public static ResponseModel ServerError(string message, object data)
        {
            return new ResponseModel
            {
                code = ErrorCodes.ServerError,
                data = data,
                message = message,
                success = false
            };
        }
    }
    public class ResponseModel<TData>
    {
        public ResponseModel()
        {
            success = false;
        }
        public TData data { get; set; }
        public string message { get; set; }
        public ErrorCodes code { get; set; }
        public bool success { get; set; }
    }
    public enum ErrorCodes
    {
        Successful = 200,
        Failed = 400,
        ServerError = 500,
        ValidDataRequired = 3,
        DataNotFound = 4
    }
}
