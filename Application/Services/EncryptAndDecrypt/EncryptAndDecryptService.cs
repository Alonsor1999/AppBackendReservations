using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.EncryptAndDecrypt;
using Microsoft.Extensions.Configuration;

namespace Application.Services.EncryptAndDecrypt
{
    public class EncryptAndDecryptService : IEncryptAndDecryptService
    {
        private readonly string _SecretKeyEncrypted;
        public EncryptAndDecryptService(IConfiguration config)
        {
            _SecretKeyEncrypted = config["SecretKey"];
        }

        public string Encrypt(string data)
        {

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(_SecretKeyEncrypted);

                aesAlg.IV = new byte[16] { 54, 169, 79, 111, 118, 220, 207, 247, 59, 230, 40, 55, 1, 35, 80, 98 };

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(data);
                    }
                    return Convert.ToBase64String(aesAlg.IV) + Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string Decrypt(string encryptedData)
        {
            string ivBase64 = encryptedData.Substring(0, 24);
            string encryptedBase64 = encryptedData.Substring(24);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(_SecretKeyEncrypted);
                aesAlg.IV = Convert.FromBase64String(ivBase64);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedBase64)))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

    }
}
