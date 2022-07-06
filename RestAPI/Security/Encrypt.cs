using System.Security.Cryptography;
using System.Text;

namespace RestAPI.Security
{
    public class Encrypt
    {
        public static string GetSHA256(string str)
        {
            SHA256 sHA256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new();
            byte[] buffer;
            buffer = sHA256.ComputeHash(encoding.GetBytes(str));
            StringBuilder strBuilder = new();
            for (int i = 0; i < buffer.Length; i++) strBuilder.Append(buffer[i]);
            return strBuilder.ToString();
        }
    }
}
