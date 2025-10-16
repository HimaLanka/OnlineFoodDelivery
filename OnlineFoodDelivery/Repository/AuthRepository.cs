using System;
using   OnlineFoodDelivery.Model;
using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Data;


namespace OnlineFoodDelivery.Repository
{
    public class AuthRepository : IAuthRepository
    {

        private readonly OnlineFoodDeliveryContext _context;

        public AuthRepository(OnlineFoodDeliveryContext context)

        {
            _context = context;

        }

        public bool Exists(string email, long phone)
        {
            return _context.User.Any(u => u.Email == email || u.PhoneNumber == phone);
        }

        public void Register(User us1)
        {
            _context.User.Add(us1);
            _context.SaveChanges();
        }

        public User GetByCredentials(string emailOrPhone, string password,string role)
        {
            return _context.User.FirstOrDefault(u =>
                (u.Email == emailOrPhone || u.PhoneNumber.ToString() == emailOrPhone)
&& u.Password == password &&u.Role==role);


        }
        public List<User> GetUserDetails()
        {
            return _context.User.ToList();

        }
        public User GetUserById(int Id)
        {
            return _context.User.FirstOrDefault(x => x.Id == Id);
        }
        public User GetUserByEmail(string email)
        {
            return _context.User.FirstOrDefault(x => x.Email == email);
        }
        public bool DeleteUserById(User user)
        {
            _context.User.Remove(user);
            _context.SaveChanges();
            return true;
        }
        public bool DeleteUserByEmail(User user)
        {
            _context.User.Remove(user);
            _context.SaveChanges();
            return true;
        }
        public int UpdateUserById(int id, User user)
        {
            var existingUser = _context.User.Find(id);
            if (existingUser == null)
            {
                return 0; // Or throw a UserNotFoundException
            }

            // Update fields
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.ConfirmPassword = user.ConfirmPassword;
            existingUser.Role = user.Role;

            return _context.SaveChanges();
        }
        //difference between find of first or default(find works on primary key but first or default on any)
        //find throws exception if not found but first or default throw null value or any based on datatype we specify

        public int UpdateUserByEmail(string email, User user)
        {
            var existingUser = _context.User.FirstOrDefault(u => u.Email == email);
            if (existingUser == null)
            {
                return 0; // Or throw a UserNotFoundException
            }

            // 🔐 Check for duplicate email (excluding current user)
            if (_context.User.Any(u => u.Email == user.Email && u.Id != existingUser.Id))
                throw new System.Exception("Email already in use");

            // ✅ Update fields
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.ConfirmPassword = user.ConfirmPassword;
            existingUser.Role = user.Role;

            return _context.SaveChanges();
        }

        public bool UpdatePasswordByEmail(string email, string newPassword)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == email);
            if (user == null) return false;

            user.Password = newPassword;
            user.ConfirmPassword = newPassword;
            _context.SaveChanges();
            return true;
        }





    }

}

    



