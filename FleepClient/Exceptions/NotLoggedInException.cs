using System;
using System.Runtime.Serialization;

namespace FleepClient.Exceptions
{
[Serializable]
public class NotLoggedInException : Exception
{
    public NotLoggedInException()
        : base() { }
    
    public NotLoggedInException(string message)
        : base(message) { }
    
    public NotLoggedInException(string format, params object[] args)
        : base(string.Format(format, args)) { }
    
    public NotLoggedInException(string message, Exception innerException)
        : base(message, innerException) { }
    
    public NotLoggedInException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException) { }
    
    protected NotLoggedInException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
}
