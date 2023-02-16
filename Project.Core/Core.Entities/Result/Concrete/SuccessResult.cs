using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Core.Entities.Result.Concrete
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {
        }
        public SuccessResult(HttpStatusCode statusCode,string message):base(true,message,statusCode)
        {

        }

        public SuccessResult() : base(true)
        {
        }
    }
}
