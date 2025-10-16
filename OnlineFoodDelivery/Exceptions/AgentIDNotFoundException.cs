namespace OnlineFoodDelivery.Exceptions
{
    public class AgentIDNotFoundException : ApplicationException
    {
        public AgentIDNotFoundException() { }
        public AgentIDNotFoundException(string message) : base(message) { }
    }
}
