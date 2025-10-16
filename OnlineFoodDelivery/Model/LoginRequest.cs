namespace OnlineFoodDelivery.Model
{
    public class LoginRequest
    {
        public string EmailOrPhone { get; set; }
        public string Password { get; set; }
        public string Role {  get; set; }
    }
}
