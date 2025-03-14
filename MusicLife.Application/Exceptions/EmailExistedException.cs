using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using MusicLife.Domain.Enums;
namespace MusicLife.Application.Exceptions
{
    public class EmailExistedException:HttpException
    {
        public EmailExistedException(string message="Email already exists")
            :base(HttpStatusCode.Conflict,ExceptionEnum.Duplicate,message)
        {
        }
    }
}
