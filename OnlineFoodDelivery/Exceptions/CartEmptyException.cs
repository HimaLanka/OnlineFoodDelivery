namespace OnlineFoodDelivery.Exceptions
{
        public class CartEmptyException : ApplicationException
        {
            public CartEmptyException(string message) : base(message) { }
        }
    
}
