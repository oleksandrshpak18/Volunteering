using Volunteering.Data.Models;

namespace Volunteering.Helpers
{
    public static class HashProcessor
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        public static bool VerifyPassword(string existingPassword, string enteredPassword)
        { 
            return BCrypt.Net.BCrypt.Verify(enteredPassword, existingPassword);
        }
    }
}
