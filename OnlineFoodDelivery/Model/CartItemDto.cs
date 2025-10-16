namespace OnlineFoodDelivery.Model
{
    public class CartItemDto
    {
        public int UserId { get; set; }
        public long MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}
