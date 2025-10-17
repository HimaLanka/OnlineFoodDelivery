namespace OnlineFoodDelivery.Exceptions
{
    public class DeliveryByIDNotFoundException : ApplicationException
    {
        public DeliveryByIDNotFoundException() { }
        public DeliveryByIDNotFoundException(string message) : base(message) { }
    }
}
