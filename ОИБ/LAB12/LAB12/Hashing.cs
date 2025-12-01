using System.Security.Cryptography;
using System.Text;

public class HashingSHA256
{
    public string ComputeHash(string text)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}