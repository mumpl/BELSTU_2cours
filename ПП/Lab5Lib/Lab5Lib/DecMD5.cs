using System;
using System.Text;
using System.Security.Cryptography;
using System.Reflection.Metadata;

namespace Lab5Lib
{
    public class DecMD5 : Decorator
    {
        public DecMD5(IWriter writer) : base(writer) { }
        public override string Save(string message)
        {
            using (var md5 = MD5.Create())
            {
                byte[] data = Encoding.UTF8.GetBytes(message ?? string.Empty);
                byte[] encryptData = md5.ComputeHash(data);
                string hashedMessage = Convert.ToBase64String(encryptData);
                return writer?.Save($"{message}{Constant.Delimiter}{hashedMessage}");
            }
        }
    }
}

