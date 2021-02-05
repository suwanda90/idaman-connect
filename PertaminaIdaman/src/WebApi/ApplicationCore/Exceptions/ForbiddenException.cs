using System;

namespace ApplicationCore.Exceptions
{
    class ForbiddenException : Exception
    {
        public ForbiddenException() : base("You are forbidden.")
        {
        }

        public ForbiddenException(string message) : base(message)
        {
        }

        public ForbiddenException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}