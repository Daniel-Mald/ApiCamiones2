using System.Security.Cryptography;
using System.Text;

namespace CamionesAPI.Helpers
{
    public class Encriptacion
    {
        public static string CalculateSHA256(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = SHA256.HashData(bytes);
            StringBuilder builder = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}