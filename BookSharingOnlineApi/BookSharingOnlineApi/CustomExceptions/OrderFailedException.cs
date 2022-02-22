using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.CustomExceptions
{
    public class OrderFailedException : Exception
    {
        public OrderFailedException()
        {
        }

        public OrderFailedException(string message) : base(message)
        {
        }

        public OrderFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
