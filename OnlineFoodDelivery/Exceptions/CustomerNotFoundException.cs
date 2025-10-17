namespace OnlineFoodDelivery.Exceptions
{
    public class CustomerNotFoundException : ApplicationException
    {
        public CustomerNotFoundException() { }
        public CustomerNotFoundException(string message) : base(message) { }
    }
}
