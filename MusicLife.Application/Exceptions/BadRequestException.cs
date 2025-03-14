using MusicLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Exceptions
{
    public class BadRequestException : HttpException
    {
        public BadRequestException(string message="Bad request")
            :base(HttpStatusCode.BadRequest,ExceptionEnum.BadRequest,message)
        {
        }
    }
}
