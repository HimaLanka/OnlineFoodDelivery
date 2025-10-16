namespace OnlineFoodDelivery.Exceptions
{
        public class CartItemNotFoundException : ApplicationException
        {
            public CartItemNotFoundException(string message) : base(message) { }
        }
}
