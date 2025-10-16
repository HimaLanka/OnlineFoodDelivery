using System;

namespace OnlineFoodDelivery.Exceptions
{
    public class OrderNotFoundException : ApplicationException
    {
        public OrderNotFoundException(string message) : base(message) { }
    }
}

