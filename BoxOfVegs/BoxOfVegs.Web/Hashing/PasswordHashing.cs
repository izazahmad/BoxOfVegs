using System;
using System.Security.Cryptography;
namespace BoxOfVegs.Web.Hashing
{   //creates a salted PBKDF2 hash of the password.
    public class PasswordHashing
    {      
        public const int salt_Byte_Size = 24;//set the salt size 24
        public const int hash_Byte_Size = 24;//hash size 24
        public const int PBKDF2_Iterations = 1000;//pbkdf2 iteration is 1000
        public const int Iteration_Index = 0;//set salt index is 0
        public const int salt_Index = 1;//salt index 1
        public const int PBKDF2_Index = 2;//hash index 2        
        //method to convert the password string in hashing and add salt and iteration and create the password       
        public static string CreateHash(string password)
        {
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();//Generate a random salt
            byte[] salt = new byte[salt_Byte_Size];
            csprng.GetBytes(salt);
            byte[] hash = PBKDF2(password, salt, hash_Byte_Size, PBKDF2_Iterations);//Hash the password and encode the parameters
            return PBKDF2_Iterations + ":" + Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);//concate salt, hash and iteration 1000 and return it
        }
        //method to validate the password
        public static bool ValidatePassword(string password, string correctHash)
        {   //Extract the parameters from the hash
            char[] delimiter = { ':' };//create a array and store delimter set as ':'           
            string[] split = correctHash.Split(delimiter);//split the hash password by using delimiter and store in array           
            int iterations = int.Parse(split[Iteration_Index]);//convert the integer and store in variable            
            byte[] salt = Convert.FromBase64String(split[salt_Index]);//create byte erray and store salt            
            byte[] hash = Convert.FromBase64String(split[PBKDF2_Index]);//create byte array and store hash            
            byte[] testHash = PBKDF2(password, salt, hash.Length, iterations);//create byte array and store PKDF2          
            return SlowEquals(hash, testHash);//return and pass to SlowEquals method
        }       
        // Compares two byte arrays in length-constant time. This comparison method is used so that password hashes cannot be extracted from     
        // True if both byte arrays are equal. False otherwise.
        private static bool SlowEquals(byte[] a, byte[] b)
        {   uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            { diff |= (uint)(a[i] ^ b[i]); }
            return diff == 0;
        }
        private static byte[] PBKDF2(string password, byte[] salt, int outputBytes, int iterations)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt)//class which does the hashing
            { IterationCount = iterations };           
            return pbkdf2.GetBytes(outputBytes);// return a hash of the password
        }
    }
}