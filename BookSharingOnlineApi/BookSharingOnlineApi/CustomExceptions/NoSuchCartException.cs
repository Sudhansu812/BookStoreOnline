using System;
using System.Runtime.Serialization;

namespace BookSharingOnlineApi.CustomExceptions
{
    [Serializable]
    internal class NoSuchCartException : Exception
    {
        public NoSuchCartException()
        {
        }

        public NoSuchCartException(string message) : base(message)
        {
        }

        public NoSuchCartException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSuchCartException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}