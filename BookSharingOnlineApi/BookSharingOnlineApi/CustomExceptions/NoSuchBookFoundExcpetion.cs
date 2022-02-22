using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.CustomExceptions
{
    public class NoSuchBookFoundExcpetion : Exception
    {
        public NoSuchBookFoundExcpetion()
        {
        }

        public NoSuchBookFoundExcpetion(string message) : base(message)
        {
        }

        public NoSuchBookFoundExcpetion(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSuchBookFoundExcpetion(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
