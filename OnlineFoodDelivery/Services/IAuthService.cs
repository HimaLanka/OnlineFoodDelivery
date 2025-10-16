using OnlineFoodDelivery.Model;


namespace OnlineFoodDelivery.Services
{
    public interface IAuthService
    {
        string Register(User us);
        string Login(LoginRequest usLog);
        public List<User> GetAllUsers();
        public User GetUserById(int Id);
        public User GetUserByEmail(string Email);
        public bool DeleteUserById(int id);
        public bool DeleteUserByEmail(string Email);
        public int UpdateUserById(int id, User user);
        public int UpdateUserByEmail(string email, User user);
        public string ForgotPassword(string email, string newPassword);





    }
}
