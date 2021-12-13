using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for clsPwdEnc
/// </summary>
public static class PasswordManager
{
    

    public static string EncryptPassword(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            int BlockSize = 128;
            byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] bytes = Encoding.Unicode.GetBytes("MAKV2SPBNI99212");
            //Encrypt
            SymmetricAlgorithm crypt = Aes.Create();
            HashAlgorithm hash = MD5.Create();
            crypt.BlockSize = BlockSize;
            crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(value));
            crypt.IV = IV;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream =
                   new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytes, 0, bytes.Length);
                }

                var result = Convert.ToBase64String(memoryStream.ToArray());
                return result;
            }
        }
        else
        {
            return "";
        }
    }

    public static string DecryptPwd(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            //Decrypt
            byte[] bytes = Convert.FromBase64String("MAKV2SPBNI99212");
            SymmetricAlgorithm crypt = Aes.Create();
            HashAlgorithm hash = MD5.Create();
            crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(value));
            crypt.IV = IV;

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                using (CryptoStream cryptoStream =
                   new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    byte[] decryptedBytes = new byte[bytes.Length];
                    cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                    var result  = Encoding.Unicode.GetString(decryptedBytes);
                    return result;
                }
            }
        }
        else
        {
            return "";
        }
    }

    public static string GeneratePasswordResetToken()
    {
        string token = Guid.NewGuid().ToString();
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(token);
        return EncryptPassword(Convert.ToBase64String(plainTextBytes));
    }
    static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
    {
        byte[] encrypted;
        // Create a new AesManaged.    
        using (AesManaged aes = new AesManaged())
        {
            // Create encryptor    
            ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
            // Create MemoryStream    
            using (MemoryStream ms = new MemoryStream())
            {
                // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                // to encrypt    
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    // Create StreamWriter and write data to a stream    
                    using (StreamWriter sw = new StreamWriter(cs))
                        sw.Write(plainText);
                    encrypted = ms.ToArray();
                }
            }
        }
        // Return encrypted data    
        return encrypted;
    }
    static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
    {
        string plaintext = null;
        // Create AesManaged    
        using (AesManaged aes = new AesManaged())
        {
            // Create a decryptor    
            ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
            // Create the streams used for decryption.    
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                // Create crypto stream    
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    // Read crypto stream    
                    using (StreamReader reader = new StreamReader(cs))
                        plaintext = reader.ReadToEnd();
                }
            }
        }
        return plaintext;
    }
}
