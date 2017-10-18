using System;

namespace Services.BusinessLogic
{
    [Serializable]
    internal class BadDateException:Exception
    {
        public BadDateException()
        {
        }

        public BadDateException(string message) : base(message)
        {
        }

        public BadDateException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}