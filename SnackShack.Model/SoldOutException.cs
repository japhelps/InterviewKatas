using System;
using System.Runtime.Serialization;

namespace SnackShack.Model
{
    [Serializable]
    public class SoldOutException : Exception
    {
        public SoldOutException()
        {
        }

        public SoldOutException(string message) : base(message)
        {
        }

        public SoldOutException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SoldOutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}