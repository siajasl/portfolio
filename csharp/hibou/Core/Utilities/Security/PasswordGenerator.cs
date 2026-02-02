using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Keane.CH.Framework.Core.Resources.RegEx;
using Keane.CH.Framework.Core.Utilities.RegEx;

namespace Keane.CH.Framework.Core.Utilities.Security
{
    /// <summary>
    /// Encpasulates methods for generating password.
    /// </summary>
    internal class PasswordGenerator
    {
        #region Constructor

        public PasswordGenerator()
        {
            InitialiseMembers();
        }

        #endregion Constructor

        #region Constants

        private const int DefaultMinimum = 6;
        private const int DefaultMaximum = 18;
        private const int UBoundDigit = 61;

        #endregion Constants

        #region Fields

        private int minSizeField;
        private int maxSizeField;
        private char[] pwdCharArrayField =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!@#$%^&*?".ToCharArray();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the random number generator.
        /// </summary>
        private RNGCryptoServiceProvider RNG
        { get; set; }
        
        /// <summary>
        /// The set of exclusions.
        /// </summary>
        public string Exclusions
        { get; set; }

        /// <summary>
        /// The minimum pasword length.
        /// </summary>
        public int Minimum
        {
            get { return this.minSizeField; }
            set
            {
                this.minSizeField = value;
                if (PasswordGenerator.DefaultMinimum > this.minSizeField)
                {
                    this.minSizeField = PasswordGenerator.DefaultMinimum;
                }
            }
        }

        /// <summary>
        /// The maximum password length.
        /// </summary>
        public int Maximum
        {
            get { return this.maxSizeField; }
            set
            {
                this.maxSizeField = value;
                if (this.minSizeField >= this.maxSizeField)
                {
                    this.maxSizeField = PasswordGenerator.DefaultMaximum;
                }
            }
        }

        /// <summary>
        /// Flag indicating whether symbols are to be excluded.
        /// </summary>
        public bool ExcludeSymbols
        { get; set; }

        /// <summary>
        /// Flag indicating whether numbers are to be included.
        /// </summary>
        public bool IncludeNumeric
        { get; set; }

        /// <summary>
        /// Flag indicating whether repeat characters are allowed.
        /// </summary>
        public bool RepeatCharacters
        { get; set; }

        /// <summary>
        /// Falg indicating whether consecutive characters are allowed.
        /// </summary>
        public bool ConsecutiveCharacters
        { get; set; }

        #endregion Properties

        #region Private methods

        /// <summary>
        /// Standard memeber initialisation routine.
        /// </summary>
        private void InitialiseMembers()
        {
            this.Minimum = DefaultMinimum;
            this.Maximum = DefaultMaximum;
            this.IncludeNumeric = true;
            this.ConsecutiveCharacters = false;
            this.RepeatCharacters = true;
            this.ExcludeSymbols = false;
            this.Exclusions = null;
            RNG = new RNGCryptoServiceProvider();
        }

        #endregion Private methods

        /// <summary>
        /// Gets a random number within the bounds.
        /// </summary>
        /// <param name="lBound">Lower bound.</param>
        /// <param name="uBound">Upper bound.</param>
        /// <returns>A random number within the bounds.</returns>
        private int GetCryptographicRandomNumber(int lBound, int uBound)
        {
            uint urndnum;
            byte[] rndnum = new Byte[4];
            if (lBound == uBound - 1)
                return lBound;
            uint xcludeRndBase = (uint.MaxValue - (uint.MaxValue % (uint)(uBound - lBound)));
            do
            {
                RNG.GetBytes(rndnum);
                urndnum = System.BitConverter.ToUInt32(rndnum, 0);
            } while (urndnum >= xcludeRndBase);
            return (int)(urndnum % (uBound - lBound)) + lBound;
        }

        /// <summary>
        /// Gets a random character.
        /// </summary>
        protected char GetRandomCharacter()
        {
            int upperBound = pwdCharArrayField.GetUpperBound(0);
            if (true == this.ExcludeSymbols)
                upperBound = PasswordGenerator.UBoundDigit;
            int randomCharPosition = GetCryptographicRandomNumber(
                pwdCharArrayField.GetLowerBound(0), upperBound);
            char randomChar = pwdCharArrayField[randomCharPosition];
            return randomChar;
        }

        /// <summary>
        /// Generates a password.
        /// </summary>
        /// <returns>A geenrated password.</returns>
        public string Generate()
        {
            // Initialise password length.
            int pwdLength = 
                GetCryptographicRandomNumber(this.Minimum, this.Maximum);
            StringBuilder pwdBuffer = new StringBuilder();
            pwdBuffer.Capacity = this.Maximum;

            // Generate password.
            char lastCharacter, nextCharacter;
            lastCharacter = nextCharacter = '\n';
            for (int i = 0; i < pwdLength; i++)
            {
                // Force a numeric if neceessary otherwise get a random character.
                if (i == 2 && IncludeNumeric)
                    nextCharacter = Convert.ToChar(@"4");
                else
                    nextCharacter = GetRandomCharacter();

                // Ensure no consecutive characters.
                if (false == this.ConsecutiveCharacters)
                {
                    while (lastCharacter == nextCharacter)
                    {
                        nextCharacter = GetRandomCharacter();
                    }
                }

                // Ensure no repeat characters.
                if (false == this.RepeatCharacters)
                {
                    string temp = pwdBuffer.ToString();
                    int duplicateIndex = temp.IndexOf(nextCharacter);
                    while (-1 != duplicateIndex)
                    {
                        nextCharacter = GetRandomCharacter();
                        duplicateIndex = temp.IndexOf(nextCharacter);
                    }
                }

                // Ensure no excluded characters.
                if ((null != this.Exclusions))
                {
                    while (-1 != this.Exclusions.IndexOf(nextCharacter))
                    {
                        nextCharacter = GetRandomCharacter();
                    }
                }

                // Append to the password buffer and cache last character.
                pwdBuffer.Append(nextCharacter);
                lastCharacter = nextCharacter;
            }

            // Null safe return.
            if (null != pwdBuffer)
            {
                return pwdBuffer.ToString();
            }
            else
            {
                return String.Empty;
            }
        }
    }
}