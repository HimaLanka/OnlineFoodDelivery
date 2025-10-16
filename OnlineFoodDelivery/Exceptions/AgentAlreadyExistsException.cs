namespace OnlineFoodDelivery.Exceptions
{
    public class AgentAlreadyExistsException : ApplicationException
    {
        public AgentAlreadyExistsException() { }
        public AgentAlreadyExistsException(string message) : base(message) { }
    }
}