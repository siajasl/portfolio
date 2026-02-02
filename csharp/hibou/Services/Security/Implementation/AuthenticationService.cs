using System;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Core;
using Keane.CH.Framework.Core.ExtensionMethods;
using Keane.CH.Framework.Core.Utilities.RegEx;
using Keane.CH.Framework.Core.Utilities.Security;
using Keane.CH.Framework.Services.Notification.Contracts;
using Keane.CH.Framework.Services.Security.Contracts.Service;
using Keane.CH.Framework.Services.Security.Contracts.Message;
using Keane.CH.Framework.Services.Security.Implementation;
using Keane.CH.Framework.Services.Security.Implementation.DataAccess;

namespace Keane.CH.Framework.Services.Security.Implementation
{
    /// <summary>
    /// Encpasulates service operations for managing User authentication.
    /// </summary>
    public class AuthenticationService :
        ServiceImplementationBase,
        IAuthenticationService
    {
        #region Properties

        #region Collaborators

        /// <summary>
        /// Gets or sets an associated dao.
        /// </summary>
        public IUserDao UserDao
        { get; set; }

        /// <summary>
        /// Gets or sets the associated security notification service.
        /// </summary>
        public ISecurityNotificationService SecurityNotificationService
        { get; set; }

        #endregion Collaborators

        /// <summary>
        /// Gets or sets the strength of the passwords being used within this system.
        /// </summary>
        public PasswordStrengthType PasswordStrength
        { get; set; }

        /// <summary>
        /// Gets or sets the strength of the admin passwords being used within this system.
        /// </summary>
        public PasswordStrengthType PasswordAdminStrength
        { get; set; }

        /// <summary>
        /// Gets or sets the number of passwords that must be unique before a password can be reused.
        /// </summary>
        public int PasswordHistoryCount
        { get; set; }

        /// <summary>
        /// Gets or sets the length of the password in bytes.
        /// </summary>
        public int PasswordByteLength
        { get; set; }

        /// <summary>
        /// Gets or sets the minimum length of the password.
        /// </summary>
        public uint PasswordMinLength
        { get; set; }

        /// <summary>
        /// Gets or sets the minimum length of the admin password.
        /// </summary>
        public uint PasswordAdminMinLength
        { get; set; }

        #endregion Properties

        #region IAuthenticationService Members

        /// <summary>
        /// Authenticates against the passed connection credentials.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        /// <remarks>
        /// This supports simple username only authentication scenarios.
        /// </remarks>
        AuthenticateConnectResponse 
            IAuthenticationService.AuthenticateConnect(
                AuthenticateConnectRequest request)
        {
            // Initialise response.
            AuthenticateConnectResponse response = new AuthenticateConnectResponse()
            {
                Status = OperationResponseStatus.Failure,
            };

            // Execute operation.
            try
            {
                // When valid ...
                User user = this.ValidateConnect(request);
                if (user != null)
                {
                    // Clear sensitive data (in case of network attack).
                    user.ClearSensitiveData();

                    // Set response.
                    response.Status = OperationResponseStatus.Success;
                    response.User = user;
                }
            }
            // Handle exceptions.
            catch (Exception ex)
            {
                // Set response.
                response.Status = OperationResponseStatus.Exception;
            }

            // Return response.
            return response;
        }

        /// <summary>
        /// Authenticates against the passed login user credentials.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        AuthenticateLoginResponse
            IAuthenticationService.AuthenticateLogin(
                AuthenticateLoginRequest request)
        {
            // Initialise response.
            AuthenticateLoginResponse response = new AuthenticateLoginResponse()
            {
                Status = OperationResponseStatus.Failure,
            };

            // Execute operation.
            try
            {
                // When valid ...
                User user = this.ValidateLogin(request);
                if (user != null)
                {
                    // Clear sensitive data (in case of network attack).
                    user.ClearSensitiveData();

                    // Set response.
                    response.Status = OperationResponseStatus.Success;
                    response.User = user;
                }
            }
            // Handle exceptions.
            catch (Exception ex)
            {
                // Set response.
                response.Status = OperationResponseStatus.Exception;
            }

            // Return response.
            return response;
        }

        /// <summary>
        /// Authenticates against the passed initial user credentials.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        AuthenticateInitialisationResponse
            IAuthenticationService.AuthenticateInitialisation(
                AuthenticateInitialisationRequest request)
        {
            // Initialise response.
            AuthenticateInitialisationResponse response = new AuthenticateInitialisationResponse()
            {
                Status = OperationResponseStatus.Failure,
            };

            // Execute operation.
            try
            {
                // When valid ...
                User user = this.ValidateInitialisation(request);
                if (user != null)
                {
                    // All tests have passed therefore update user account with the user's initial password.
                    byte[] newSalt; 
                    byte[] newHash;
                    PasswordUtility.CreateHashAndSalt(request.NewPassword, out newSalt, out newHash);
                    user.PasswordSalt = newSalt;
                    user.PasswordHash = newHash;

                    // Indicate that the acount is no longer new.
                    user.IsNew = false;

                    // Persist account changes.
                    UserDao.Save(user);

                    // Clear password data in case of network attack.
                    user.ClearSensitiveData();

                    // Set response.
                    response.Status = OperationResponseStatus.Success;
                    response.User = user;
                }
            }
            // Handle exceptions.
            catch (Exception ex)
            {
                // Set response.
                response.Status = OperationResponseStatus.Exception;
            }

            // Return response.
            return response;
        }

        /// <summary>
        /// Authenticates against the passed changed user credentials.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        AuthenticateChangeResponse
            IAuthenticationService.AuthenticateChange(
                AuthenticateChangeRequest request)
        {
            // Initialise response.
            AuthenticateChangeResponse response = new AuthenticateChangeResponse()
            {
                Status = OperationResponseStatus.Failure,
            };

            // Execute operation.
            try
            {
                // When valid ...
                byte[] newHash;
                User user = ValidateChange(request, out newHash);
                if (user != null)
                {
                    // Initialise password history (if necessary).
                    if (user.PasswordHistoryHash == null)
                        user.PasswordHistoryHash = new byte[PasswordHistoryCount * PasswordByteLength];

                    // Copy old password into history.
                    int slot = (user.PasswordChangeCount % PasswordHistoryCount) * PasswordByteLength;
                    for (int i = 0; i < PasswordByteLength; i++)
                    {
                        user.PasswordHistoryHash[slot + i] = user.PasswordHash[i];
                    }

                    // Update account.
                    user.PasswordHash = newHash;
                    user.PasswordChangeCount++;
                    UserDao.Save(user);

                    // Clear password data in case of network attack.
                    user.ClearSensitiveData();

                    // Set response.
                    response.Status = OperationResponseStatus.Success;
                    response.User = user;
                }
            }
            // Handle exceptions.
            catch (Exception ex)
            {
                // Set response.
                response.Status = OperationResponseStatus.Exception;
            }

            // Return response.
            return response;
        }

        /// <summary>
        /// Step one of forgotten user credentials authentication.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        AuthenticateForgottenStepOneResponse
            IAuthenticationService.AuthenticateForgottenStepOne(
                AuthenticateForgottenStepOneRequest request)
        {
            // Initialise response.
            AuthenticateForgottenStepOneResponse response = new AuthenticateForgottenStepOneResponse()
            {
                Status = OperationResponseStatus.Failure,
            };

            // Execute operation.
            try
            {
                // When valid ...
                User user = ValidateForgottenStepOne(request);
                if (user != null)
                {
                    // Clear password data in case of network attack.
                    user.ClearSensitiveData();

                    // Set response.
                    response.Status = OperationResponseStatus.Success;
                    response.User = user;
                }
            }
            // Handle exceptions.
            catch (Exception ex)
            {
                // Set response.
                response.Status = OperationResponseStatus.Exception;
            }

            // Return response.
            return response;
        }

        /// <summary>
        /// Step two of forgotten user credentials authentication.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        AuthenticateForgottenStepTwoResponse
            IAuthenticationService.AuthenticateForgottenStepTwo(
                AuthenticateForgottenStepTwoRequest request)
        {
            // Initialise response.
            AuthenticateForgottenStepTwoResponse response = new AuthenticateForgottenStepTwoResponse()
            {
                Status = OperationResponseStatus.Failure,
            };

            // Execute operation.
            try
            {
                // When valid ...
                User user = ValidateForgottenStepTwo(request);
                if (user != null)
                {
                    // Create a temporary password.
                    string temporaryPassword;
                    byte[] salt, hash;
                    PasswordUtility.CreatePassword(
                        PasswordMinLength, PasswordStrength, out temporaryPassword, out salt, out hash);

                    // Send notification.
                    SendForgottenCredentialsNotificationRequest notificationRequest = new SendForgottenCredentialsNotificationRequest()
                    {
                        Context = request.Context,
                        EmailAddress = user.EmailAddress,
                        TemporaryPassword = temporaryPassword
                    };
                    this.SecurityNotificationService.SendForgottenCredentialsNotification(notificationRequest);

                    // Persist account changes.
                    user.PasswordHash = hash;
                    user.PasswordSalt = salt;
                    UserDao.Save(user);

                    // Clear password data in case of network attack.
                    user.ClearSensitiveData();

                    // Set response.
                    response.Status = OperationResponseStatus.Success;
                    response.User = user;
                }
            }
            // Handle exceptions.
            catch (Exception ex)
            {
                // Set response.
                response.Status = OperationResponseStatus.Exception;
            }

            // Return response.
            return response;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Validates change credentials.
        /// </summary>
        /// <param name="credentials">The user credentials.</param>
        /// <param name="newHash">The hash of the new password.</param>
        /// <returns>A user instance if the credentials were validated.</returns>
        private User ValidateChange(
            AuthenticateChangeRequest request, 
            out byte[] newHash)
        {
            // Initialise arrays.
            newHash = new byte[PasswordByteLength];

            // False if passwords match.
            if (request.Password.Equals(request.NewPassword))
                return null;

            // False if password is an email address.
            if (RegExUtility.IsValidEmailAddress(request.NewPassword))
                return null;

            // False if passwords are weak.
            if (PasswordUtility.IsPasswordWeak(PasswordStrength, request.Password, PasswordMinLength))
                return null;
            if (PasswordUtility.IsPasswordWeak(PasswordStrength, request.NewPassword, PasswordMinLength))
                return null;

            // False if user not found.
            User user = UserDao.Get(request.UserName);
            if (user == null)
                return null;

            // False if user is new (they must go through new user login).
            if (user.IsNew)
                return null;

            // False if new password contains user name | first name | surname | date of birth
            string[] words = new string[] { user.EmailAddress, user.FirstName, user.Surname, user.UserName };
            if (RegExUtility.Contains(words, request.NewPassword))
                return null;

            // False if password is invalid.
            if (PasswordUtility.IsPasswordInvalid(request.Password, user.PasswordSalt, user.PasswordHash))
                return null;

            // False if password matches one in history.
            if (IsNewPasswordInHistory(user, request.NewPassword, out newHash))
                return null;

            // All tests passed therefore return the user instance.
            return user;
        }

        /// <summary>
        /// Validates connect credentials.
        /// </summary>
        /// <param name="credentials">The user credentials.</param>
        /// <returns>A user instance if the credentials were validated.</returns>
        private User ValidateConnect(
            AuthenticateConnectRequest request)
        {
            // False if user not found.
            User user = UserDao.Get(request.UserName);
            if (user == null)
                return null;

            // False if user is new (they must go through new user login).
            if (user.IsNew)
                return null;

            // All tests passed therefore return the user instance.
            return user;
        }

        /// <summary>
        /// Validates login credentials.
        /// </summary>
        /// <param name="credentials">The user credentials.</param>
        /// <returns>A user instance if the credentials were validated.</returns>
        private User ValidateLogin(
            AuthenticateLoginRequest request)
        {
            // False if password is too weak.
            if (PasswordUtility.IsPasswordWeak(
                    PasswordStrength,
                    request.Password,
                    PasswordMinLength))
                return null;            

            // False if user not found.
            User user = UserDao.Get(request.UserName);
            if (user == null)
                return null;

            // False if user is new (they must go through new user login).
            if (user.IsNew)
                return null;

            // False if password is invalid.
            if (!PasswordUtility.IsPasswordValid(
                    request.Password, 
                    user.PasswordSalt, 
                    user.PasswordHash))
                return null;

            // All tests passed therefore return the user instance.
            return user;
        }

        /// <summary>
        /// Validates initialisation credentials.
        /// </summary>
        /// <param name="credentials">The user credentials.</param>
        /// <returns>A user instance if the credentials were validated.</returns>
        private User ValidateInitialisation(
            AuthenticateInitialisationRequest request)
        {
            // False if passwords match.
            if (request.Password.Equals(request.NewPassword))
                return null;

            // False if password is an email address.
            if (RegExUtility.IsValidEmailAddress(request.NewPassword))
                return null;

            // False if passwords are weak.
            if (PasswordUtility.IsPasswordWeak(PasswordStrength, request.Password, PasswordMinLength))
                return null;
            if (PasswordUtility.IsPasswordWeak(PasswordStrength, request.NewPassword, PasswordMinLength))
                return null;

            // False if user not found.
            User user = UserDao.Get(request.UserName);
            if (user == null)
                return null;

            // False if user is an old one.
            if (!user.IsNew)
                return null;

            // False if new password contains user name | first name | surname | date of birth
            string[] words = new string[] { user.EmailAddress, user.FirstName, user.Surname, user.UserName };
            if (RegExUtility.Contains(words, request.NewPassword))
                return null;

            // False if password is invalid.
            if (PasswordUtility.IsPasswordInvalid(request.Password, user.PasswordSalt, user.PasswordHash))
                return null;

            // All tests passed therefore return the user instance.
            return user;
        }

        /// <summary>
        /// Validates step two forgotten credentials.
        /// </summary>
        /// <param name="credentials">The user credentials.</param>
        /// <returns>A user instance if the credentials were validated.</returns>
        private User ValidateForgottenStepTwo(
            AuthenticateForgottenStepTwoRequest request)
        {
            // Revalidate step one.
            User user = ValidateForgottenStepOne(request);
            if (user == null)
                return null;

            // False if password answers do not match (case invariant).
            if (!user.PasswordQuestionAnswer.Trim().ToUpper().Equals(request.PasswordQuestionAnswer.Trim().ToUpper()))
                return null;

            // All tests passed therefore return the user instance.
            return user;
        }

        /// <summary>
        /// Validates step one forgotten credentials.
        /// </summary>
        /// <param name="credentials">The user credentials.</param>
        /// <returns>A user instance if the credentials were validated.</returns>
        private User ValidateForgottenStepOne(
            AuthenticateForgottenStepOneRequest request)
        {
            // False if email is invalid.
            if (!RegExUtility.IsValidEmailAddress(request.EmailAddress))
                return null;

            // False if user not found.
            User user = UserDao.Get(request.UserName);
            if (user == null)
                return null;

            // False if user is new (they must go through new user login).
            if (user.IsNew)
                return null;

            // False if email addresses do not match (case invariant).
            if (!user.EmailAddress.Trim().ToUpper().Equals(request.EmailAddress.Trim().ToUpper()))
                return null;

            // All tests passed therefore return the user instance.
            return user;
        }

        /// <summary>
        /// Determines whether the new password is matched in the history.
        /// </summary>
        /// <param name="user">The user whose credentials are being valdiated.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="newHash">The new password hash.</param>
        /// <returns>True if the password is matched.</returns>
        private bool IsNewPasswordInHistory(
            User user, 
            string newPassword,
            out byte[] newHash)
        {
            // Initialise result;
            bool result = false;

            // Derive new password hash.
            PasswordUtility.CreateHash(newPassword, user.PasswordSalt, out newHash);

            // False if password history is empty.
            if (user.PasswordHistoryHash == null) 
                return false;

            // True if password matches one in history.
            int slot = default(int);
            byte[] oldHash = new byte[PasswordByteLength];
            for (int i = 0; i < (PasswordHistoryCount); i++)
            {
                // Read the historical password.
                for (int j = 0; j < PasswordByteLength; j++)
                {
                    oldHash[j] = user.PasswordHistoryHash[slot];
                    slot++;
                }

                // Compare the new & old return true if a match is found.
                if (newHash.AreEqualArrays(oldHash))
                {
                    result = true;
                    break;
                }
            }

            // Return.
            return result;
        }

        #endregion Private methods
    }
}