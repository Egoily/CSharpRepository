using System;

namespace ee.Core.Exceptions
{
    public class DevelopmentException : Exception
    {

        public DevelopmentException(string message)
            : base(message)
        {

        }
    }
}
