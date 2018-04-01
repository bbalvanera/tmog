using System;

namespace TMog.Services.Exceptions
{

    [Serializable]
    public class DuplicateEntityException : ServiceException
    {
        public DuplicateEntityException() { }
        public DuplicateEntityException(string message) : base(message) { }
        public DuplicateEntityException(string message, Exception inner) : base(message, inner) { }
        protected DuplicateEntityException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
