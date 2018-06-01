namespace DemoTests.SharedLibrary.Exceptions
{
    using System;

    public class NotFoundException: Exception
    {
        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotFoundException() : base()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        protected NotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
