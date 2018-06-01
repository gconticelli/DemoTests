namespace DemoTests.SharedLibrary.Exceptions
{
    using System;

    public class DatabaseException: Exception
    {
        public DatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DatabaseException() : base()
        {
        }

        public DatabaseException(string message) : base(message)
        {
        }

        protected DatabaseException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
