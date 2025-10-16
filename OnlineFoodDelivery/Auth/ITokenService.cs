using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Auth
{
    public interface ITokenService
    {
        string CreateToken(User us);
    }
}
