using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MiddlewareTool.Utility
{
    #region Cipher Utility

    /// <summary>
    /// Cipher Utility
    /// </summary>
    public static class CipherUtility
    {
        private const string Default_Key = "P@ssw0rd";
        private const string KeyIV = "LV@2020";
        private const int KeySize = 256;
        private const int BlockSize = 128;
        private const int MaxQA = 3;
        private const int LenToken = 6;
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="toEncrypt">to Encrypt</param>
        /// <param name="useHashing">use Hashing</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            return Encrypt(toEncrypt, useHashing, Default_Key);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="toEncrypt">to Encrypt</param>
        /// <param name="useHashing">use Hashing</param>
        /// <param name="key">key</param>
        /// <returns></returns>       

        public static string Encrypt(string toEncrypt, bool useHashing, string key)
        {
            byte[] resultArray = EncryptStringToBytes_Aes(toEncrypt, key, KeyIV);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipherString">cipher String</param>
        /// <param name="useHashing">use Hashing</param>
        /// <returns></returns>
        public static string Decrypt(string cipherString, bool useHashing)
        {
            return Decrypt(cipherString, useHashing, Default_Key);
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipherString">cipher String</param>
        /// <param name="useHashing">use Hashing</param>
        /// <param name="key">key</param>
        /// <returns></returns>

        public static string Decrypt(string cipherString, bool useHashing, string key)
        {
            return DecryptStringFromBytes_Aes(cipherString, key, KeyIV);
        }
        /// <summary>
        /// Decrypt To Large
        /// </summary>
        /// <param name="cipherString">cipher String</param>
        /// <param name="useHashing">use Hashing</param>
        /// <returns></returns>
        public static string DecryptToLarge(string cipherString, bool useHashing)
        {
            return DecryptToLarge(cipherString, useHashing, Default_Key);
        }


        /// <summary>
        /// DecryptToLarge
        /// </summary>
        /// <param name="cipherString">cipher String</param>
        /// <param name="useHashing">use Hashing</param>
        /// <param name="key">key</param>
        /// <returns></returns>

        public static string DecryptToLarge(string cipherString, bool useHashing, string key)
        {
            string stringToDecrypt = (!string.IsNullOrEmpty(cipherString) ? cipherString.Replace(" ", "+") : cipherString);
            return DecryptStringFromBytes_Aes(stringToDecrypt, key, KeyIV);
        }
        public static string GetSha256Hash(string input)
        {
            return ComputeSha256Hash(input);
        }

        public static string GenneralToken()
        {
            var _rand = RandomValue(8);
            StringBuilder builder = new StringBuilder();
            string[] numbers = Regex.Split(_rand, @"\D+");
            foreach (var item in numbers)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    builder.Append(item);
                }
                else { }
            }
            if (builder.Length >= LenToken) { return builder.ToString().Substring(0, LenToken); }
            else { return builder.ToString().PadLeft(LenToken, '0'); }
        }
        /// <summary>
        /// Genneral Token
        /// </summary>
        /// <param name="lenghtRand">lenght Random</param>
        /// <returns></returns>
        public static string GenneralToken(int len)
        {
            if (len == 0) { len = LenToken; }
            var _rand = RandomValue(8);
            StringBuilder builder = new StringBuilder();
            string[] numbers = Regex.Split(_rand, @"\D+");
            foreach (var item in numbers)
            {
                if (!string.IsNullOrEmpty(item)) { builder.Append(item); }
                else { }
            }
            if (builder.Length >= len) { return builder.ToString().Substring(0, len); }
            else { return builder.ToString().PadLeft(len, '0'); }

        }

        public static string GenneralCustomerAccout()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(DateTime.Now.ToString("yy"));
            builder.Append(RandomValue(4));
            return builder.ToString().ToUpper();
        }
        public static string GenneralCodeValue(int len)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomValue(len));
            return builder.ToString().ToUpper();
        }

        public static int GenneralQAIndex()
        {
            int result = 0;
            var _rand = RandomValue(8);
            string[] numbers = Regex.Split(_rand, @"\D+");
            numbers = numbers.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            IDictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < MaxQA; i++)
            {
                if (i < numbers.Length)
                {
                    int.TryParse(numbers[i], out int dictVal);
                    dict.Add(i, dictVal);
                }
            }
            result = dict.OrderBy(x => x.Value).Select(x => x.Key).FirstOrDefault();
            return result;
        }
        private static string RandomValue(int len)
        {
            var randomGenerator = RandomNumberGenerator.Create(); // Compliant for security-sensitive use cases
            byte[] data = new byte[len];
            randomGenerator.GetBytes(data);
            var _rand = BitConverter.ToString(data);
            return _rand.Replace("-", "");
        }
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private static byte[] EncryptStringToBytes_Aes(string plainText, string keyText, string ivText)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }

            if (string.IsNullOrEmpty(keyText))
            {
                throw new ArgumentNullException("keyText");
            }

            if (string.IsNullOrEmpty(ivText))
            {
                throw new ArgumentNullException("ivText");
            }

            byte[] encrypted;
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            byte[] keyBytes = Encoding.ASCII.GetBytes(keyText.PadLeft(32));
            byte[] ivBytes = Encoding.ASCII.GetBytes(ivText.PadLeft(16));
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.KeySize = KeySize;
                aesAlg.BlockSize = BlockSize;
                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;
                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
                aesAlg.Clear();
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        private static string DecryptStringFromBytes_Aes(string encryptedString, string keyText, string ivText)
        {
            // Check arguments.
            if (string.IsNullOrEmpty(encryptedString))
            {
                throw new ArgumentNullException("encryptedString");
            }
            if (string.IsNullOrEmpty(keyText))
            {
                throw new ArgumentNullException("keyText");
            }
            if (string.IsNullOrEmpty(ivText))
            {
                throw new ArgumentNullException("ivText");
            }



            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            //Convert cipher text back to byte array
            byte[] cipherText = Convert.FromBase64String(encryptedString);
            byte[] keyBytes = Encoding.ASCII.GetBytes(keyText.PadLeft(32));
            byte[] ivBytes = Encoding.ASCII.GetBytes(ivText.PadLeft(16));
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.KeySize = KeySize;
                aesAlg.BlockSize = BlockSize;
                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;
                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                ////Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }

                }
                aesAlg.Clear();
            }

            return plaintext;

        }
    }
    #endregion

    #region Crypto Stream Utility
    /// <summary>
    /// Crypto Stream Utility
    /// </summary>

    public sealed class CryptoStreamUtility
    {
        private static readonly string ValHash = "D8CFDF35";
        private static readonly string SaltKey = "30D85B99";
        private static readonly string VIKey = "@xXrT9wzhrVbgMFR";
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="plainText">plain Text</param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(ValHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="encryptedText">encrypted Text</param>
        /// <returns></returns>
        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(ValHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
    }
    #endregion

    #region Generate Security Token
    /// <summary>
    /// Generate Security Token
    /// </summary>
    public sealed class GenerateSecurityToken
    {
        private const string keyInput = @"qWqmXSRd4cM";
        private const string keyLevel2 = @"SIGVN2020";

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="input">input</param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            try
            {
                var ecrlv1 = CryptoStreamUtility.Encrypt(input + keyInput);
                return CipherUtility.Encrypt(ecrlv1, true, keyLevel2);
            }
            catch { return string.Empty; }
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="encryptedText">encrypted Text</param>
        /// <returns></returns>
        public static string Decrypt(string encryptedText)
        {
            try
            {
                char[] removeChar = keyInput.ToCharArray();
                var codeDecryptMD5 = CipherUtility.DecryptToLarge(encryptedText, true, keyLevel2);
                var codeDecryptCrypto = CryptoStreamUtility.Decrypt(codeDecryptMD5);
                //return codeDecryptCrypto.TrimEnd(removeChar);
                return codeDecryptCrypto.Remove(codeDecryptCrypto.Length - keyInput.Length);

            }
            catch { return string.Empty; }
        }
        /// <summary>
        /// ToHex String
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToHexString(string str)
        {
            var sb = new StringBuilder();
            var bytes = Encoding.Unicode.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// From Hex String
        /// </summary>
        /// <param name="hexString">hex String</param>
        /// <returns></returns>
        public static string FromHexString(string hexString)
        {
            if (!string.IsNullOrEmpty(hexString))
            {
                var bytes = new byte[hexString.Length / 2];
                for (var i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                }
                return Encoding.Unicode.GetString(bytes);
            }
            return string.Empty;
        }
    }
    #endregion
}
