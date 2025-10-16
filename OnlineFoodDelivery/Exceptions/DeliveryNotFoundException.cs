namespace OnlineFoodDelivery.Exceptions
{
    public class DeliveryNotFoundException : ApplicationException
    {
        public DeliveryNotFoundException() { }
        public DeliveryNotFoundException(string message) : base(message) { }
    }
}
