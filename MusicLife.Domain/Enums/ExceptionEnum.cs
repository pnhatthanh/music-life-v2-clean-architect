using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Domain.Enums
{
    public class ExceptionEnum
    {
        public const string BadRequest = "BAD_REQUEST";
        public const string NotFound = "NOT_FOUND";
        public const string Unauthorized = "UNAUTHORIZED";
        public const string Internal = "INTERNAL";
        public const string Duplicate = "DUPLICATE";
        public const string Forbidden = "FORBIDDEN";
    }
}
