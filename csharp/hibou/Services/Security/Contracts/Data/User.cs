using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Security.Contracts.Message
{
    /// <summary>
    /// Defines a user within the system.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Security")]
    public class User : 
        EntityBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the user's application name.
        /// </summary>        
        [DataMember()]
        public string UserName
        { get; set; }

        /// <summary>
        /// Gets or sets the user's full name.
        /// </summary>        
        [DataMember()]
        public string FullName
        { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>        
        [DataMember()]
        public string FirstName
        { get; set; }

        /// <summary>
        /// Gets or sets the user's surname.
        /// </summary>        
        [DataMember()]
        public string Surname
        { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>        
        [DataMember()]
        public string EmailAddress
        { get; set; }

        /// <summary>
        /// Gets or sets the id of the associated canton.
        /// </summary>        
        [DataMember()]
        public int RefIdCanton
        { get; set; }

        /// <summary>
        /// Gets or sets the id of the associated culture.
        /// </summary>        
        [DataMember()]
        public int RefIdSupportedCulture
        { get; set; }

        /// <summary>
        /// Gets or sets the id of the associated user role type.
        /// </summary>        
        [DataMember()]
        public int RefIdUserRoleType
        { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        [DataMember()]
        public byte[] PasswordHash
        { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        [DataMember()]
        public byte[] PasswordSalt
        { get; set; }

        /// <summary>
        /// Gets or sets the count of password changes.
        /// </summary>
        [DataMember()]
        public int PasswordChangeCount
        { get; set; }

        /// <summary>
        /// Gets or sets the password history hash.
        /// </summary>
        [DataMember()]
        public byte[] PasswordHistoryHash
        { get; set; }

        /// <summary>
        /// Gets or sets the password question.
        /// </summary>
        [DataMember()]
        public string PasswordQuestion
        { get; set; }

        /// <summary>
        /// Gets or sets the password question answer.
        /// </summary>
        [DataMember()]
        public string PasswordQuestionAnswer
        { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the user is deletable or not.
        /// </summary>
        [DataMember()]
        public bool IsDeletable
        { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the user is new to the system or not.
        /// </summary>
        [DataMember()]
        public bool IsNew
        { get; set; }

        #endregion Properties

        #region EntityBase overrides

        public override void InitialiseState()
        {
            base.InitialiseState();
            this.PasswordHash = new byte[32];
            this.PasswordSalt = new byte[32];
            this.RefIdUserRoleType = 1;
            this.IsDeletable = true;
        }

        #endregion EntityBase overrides

        #region Methods

        /// <summary>
        /// Derives the user's display name from the user profile.
        /// </summary>
        /// <returns>The user's display name.</returns>
        public string GetDisplayName()
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(this.FullName))
            {
                result = this.FullName;
            }
            else
            {
                // Concatanate the first name & last name.
                if (!string.IsNullOrEmpty(this.FirstName))
                {
                    result += this.FirstName.Trim();
                }
                if ((!string.IsNullOrEmpty(result)) &&
                    (!string.IsNullOrEmpty(this.Surname)))
                {
                    result += @" ";
                }
                if (!string.IsNullOrEmpty(this.Surname))
                {
                    result += this.Surname.Trim();
                }
            }

            // If still empty then revert to username.
            if ((string.IsNullOrEmpty(result)) &&
                (!string.IsNullOrEmpty(this.UserName)))
            {
                result += this.UserName.Trim();
            }

            return result;
        }

        /// <summary>
        /// Clears the password information so that it is not unecessarily transmitted across a network.
        /// </summary>
        public void ClearSensitiveData()
        {
            this.PasswordHash = null;
            this.PasswordHistoryHash = null;
            this.PasswordQuestionAnswer = null;
            this.PasswordSalt = null;
        }

        #endregion Methods
    }
}