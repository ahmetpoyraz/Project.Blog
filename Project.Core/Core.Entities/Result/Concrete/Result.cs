using Project.Core.Core.Entities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Core.Entities.Result.Concrete
{
    public class Result : IResult
    {
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }
        public Result(bool success, string message,HttpStatusCode statusCode) : this(success,message)
        {
            StatusCode = statusCode;
        }

        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
        public string Message { get; }
        public HttpStatusCode StatusCode { get; }
    }
}
