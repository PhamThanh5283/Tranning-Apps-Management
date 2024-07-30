using System.Security.Cryptography;
using System.Text;

namespace Tranning_Apps_Management.Utils
{
    public class PasswordHash
    {
        public string Encrypt(string key, string decrypted)
        {
            //string hash = "Password@2023$";
            //byte[] data = UTF8Encoding.UTF8.GetBytes(decrypted);

            //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //TripleDESCryptoServiceProvider tripDES = new TripleDESCryptoServiceProvider();

            //tripDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            //tripDES.Mode = CipherMode.ECB;

            //ICryptoTransform transform = tripDES.CreateEncryptor();
            //byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            //return Convert.ToBase64String(result);
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(decrypted);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
        public string Decrypt(string key, string encrypted)
        {
            //string hash = "Password@2023$";
            //byte[] data = Convert.FromBase64String(encrypted);

            //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //TripleDESCryptoServiceProvider tripDES = new TripleDESCryptoServiceProvider();

            //tripDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            //tripDES.Mode = CipherMode.ECB;

            //ICryptoTransform transform = tripDES.CreateEncryptor();
            //byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            //return UTF8Encoding.UTF8.GetString(result);
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(encrypted);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
