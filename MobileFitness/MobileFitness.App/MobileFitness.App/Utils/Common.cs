/// <summary>
/// Помощни инструменти
/// </summary>
namespace MobileFitness.App.Utils
{
    using System;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    /// <summary>
    /// Съдържа специфични команди
    /// </summary>
    internal static class Common
    {
        /// <summary>
        /// Създава произволен salt за парола
        /// </summary>
        /// <param name="length">Дължина на паролата</param>
        /// <returns>salt</returns>
        public static byte[] GetRandomSalt(int length)
        {
            using (var random = new RNGCryptoServiceProvider())
            {

                byte[] salt = new byte[length];
                random.GetNonZeroBytes(salt);
                return salt;
            }
        }

        /// <summary>
        /// Създава парола със salt
        /// </summary>
        /// <param name="password">Парола</param>
        /// <param name="salt">Salt</param>
        /// <returns>cripted password</returns>
        public static byte[] SaltHashPassword(byte[] password, byte[] salt)
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

        /// <summary>
        /// Изчислява разликата в години между две дати
        /// </summary>
        /// <param name="firstDate">Първа дата</param>
        /// <param name="secondDate">Втора дата</param>
        /// <returns></returns>
        public static int GetAgeDiff(DateTime firstDate, DateTime secondDate)
            => Math.Abs((int)((firstDate - secondDate).Days / 365.2425));
    }
}
