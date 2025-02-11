using System.Runtime.Serialization;

namespace NerdStore.Core.Exceptions
{
    public class DomainException : SystemException
    {
        public DomainException()
        {
        }

        public DomainException(string? message) : base(message)
        {
        }

        public DomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
