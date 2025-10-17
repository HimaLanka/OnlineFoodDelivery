namespace OnlineFoodDelivery.Exceptions
{
    public class AgentNotFoundException : ApplicationException
    {
        public AgentNotFoundException() { }
        public AgentNotFoundException(string message) : base(message){ }
    }
}