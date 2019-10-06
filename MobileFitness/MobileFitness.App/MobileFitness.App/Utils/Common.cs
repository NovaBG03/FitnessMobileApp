namespace MobileFitness.App.Utils
{
    using System;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    internal class Common
    {
        /// <summary>
        /// Creates random salt string
        /// </summary>
        /// <param name="length"></param>
        /// <returns>salt</returns>
        public static async Task<byte[]> GetRandomSalt(int length)
        {
            using (var random = new RNGCryptoServiceProvider())
            {

                byte[] salt = new byte[length];
                random.GetNonZeroBytes(salt);
                return salt;
            }
        }

        /// <summary>
        /// Creates password with salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns>cripted password</returns>
        public static async Task<byte[]> SaltHashPassword(byte[] password, byte[] salt)
        {
            using (HashAlgorithm algorithm = new SHA256Managed())
            {
                byte[] plainTextWithSaltBytes = new byte[password.Length + salt.Length];

                for (int i = 0; i < password.Length; i++)
                {
                    plainTextWithSaltBytes[i] = password[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[password.Length + i] = salt[i];
                }

                return algorithm.ComputeHash(plainTextWithSaltBytes);
            }
        }
    }
}
