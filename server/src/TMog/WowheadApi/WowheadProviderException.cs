using System;

namespace TMog.WowheadApi
{

    [System.Serializable]
    public class WowheadProviderException : Exception
    {
        public WowheadProviderException() { }
        public WowheadProviderException(string message) : base(message) { }
        public WowheadProviderException(string message, Exception inner) : base(message, inner) { }
        protected WowheadProviderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
