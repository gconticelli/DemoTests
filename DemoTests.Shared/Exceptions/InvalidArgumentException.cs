namespace DemoTests.SharedLibrary.Exceptions
{
    using System;

    public class InvalidArgumentException: Exception
    {
        public InvalidArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidArgumentException() : base()
        {
        }

        public InvalidArgumentException(string message) : base(message)
        {
        }

        protected InvalidArgumentException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
