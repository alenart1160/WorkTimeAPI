using Microsoft.AspNetCore.Identity;
using WorkTimeAPI.Model;

namespace WorkTimeAPI.Models
{
    public interface IPasswordHelper
    {
        bool VerifyPassword(User user, string hashedPassword, string password);
    }

    public class PasswordHelper : IPasswordHelper
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public PasswordHelper(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string GeneratePassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }


        public bool VerifyPassword(User user, string hashedPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
            // if required, you can handle if result is SuccessRehashNeeded
            return result == PasswordVerificationResult.Success;
        }
    }

}
