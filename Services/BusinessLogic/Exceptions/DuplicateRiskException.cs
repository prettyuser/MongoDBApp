using System;

namespace Services.BusinessLogic
{
    [Serializable]
    internal class DuplicateRiskException:Exception
    {
        public DuplicateRiskException()
        {
        }

        public DuplicateRiskException(string message) : base(message)
        {
        }

        public DuplicateRiskException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}