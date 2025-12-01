using System;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace Lab5Lib
{
    public class DecRSA : Decorator
    {
        public DecRSA(IWriter writer) : base(writer) { }
        public override string Save(string message)
        {
            string publicKey;
            byte[] encyptData;
            message = message ?? string.Empty;
            int index = message.IndexOf(Constant.Delimiter);
            if (index == -1) throw new Exception("Delimiter not found");
            string temp = message.Substring(0, index);
            string temp2 = message.Substring(index + 1);
            byte[] temp3 = Convert.FromBase64String(temp2);
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                publicKey = rsa.ToXmlString(true);
                rsa.ImportParameters(rsa.ExportParameters(false));
                encyptData = rsa.Encrypt(temp3, false);
            }
            return writer?.Save($"{temp}{Constant.Delimiter}{Convert.ToBase64String(encyptData)}{Constant.Delimiter}{publicKey}");
        }
    }
    //8 вопрос
    public class DecAES : Decorator
    {
        public DecAES(IWriter writer) : base(writer) { }
        public override string Save(string message)
        {
            using (Aes aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] inputBytes = Encoding.UTF8.GetBytes(message ?? string.Empty);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                string encryptedMessage = Convert.ToBase64String(encryptedBytes);
                string keyAndIV = $"{Convert.ToBase64String(aes.Key)}:{Convert.ToBase64String(aes.IV)}";
                return writer?.Save($"{message}{Constant.Delimiter}{encryptedMessage}{Constant.Delimiter}{keyAndIV}");
            }
        }
    }
    //9 вопрос
    public class DecHttp : Decorator
    {
        public DecHttp(IWriter writer) : base(writer) { }
        public override string Save(string message)
        {
            string httpMessage =
                $"HTTP/1.1 200 OK\r\n" +
                $"Content-Type: text/plain\r\n" +
                $"Content-Length: {message.Length}\r\n\r\n" +
                $"{message}";
            return writer?.Save(httpMessage);
        }
    }


}

