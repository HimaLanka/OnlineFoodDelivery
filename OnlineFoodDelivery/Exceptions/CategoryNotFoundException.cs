namespace OnlineFoodDelivery.Exceptions
{
    public class CategoryNotFoundException : ApplicationException
    {

        public CategoryNotFoundException() { }
        
        public CategoryNotFoundException(string message) : base(message) { }
    }
}
