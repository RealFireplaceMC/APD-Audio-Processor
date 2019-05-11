using System;
using System.Collections.Generic;
using System.Text;

namespace Processing.Exceptions
{
    class InvalidTypeException : Exception
    {
        public InvalidTypeException()
        {

        }

        public InvalidTypeException(string message)
            : base(message)
        {

        }

        public InvalidTypeException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
