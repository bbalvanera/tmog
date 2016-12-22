using System;
using System.Runtime.Serialization;

namespace TMog.WowApi
{
    [Serializable]
    internal class WowProviderException : Exception
    {
        public WowProviderException()
        {
        }

        public WowProviderException(string message) : base(message)
        {
        }

        public WowProviderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WowProviderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}