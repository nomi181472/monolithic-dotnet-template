using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.AuthCrypto
{
    public class PasswordHasher
    {
       public static byte[] GenerateSalt()
        {
            using (var rng =  RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[32]; // 32 bytes (256 bits) is a good size for salt
                rng.GetBytes(salt);
                return salt;
            }
  

        }
        public static string HashPassword(string password, byte[] salt)
        {
            string hashedPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                )
            );

            return hashedPassword;
        }
        public static bool VerifyPassword(string enteredPassword,string hashed,string storedSalt)
        {
            return hashed.Equals(HashPassword(enteredPassword, Convert.FromBase64String(storedSalt)));
        }

    }
}
