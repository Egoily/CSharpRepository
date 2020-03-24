using System;
using System.Collections.Generic;

namespace ee.Core.Exceptions
{
    public interface IAggregateException
    {
        string StackTrace { get; }
        string Message { get; }
        IList<Exception> InnerExceptions { get; }
        Exception InnerException { get; }
    }
}
