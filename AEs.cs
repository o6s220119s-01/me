
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class AesEncryptionExample
{
    public static byte[] Encrypt(string plainText, byte[] key, byte[] iv)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return msEncrypt.ToArray();
                }
            }
        }
    }

    public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    public static void Main(string[] args)
    {
        string originalText = "This is the text to be encrypted!";
        byte[] key = new byte[32]; // 256-bit key size for AES
        byte[] iv = new byte[16]; // 128-bit IV size for AES

        // Generate random key and IV (for demonstration purposes)
        using (Aes aes = Aes.Create())
        {
            aes.GenerateKey();
            aes.GenerateIV();
            Array.Copy(aes.Key, key, 32);
            Array.Copy(aes.IV, iv, 16);
        }

        byte[] encryptedBytes = Encrypt(originalText, key, iv);
        string decryptedText = Decrypt(encryptedBytes, key, iv);

        Console.WriteLine("Original Text: " + originalText);
        Console.WriteLine("Encrypted Text (Base64): " + Convert.ToBase64String(encryptedBytes));
        Console.WriteLine("Decrypted Text: " + decryptedText);
    }
}  encrypted bytes, key, and IV to decrypt the text. The `Main` method generates a random key and IV, encrypts the text, and then decrypts it back to its original form.
