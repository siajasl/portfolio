using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Core.Resources.RegEx;
using System.Text.RegularExpressions;
using Keane.CH.Framework.Core.Utilities.Security;

namespace Keane.CH.Framework.Core.Utilities.RegEx
{
    /// <summary>
    /// Encapsualtes regular expression utility functions.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public sealed class RegExUtility
    {
        #region Ctor.

        private RegExUtility() { }

        #endregion Ctor.

        #region Public Methods

        /// <summary>
        /// Get regular expression to avoid a given text.
        /// </summary>
        /// <param name="textToAvoid">The given text to avoid.</param>
        /// <returns>A string representing the regular expression to avoid a text.</returns>
        public static string RegExForToAvoidText(string textToAvoid)
        {
            return string.Format(RegExLibrary.TextToAvoid, textToAvoid);
        }

        /// <summary>
        /// Returns a regular expression to use for email validation.
        /// </summary>
        /// <returns>A regular expression for validting email addresses.</returns>
        public static string RegExForEmailAddress()
        {
            return RegExLibrary.EmailAddress;
        }

        /// <summary>
        /// Returns a regular expression to use for user name validation.
        /// </summary>
        /// <param name="minimumLength">The mimimum length of the username.</param>
        /// <param name="maximumLength">The maximum length of the username.</param>
        /// <returns>A regular expression for validting passwords.</returns>
        public static string RegExForUserName(
            uint minimumLength, uint maximumLength)
        {
            string result = RegExLibrary.UserName;
            // N.B. Cannot do this with String.Format function as the placeholder 
            // cannot be evaluated correctly due to the nature of the regualr expression.
            result = result.Replace("MIN_LENGTH", minimumLength.ToString());
            result = result.Replace("MAX_LENGTH", maximumLength.ToString());
            return result;
        }

        /// <summary>
        /// Returns a regular expression to use for password validation.
        /// </summary>
        /// <param name="passwordStrength">The strength of the password.</param>
        /// <param name="minimumLength">The mimimum length of the password.</param>
        /// <returns>A regular expression for validating passwords.</returns>
        public static string RegExForPassword(
            PasswordStrengthType passwordStrength, uint minimumLength)
        {
            // Assign regular expression.
            string regEx = string.Empty;
            switch (passwordStrength)
            {
                case PasswordStrengthType.High:
                    regEx = RegExLibrary.StrongPassword;
                    break;
                case PasswordStrengthType.Medium:
                    regEx = RegExLibrary.MediumPassword;
                    break;
                case PasswordStrengthType.Low:
                    regEx = RegExLibrary.WeakPassword;
                    break;
                default:
                    regEx = RegExLibrary.StrongPassword;
                    break;
            }

            // Parse minimum length.
            regEx = regEx.Replace("MIN_LENGTH", minimumLength.ToString());
            
            // Return derived regular expression.
            return regEx;
        }

        /// <summary>
        /// Determines whether the text is matched when parsed by the regular expression.
        /// </summary>
        /// <param name="regEx">The regular expression against which the text is parsed.</param>
        /// <param name="text">The text to be parsed.</param>
        /// <returns>True if matched.</returns>
        public static bool IsMatched(
            string regEx, string text)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(regEx) &&
                !string.IsNullOrEmpty(text))
            {
                Regex rx = new Regex(regEx);
                result = rx.IsMatch(text);            
            }
            return result;
        }

        /// <summary>
        /// Determines whether the enmail address is valid when parsed by the regular expression.
        /// </summary>
        /// <param name="emailAddress">The email address being validated.</param>
        /// <returns>True if valid.</returns>
        public static bool IsValidEmailAddress(
            string emailAddress)
        {
            return IsMatched(RegExLibrary.EmailAddress, emailAddress);
        }

        /// <summary>
        /// Determines whether any of the array of words exists within the passed text.
        /// </summary>
        /// <param name="regEx">The regex being tested.</param>
        /// <param name="text">The text being tested.</param>
        /// <returns>True if valid.</returns>
        public static bool IsValidExclusion(
            string regEx, string text)
        {
            return !IsMatched(regEx, text);
        }

        /// <summary>
        /// Determines whether any of the array of words exists within the passed text.
        /// </summary>
        /// <param name="wordArray">The array of words.</param>
        /// <param name="text">The text being tested.</param>
        /// <returns>True if the text contains one of the passed words.</returns>
        public static bool Contains(
            string[] wordArray, string text)
        {
            string regEx = string.Empty;
            wordArray.ToList<string>().ForEach(w => regEx += w + "|");
            regEx = regEx.Substring(0, regEx.Length - 1);
            return IsMatched(regEx, text);
        }
        
        #endregion Public Methods
    }
}
