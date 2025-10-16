using OnlineFoodDelivery.Model;
namespace OnlineFoodDelivery.Repository
{
    public interface IAuthRepository
    {
        bool Exists(string email, long phone);
        void Register(User us1);
        User GetByCredentials(string emailOrPhone, string password,string role);
        //User GetByEmail(string email);
        public List<User> GetUserDetails();
        public User GetUserById(int Id);
        public User GetUserByEmail(string Email);
        public bool DeleteUserById(User user);
        public bool DeleteUserByEmail(User user);
        public int UpdateUserById(int id, User user);
        public int UpdateUserByEmail(string email, User user);
        public bool UpdatePasswordByEmail(string email, string newPassword);



    }
}
