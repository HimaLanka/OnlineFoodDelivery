namespace OnlineFoodDelivery.Exceptions
{
    public class AgentsNotAvailableException : ApplicationException
    {
        public AgentsNotAvailableException() { }
        public AgentsNotAvailableException(string message) : base(message) { }
    }
}
