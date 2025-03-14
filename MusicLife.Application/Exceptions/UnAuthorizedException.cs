using MusicLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Exceptions
{
    public class UnAuthorizedException:HttpException
    {
        public UnAuthorizedException(string message="Unauthorized")
            :base(HttpStatusCode.Unauthorized,ExceptionEnum.Unauthorized,message)
        { 
        }
    }
}
