using System;

namespace TMog.Services.Exceptions
{

    [Serializable]
    public class EntityNotFoundException : ServiceException
    {
        public EntityNotFoundException() { }
        public EntityNotFoundException(string message) : base(message) { }
        public EntityNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected EntityNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public string EntityName { get; internal set; }

        public string ItemNotFound { get; internal set; }
    }
}
