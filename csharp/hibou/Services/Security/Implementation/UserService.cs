using System.Diagnostics;
using Keane.CH.Framework.Core.Utilities.Security;
using Keane.CH.Framework.Services.Entity.Contracts.Message;
using Keane.CH.Framework.Services.Entity.Implementation;
using Keane.CH.Framework.Services.Notification.Contracts;
using Keane.CH.Framework.Services.Security.Contracts.Message;

namespace Keane.CH.Framework.Services.Security.Implementation
{
    /// <summary>
    /// Encpasulates service operations for managing the User.
    /// </summary>
    public class UserService<U> :
        EntityService<U> 
        where U : User, new()
    {
        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserService()
        {
            this.InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected void InitialiseMembers()
        {
            this.PasswordStrength = PasswordStrengthType.Medium;
            this.PasswordMinLength = 8;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the associated security notification service.
        /// </summary>
        public ISecurityNotificationService SecurityNotificationService
        { get; set; }

        /// <summary>
        /// Gets or sets the strength of the passwords being used within this system.
        /// </summary>
        public PasswordStrengthType PasswordStrength
        { get; set; }

        /// <summary>
        /// Gets or sets the minimum length of the password.
        /// </summary>
        public uint PasswordMinLength
        { get; set; }

        #endregion Properties

        #region IUserService Members

        /// <summary>
        /// Inserts a single instance.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>The id of the newly inserted instance.</returns>
        public override InsertResponse
            Insert(InsertRequest request)
        {
            // Defensive programming.
            Debug.Assert(request != null, "request");
            Debug.Assert(request.Entity != null, "request.Instance");
            U instance = request.Entity as U;
            Debug.Assert(instance != null, "Casting failure from EntityBase to User.");

            // Intiialise account password.
            byte[] hash; 
            byte[] salt; 
            string initialPassword;
            PasswordUtility.CreatePassword(
                PasswordMinLength, 
                PasswordStrength, 
                out initialPassword, 
                out salt, 
                out hash);
            instance.PasswordHash = hash;
            instance.PasswordSalt = salt;

            // Send account initialisation notification.
            SendCreateUserNotificationRequest notificationRequest = new SendCreateUserNotificationRequest() 
            { 
                Context = request.Context,
                EmailAddress = instance.EmailAddress,
                InitialPassword = initialPassword
            };
            this.SecurityNotificationService.SendCreateUserNotification(notificationRequest);

            // Insert.
            return base.Insert(request);
        }

        #endregion
    }
}