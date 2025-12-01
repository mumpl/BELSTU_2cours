using System.Security.Cryptography;

class MainProgram
{
    static void Main(string[] args)
    {
        string surname = "Авчинник Елизавета Сергеевна";
        string alteredSurname = "Авчинник Елизавета Сергеевна";

        CryptographyAES aes = new CryptographyAES();
        HashingSHA256 sha256 = new HashingSHA256();
        DigitalSignature ds = new DigitalSignature();
        FileManager fileManager = new FileManager();

        byte[] key = new byte[16];
        byte[] iv = new byte[16];

        RandomNumberGenerator.Fill(key);
        RandomNumberGenerator.Fill(iv);

        fileManager.SaveToFile("Key.txt", Convert.ToBase64String(key));
        fileManager.SaveToFile("IV.txt", Convert.ToBase64String(iv));

        // Сохранение ключей в HEX-формате
        fileManager.SaveToFile("Key.hex", BitConverter.ToString(key).Replace("-", ""));
        fileManager.SaveToFile("IV.hex", BitConverter.ToString(iv).Replace("-", ""));

        string encryptedSurname = aes.Encrypt(surname, key, iv);
        fileManager.SaveToFile("EncryptedSurname.txt", encryptedSurname);

        // Сохранение зашифрованных данных в HEX-формате
        byte[] encryptedBytes = Convert.FromBase64String(encryptedSurname);
        fileManager.SaveToFile("EncryptedSurname.hex", BitConverter.ToString(encryptedBytes).Replace("-", ""));

        string decryptedSurname = aes.Decrypt(encryptedSurname, key, iv);
        Console.WriteLine($"Расшифрованное имя: {decryptedSurname}");

        // Хеширование фамилии
        string hash = sha256.ComputeHash(surname);
        fileManager.SaveToFile("Hash.txt", hash);

        string alteredHash = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF"; // Неправильный хэш

        string readHash = fileManager.ReadFromFile("Hash.txt");

        bool isOriginalValid = ds.VerifyMessage(surname, surname, hash, readHash);
        Console.WriteLine($"Проверка оригинального сообщения и хэша: {isOriginalValid}");

        bool isAlteredMessageValid = ds.VerifyMessage(surname, alteredSurname, hash, readHash);
        Console.WriteLine($"Проверка изменённого сообщения: {isAlteredMessageValid}");

        bool isAlteredHashValid = ds.VerifyMessage(surname, surname, hash, alteredHash);
        Console.WriteLine($"Проверка изменённого хэша: {isAlteredHashValid}");
    }
}