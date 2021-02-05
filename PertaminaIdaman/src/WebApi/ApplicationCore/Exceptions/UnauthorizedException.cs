using System;

namespace ApplicationCore.Exceptions
{
    class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("You are not authorized.")
        {
        }

        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}