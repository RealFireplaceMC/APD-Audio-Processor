using System;
using System.Collections.Generic;
using System.Text;

namespace Processing.Exceptions
{
    public class WasapiNotInitializedException : Exception
    {

        public WasapiNotInitializedException()
        {

        }

        public WasapiNotInitializedException(string message)
            : base(message)
        {

        }

        public WasapiNotInitializedException(string message, Exception inner)
            : base(message, inner)
        {

        }

    }
}
