namespace OnlineFoodDelivery.Exceptions
{
    public class CustomExceptions
    {

    }
    public class OrderNotExistsException : Exception
    {
        public OrderNotExistsException(int orderId)
            : base($"Order with ID {orderId} not found.") { }
    }

    public class PaymentProcessingException : Exception
    {
        public PaymentProcessingException(string message)
            : base($"{message}") { }
    }

    
}
