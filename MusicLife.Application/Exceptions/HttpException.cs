using System.Net;

namespace MusicLife.Application.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public object Response { get; set; }
        public HttpException(HttpStatusCode statusCode, string code, string message)
            : base(message)
        {
            StatusCode = statusCode;
            Response = new
            {
               statusCode =(int) statusCode,
               code,
               message
            };
            
        }

    }
}
