using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.CustomExceptions
{
    public class AlreadyRatedException : Exception
    {
        public AlreadyRatedException()
        {
        }

        public AlreadyRatedException(string message) : base(message)
        {
        }

        public AlreadyRatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyRatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
