using MusicLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Exceptions
{
    public class DuplicatedException : HttpException
    {
        public DuplicatedException(string message="Duplicated record")
            : base(HttpStatusCode.Conflict, ExceptionEnum.Duplicate, message)
        {
        }
    }
}
