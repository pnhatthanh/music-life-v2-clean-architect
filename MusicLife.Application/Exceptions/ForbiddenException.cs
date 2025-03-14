using MusicLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Exceptions
{
    public class ForbiddenException:HttpException
    {
        public ForbiddenException(string message="Forbidden")
            :base(HttpStatusCode.Forbidden,ExceptionEnum.Forbidden,message)
        {
        }
    }
}
