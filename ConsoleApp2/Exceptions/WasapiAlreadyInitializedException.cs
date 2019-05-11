using System;
using System.Collections.Generic;
using System.Text;

namespace Processing.Exceptions
{
    class WasapiAlreadyInitializedException : Exception
    {
        public WasapiAlreadyInitializedException()
        {

        }

        public WasapiAlreadyInitializedException(string message)
            : base(message)
        {

        }

        public WasapiAlreadyInitializedException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
