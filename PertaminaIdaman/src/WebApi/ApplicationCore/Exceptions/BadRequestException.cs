using System;

namespace ApplicationCore.Exceptions
{
    class BadRequestException : Exception
    {
        public object Errors { get; set; }

        public BadRequestException() : base("There is something wrong with the request.")
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}