using OnlineFoodDelivery.Auth;
using OnlineFoodDelivery.Exceptions;
using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.Repository;

namespace OnlineFoodDelivery.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public string Register(User user)
        {
            if (_repository.Exists(user.Email, user.PhoneNumber))
                return "Email or phone already registered";

            _repository.Register(user);
            return "User registered successfully";
        }

        public string Login(LoginRequest request)
        {
            var user = _repository.GetUserByEmail(request.EmailOrPhone)
                       ?? _repository.GetUserDetails().FirstOrDefault(u => u.PhoneNumber.ToString() == request.EmailOrPhone);

            if (user == null)
                return "Invalid email or phone";

            if (user.Password != request.Password)
                return "Invalid password";

            if (user.Role != request.Role)
                return "Invalid role";

            return _tokenService.CreateToken(user);
        }


        public List<User> GetAllUsers()
        {
            return _repository.GetUserDetails();
        }

        public User GetUserById(int id)
        {
            var user = _repository.GetUserById(id);
            if (user == null)
                throw new UserNotFoundException($"User with ID {id} does not exist.");
            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = _repository.GetUserByEmail(email);
            if (user == null)
                throw new UserNotFoundException($"User with email {email} does not exist.");
            return user;
        }

        public bool DeleteUserById(int id)
        {
            var user = _repository.GetUserById(id);
            if (user == null)
                throw new UserNotFoundException($"User with ID {id} does not exist.");
            return _repository.DeleteUserById(user);
        }

        public bool DeleteUserByEmail(string email)
        {
            var user = _repository.GetUserByEmail(email);
            if (user == null)
                throw new UserNotFoundException($"User with email {email} does not exist.");
            return _repository.DeleteUserByEmail(user);
        }

        public int UpdateUserById(int id, User user)
        {
            var existingUser = _repository.GetUserById(id);
            if (existingUser == null)
                throw new UserNotFoundException($"User with ID {id} does not exist.");
            return _repository.UpdateUserById(id, user);
        }
        public int UpdateUserByEmail(string email, User user)
        {
            var existingUser = _repository.GetUserByEmail(email);
            if (existingUser == null)
                throw new UserNotFoundException($"User with {email} does not exists");
            return (_repository.UpdateUserByEmail(email, user));

        }
        public string ForgotPassword(string email, string newPassword)
        {
            bool updated = _repository.UpdatePasswordByEmail(email, newPassword);
            return updated ? "Password updated successfully" : "User not found";
        }






    }
}
