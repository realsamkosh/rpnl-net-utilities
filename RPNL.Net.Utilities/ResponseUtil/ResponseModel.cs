﻿using System.Linq;
using System.Text.Json;

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
                success = errorCodes == ErrorCodes.Successful
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
        UnAuthorized = 401,
        TokenExpired = 406,
        ServerError = 500,
        LinkExpired = 600,
        ValidDataRequired = 3,
        DataNotFound = 4
    }
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToUnderscoreCase();
    };


    public static class StringExtensionMethod
    {
        public static string ToUnderscoreCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
