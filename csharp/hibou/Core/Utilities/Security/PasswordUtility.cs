using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Keane.CH.Framework.Core.Resources.RegEx;
using Keane.CH.Framework.Core.Utilities.RegEx;
using Keane.CH.Framework.Core.ExtensionMethods;

namespace Keane.CH.Framework.Core.Utilities.Security
{
    /// <summary>
    /// Encapsualtes pasword generation utility functions.
    /// </summary>
    public sealed class PasswordUtility
    {
        #region Ctor.

        private PasswordUtility() { }

        #endregion Ctor.

        #region Constants

        private const int ITERATIONS = 10000;
        private const int SALT_SIZE = 32;
        private const int HASH_SIZE = 32;

        #endregion Constants

        #region Methods

        /// <summary>
        /// Creates a password hash using an existing salt.
        /// </summary>
        /// <remarks>
        /// Rfc2898DeriveBytes is a .NET framework helper class that 
        /// iteratrively salts and hashes the password.
        /// </remarks>
        /// <param name="password">The password being salted and hashed.</param>
        /// <param name="salt">The salt used to create the password.</param>
        /// <param name="hash">The hash used to create the password.</param>
        public static void CreateHash(
            string password,
            byte[] salt,
            out byte[] hash)
        {
            Rfc2898DeriveBytes rdb =
                new Rfc2898DeriveBytes(password, SALT_SIZE, ITERATIONS);
            rdb.Salt = salt;
            hash = rdb.GetBytes(HASH_SIZE);
        }

        /// <summary>
        /// Creates a password hash and salt.
        /// </summary>
        /// <remarks>
        /// Rfc2898DeriveBytes is a .NET framework helper class that 
        /// iteratrively salts and hashes the password.
        /// </remarks>
        /// <param name="password">The password being salted and hashed.</param>
        /// <param name="salt">The salt used to create the password.</param>
        /// <param name="hash">The hash used to create the password.</param>
        public static void CreateHashAndSalt(
            string password,
            out byte[] salt,
            out byte[] hash)
        {
            Rfc2898DeriveBytes rdb =
                new Rfc2898DeriveBytes(password, SALT_SIZE, ITERATIONS);
            salt = rdb.Salt;
            hash = rdb.GetBytes(HASH_SIZE);
        }

        /// <summary>
        /// Validates a password against the passed salt and hash.
        /// </summary>
        /// <param name="password">The password to be validated.</param>
        /// <param name="storedSalt">The previously stored password salt.</param>
        /// <param name="storedHash">The previously stored password hash.</param>
        /// <returns>True if valid, false if invalid.</returns>
        /// <remarks>
        /// Rfc2898DeriveBytes is a .NET framework helper class that 
        /// iteratrively salts and hashes the password.
        /// </remarks>
        public static bool IsPasswordValid(
            string password,
            byte[] storedSalt,
            byte[] storedHash)
        {
            // Derive the password hash.
            Rfc2898DeriveBytes rdb =
                new Rfc2898DeriveBytes(password, storedSalt, ITERATIONS);
            byte[] hash = rdb.GetBytes(HASH_SIZE);

            // Return the comparson result.
            return hash.AreEqualArrays(storedHash);
        }

        /// <summary>
        /// Returns whether the password is invalid or not.
        /// </summary>
        /// <param name="password">The password to be validated.</param>
        /// <param name="storedSalt">The previously stored password salt.</param>
        /// <param name="storedHash">The previously stored password hash.</param>
        /// <returns>True if invalid, false if valid.</returns>
        public static bool IsPasswordInvalid(
            string password, 
            byte[] storedSalt,
            byte[] storedHash)
        {
            return !IsPasswordValid(password, storedSalt, storedHash);
        }

        /// <summary>
        /// Determines whether a password is strong enough to be used.
        /// </summary>
        /// <param name="passwordStrength">The strength of the password.</param>
        /// <param name="password">The password being validated for strength.</param>
        /// <param name="minimumLength">The minimum length of the password.</param>
        /// <returns>True if deemed strong enough by the password complexity regular expression.</returns>
        public static bool IsPasswordStrongEnough(
            PasswordStrengthType passwordStrength, string password, uint minimumLength)
        {
            // Get the relevant password related regular expression.
            string passwordRegEx = 
                RegExUtility.RegExForPassword(passwordStrength, minimumLength);

            // Use the reg ex utility to validate.
            return RegExUtility.IsMatched(passwordRegEx, password);
        }

        /// <summary>
        /// Determines whether a password is too weak.
        /// </summary>
        /// <param name="passwordStrength">The strength of the password.</param>
        /// <param name="password">The password being validated for complexity.</param>
        /// <param name="minimumLength">The minimum length of the password.</param>
        /// <returns>True if valid according to the password complexity regualr expression.</returns>
        public static bool IsPasswordWeak(
            PasswordStrengthType passwordStrength, string password, uint minimumLength)
        {
            return !IsPasswordStrongEnough(passwordStrength, password, minimumLength);
        }

        /// <summary>
        /// Creates a temporary password of the required length.
        /// </summary>
        /// <param name="minimumLength">The minimum password length (between 6 & 36).</param>
        /// <param name="excludeSymbols">A flag inidcating whether symbols will be included ornot in the password.</param>
        /// <returns>A temporary password.</returns>
        public static string CreatePassword(
            uint minimumLength, bool excludeSymbols)
        {
            if ((minimumLength < 6) || (minimumLength > 20))
                throw new ArgumentException("Password length must be between 6 & 20 chracters long.");
            PasswordGenerator passwordGenerator = new PasswordGenerator() 
            {
                Minimum = Convert.ToInt32(minimumLength),
                Maximum = 20,                
                ExcludeSymbols = excludeSymbols,
                ConsecutiveCharacters = false,
                RepeatCharacters = false,
            };
            return passwordGenerator.Generate();
        }

        /// <summary>
        /// Crates, salts and hashes a temporary password.
        /// </summary>
        /// <param name="temporaryPassword">The temporary password being created, salted and hashed.</param>
        /// <param name="excludeSymbols">A flag inidcating whether symbols will be included ornot in the password.</param>
        /// <param name="salt">The salt used to create the temporary password.</param>
        /// <param name="hash">The hash used to create the temporary password.</param>
        public static void CreatePassword(
            uint minimumLength, 
            PasswordStrengthType passwordStrength,
            out string temporaryPassword,
            out byte[] salt,
            out byte[] hash)
        {
            temporaryPassword = string.Empty;
            bool excludeSymbols = (passwordStrength != PasswordStrengthType.High);
            bool isUsable = false;
            while (!isUsable)
            {
                temporaryPassword = CreatePassword(minimumLength, excludeSymbols);
                isUsable =
                    IsPasswordStrongEnough(passwordStrength, temporaryPassword, minimumLength)
                    &
                    temporaryPassword.Length <= 20;
            }
            CreateHashAndSalt(temporaryPassword, out salt, out hash);
        }

        #endregion Methods
    }
}
