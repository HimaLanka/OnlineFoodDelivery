namespace OnlineFoodDelivery.Exceptions
{
    public class EmailDeliveryException : ApplicationException
    {
        public EmailDeliveryException() { }
        public EmailDeliveryException(string message) : base(message) { }
    }
}
