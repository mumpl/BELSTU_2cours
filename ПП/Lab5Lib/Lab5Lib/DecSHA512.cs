using System;
using System.Security.Cryptography;
using System.Text;

namespace Lab5Lib
{
    public class DecSHA512 : Decorator
    {
        public DecSHA512(IWriter writer) : base(writer) { }
        public override string? Save(string message)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(message ?? string.Empty);
                byte[] hashBytes = sha512.ComputeHash(inputBytes);
                string hashed = Convert.ToBase64String(hashBytes);
                return writer?.Save($"{message}{Lab5Lib.Constant.Delimiter}{hashed}");
            }
        }
    }
    //7 вопрос
    public class DecSHA256 : Decorator
    {
        public DecSHA256(IWriter writer) : base(writer) { }
        public override string? Save(string message)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(message ?? string.Empty);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                string hashed = Convert.ToBase64String(hashBytes);
                return writer?.Save($"{message}{Lab5Lib.Constant.Delimiter}{hashed}");
            }
        }
    }

}
