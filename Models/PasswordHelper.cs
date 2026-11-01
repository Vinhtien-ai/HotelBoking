using System.Security.Cryptography;
using System.Text;
namespace HotelBoking.Models
{
    public static class PasswordHelper
    {
        public static string Hash(string value)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(value ?? ""));
                var result = new StringBuilder();
                foreach (var b in bytes) result.Append(b.ToString("x2"));
                return result.ToString();
            }
        }
    }
}