using System.Globalization;
using System.Net;

namespace ApiFoto.Infrastructure.Communication.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; set; }
        public List<string> Args { get; set; }

        public AppException() : base()
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public AppException(string message)
            : base(String.Format(CultureInfo.CurrentCulture, message))
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
        }

        public AppException(string message, int statusCode)
            : base(String.Format(CultureInfo.CurrentCulture, message))
        {
            StatusCode = statusCode;
        }

        public AppException(string message, int statusCode, List<string> args)
            : base(String.Format(CultureInfo.CurrentCulture, message))
        {
            StatusCode = statusCode;
            Args = args;
        }
    }
}
