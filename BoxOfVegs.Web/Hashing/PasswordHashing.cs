using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace BoxOfVegs.Web.Hashing
{
    public class PasswordHashing
    {

        /// <summary>
        /// creates a salted PBKDF2 hash of the password.
        /// </summary>
        /// <param name="password">the password to hash.</param>
        /// <returns>The hash of the password.</returns>
        /*set the salt size 24
         * hash size 24
         * pbkdf2 iteration is 1000
         * set salt index is 0
         * hash index 1
         */
        public const int salt_Byte_Size = 24;
        public const int hash_Byte_Size = 24;
        public const int PBKDF2_Iterations = 1000;
        public const int Iteration_Index = 0;
        public const int salt_Index = 1;
        public const int PBKDF2_Index = 2;
        /// <summary>
        /// convert the password string in hashing and add salt and iteration
        /// and create the password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string CreateHash(string password)
        {
            //Generate a random salt
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[salt_Byte_Size];
            csprng.GetBytes(salt);

            //Hash the password and encode the parameters
            byte[] hash = PBKDF2(password, salt, hash_Byte_Size, PBKDF2_Iterations);
            //concate salt, hash and iteration 1000 and return it
            return PBKDF2_Iterations + ":" +
                   Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);
        }
        /// <summary>
        /// validate the password
        /// it split the hashed string by : sign
        /// convert the entered password in hash
        /// compare the both converted and split
        /// </summary>
        /// <param name="password"></param>
        /// <param name="correctHash"></param>
        /// <returns></returns>
        public static bool ValidatePassword(string password, string correctHash)
        {
            //Extract the parameters from the hash
            //create a array and store delimter set as ':'
            char[] delimiter = { ':' };
            //split the hash password by using delimiter and store in array
            string[] split = correctHash.Split(delimiter);
            //convert the integer and store in variable
            int iterations = int.Parse(split[Iteration_Index]);
            //create byte erray and store salt
            byte[] salt = Convert.FromBase64String(split[salt_Index]);
            //create byte array and store hash
            byte[] hash = Convert.FromBase64String(split[PBKDF2_Index]);
            //create byte array and store PKDF2
            byte[] testHash = PBKDF2(password, salt, hash.Length, iterations);
            //return and pass to SlowEquals method
            return SlowEquals(hash, testHash);
        }
        /// <summary>
        /// Compares two byte arrays in length-constant time. This comparison
        /// method is used so that password hashes cannot be extracted from
        /// on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">The first byte array.</param>
        /// <param name="b">The second byte array.</param>
        /// <returns>True if both byte arrays are equal. False otherwise.</returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }

            return diff == 0;
        }
        private static byte[] PBKDF2(string password, byte[] salt, int outputBytes, int iterations)
        {
            //class which is does hashing
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt)
            {
                IterationCount = iterations
            };
            // return a hash of the password
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}