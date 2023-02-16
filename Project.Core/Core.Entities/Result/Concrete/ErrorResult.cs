using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Core.Entities.Result.Concrete
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message)
        {
        }
        public ErrorResult(HttpStatusCode statusCode, string message) : base(false, message,statusCode)
        {
        }
        public ErrorResult() : base(false)
        {
        }
    }
}
