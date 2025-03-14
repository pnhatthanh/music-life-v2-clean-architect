using MusicLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Exceptions
{
    public class InternalException : HttpException
    {
        public InternalException(string message="Internal server error")
            :base(HttpStatusCode.InternalServerError,ExceptionEnum.Internal,message)
        {
        }
    }
}
