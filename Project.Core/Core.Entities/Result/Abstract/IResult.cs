using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Core.Entities.Result.Abstract
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
        HttpStatusCode StatusCode { get; }
    }
}
