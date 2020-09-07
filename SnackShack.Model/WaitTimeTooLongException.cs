using System;
using System.Runtime.Serialization;

namespace SnackShack.Model
{
    [Serializable]
    public class WaitTimeTooLongException : Exception
    {
        public WaitTimeTooLongException()
        {
        }

        public WaitTimeTooLongException(string message) : base(message)
        {
        }

        public WaitTimeTooLongException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WaitTimeTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}