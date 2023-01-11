using System;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.IdentityModel.Tokens;

namespace Utility.Helpers
{
    public sealed class PasswordHasher
    {
        private const string SALT_KEY = "MediaX_XPhone";
        private const string encryptionKey = "SDWJLD1VNG3XM7G";
        private static Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //This security key should be very complex and Random for encrypting the text. This playing vital role in encrypting the text.
        private const string SecurityKey = "FSXDSYZR_FSXDSYZR";
        

        //This method is used to convert the plain text to Encrypted/Un-Readable Text format.
        public static string EncryptPlainTextToCipherText(string PlainText)
        {
            // Getting the bytes of Input String.
            byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);

            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();
            //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
            //De-allocatinng the memory after doing the Job.
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
            //Assigning the Security key to the TripleDES Service Provider.
            objTripleDESCryptoService.Key = securityKeyArray;
            //Mode of the Crypto service is Electronic Code Book.
            objTripleDESCryptoService.Mode = CipherMode.ECB;
            //Padding Mode is PKCS7 if there is any extra byte is added.
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;


            var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor();
            //Transform the bytes array to resultArray
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
            objTripleDESCryptoService.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        //This method is used to convert the Encrypted/Un-Readable Text back to readable  format.
        public static string DecryptCipherTextToPlainText(string CipherText)
        {
            byte[] toEncryptArray = Convert.FromBase64String(CipherText);
            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();

            //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
            //Assigning the Security key to the TripleDES Service Provider.
            objTripleDESCryptoService.Key = securityKeyArray;
            //Mode of the Crypto service is Electronic Code Book.
            objTripleDESCryptoService.Mode = CipherMode.ECB;
            //Padding Mode is PKCS7 if there is any extra byte is added.
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = objTripleDESCryptoService.CreateDecryptor();
            //Transform the bytes array to resultArray
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            objTripleDESCryptoService.Clear();

            //Convert and return the decrypted data/byte into string format.
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static bool CompareEncryptedPassword(string PlainPassword, string savePassword)
        {
            string encryptedPassword = EncryptPlainTextToCipherText(PlainPassword);
            return encryptedPassword.Equals(savePassword);
        }

        #region not in use
        private static string Encrypt(string clearText)
        {

            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                // Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        private static string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        private static string GenerateHashPassword(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + SALT_KEY);
            SHA256Managed sHA256ManagedString = new();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlainPassword">User enter password</param>
        /// <param name="savePassword">Database save password</param>
        /// <returns></returns>
        private static bool AreEqual(string PlainPassword, string savePassword)
        {
            string newHashedPin = GenerateHashPassword(PlainPassword);
            return newHashedPin.Equals(savePassword);
        }
        private static string CreateRandomPassword(int passwordLength = 12)
        {
            string dictionary = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new();

            char[] chars = new char[passwordLength];
            for (int j = 0; j < passwordLength; j++)
            {
                chars[j] = dictionary[random.Next(0, dictionary.Length)];
            }
            return new string(chars);
        }
        private const int SaltSize = 128;
        private static string CreateSalt()
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new();
            byte[] buff = new byte[SaltSize];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        //public   string GenerateString(int LNG)
        //{

        //    var bts = new byte[LNG];
        //    using (var rndm = RandomNumberGenerator.Create())
        //    {
        //        random.GetBytes(bts);
        //    }
        //    return new string(bytes.Select(x => AllowableCharacters[x % AllowG.Length]).ToArray());
        //}

        //public string Encrypt(string txt, string IV, string key)
        //{
        //    Aes cipher = CreateCipher(key);
        //    cipher.IV = Convert.FromBase64String(IV);

        //    ICryptoTransform cryptTransform = cipher.CreateEncryptor();
        //    byte[] ptxt = Encoding.UTF8.GetBytes(txt);
        //    byte[] ciptxt = cryptTransform.TransformFinalBlock(ptxt, 0, ptxt.Length);

        //    return Convert.ToBase64String(ciptxt);
        //}
        #endregion

    }
}
