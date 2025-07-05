using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ShowTime.DataAccess.Security;

public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

        byte[] hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8
        );

        string base64Hash = Convert.ToBase64String(hash);
        string base64Salt = Convert.ToBase64String(salt);

        return $"{base64Hash}:{base64Salt}";
    }

    public static bool ComparePasswords(string storedHashWithSalt, string providedPassword)
    {
        string[] parts = storedHashWithSalt.Split(':');
        if (parts.Length != 2) return false;

        string base64Hash = parts[0];
        string base64Salt = parts[1];

        byte[] salt = Convert.FromBase64String(base64Salt);
        byte[] providedHash = KeyDerivation.Pbkdf2(
            password: providedPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8
        );

        string providedBase64Hash = Convert.ToBase64String(providedHash);

        return CryptographicOperations.FixedTimeEquals(
            Convert.FromBase64String(base64Hash),
            providedHash
        );
    }
}
