using System.Net;

namespace RegistanFerghanaLC.Web.Exceptions
{
    public class StatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public StatusCodeException()
        {

        }
        public StatusCodeException(HttpStatusCode statusCode, string message) :
            base(message)
        {
            StatusCode = statusCode;
        }
    }
}
