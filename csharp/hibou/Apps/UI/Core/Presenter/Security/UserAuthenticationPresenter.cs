using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Core.Presenter;
using Keane.CH.Framework.Apps.UI.Core.Presenter.Security;
using Keane.CH.Framework.Apps.UI.Core.View.Security;
using Keane.CH.Framework.Services.Resources.Contracts.Data;
using Keane.CH.Framework.Services.Security.Contracts.Message;
using Keane.CH.Framework.Services.Security.Contracts.Service;
using Keane.CH.Framework.Core.Utilities.Caching;
using Keane.CH.Framework.Services.Codes.Contracts;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Security
{
    /// <summary>
    /// UI presenter encapsulating logically related entity operations.
    /// </summary>
    public class UserAuthenticationPresenter : 
        PresenterBase,
        IAuthenticationPresenter
    {
        #region Collaborators

        /// <summary>
        /// Gets or sets an associated service.
        /// </summary>
        public IAuthenticationService AuthenticationService
        { get; set; }

        /// <summary>
        /// Gets or sets a collaborator.
        /// </summary>
        public IEntityCacheAccessor Cache
        { get; set; }

        /// <summary>
        /// Gets or sets the count upon which the authentication lockout will occur.
        /// </summary>
        public int AuthenticationLockout
        { get; set; }

        #endregion Collaborators

        #region IUserAuthenticationPresenter Members

        /// <summary>
        /// Authenticates the passed login user credentials.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void IAuthenticationPresenter.AuthenticateLogin(
            IAuthenticationLoginView view, 
            GuiContext viewContext)
        {
#if DEBUG
            // This is a defualt user that exists in the database
            view.OnAuthenticationSuccess("Debug User", new string[] { "Admin" }, "Debug User", "fr-CH", "fr-CH", 4, new int[] { 2 }, 0, string.Empty, 2);
#else
            // Invoke service operation.
            AuthenticateLoginRequest request = new AuthenticateLoginRequest() 
            {                 
                Context = base.GetRequestContext(viewContext),
                Password = view.Password,
                UserName = view.UserName
            };
            AuthenticateLoginResponse response =
                this.AuthenticationService.AuthenticateLogin(request);

            // Process service response.
            if (response.Succeeded)
                OnAuthenticationSuccess(view, viewContext, response.User);
            else
                OnAuthenticationFailure(view, viewContext);
#endif
        }

        /// <summary>
        /// Authenticates the passed initial user credentials.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void IAuthenticationPresenter.AuthenticateInititalisation(
            IAuthenticationInitialisationView view,
            GuiContext viewContext)
        {
#if DEBUG
            // This is a defualt user that exists in the database
            view.OnAuthenticationSuccess("Debug User", new string[] { "Admin" }, "Debug User", "fr-CH", "fr-CH", 4, new int[] { 2 }, 0, string.Empty, 2);
#else
            // Invoke service operation.
            AuthenticateInitialisationRequest request = new AuthenticateInitialisationRequest()
            {
                Context = base.GetRequestContext(viewContext),
                Password = view.Password,
                NewPassword = view.NewPassword,
                UserName = view.UserName
            };
            AuthenticateInitialisationResponse response =
                this.AuthenticationService.AuthenticateInitialisation(request);

            // Process service response.
            if (response.Succeeded)
                OnAuthenticationSuccess(view, viewContext, response.User);
            else
                OnAuthenticationFailure(view, viewContext);
#endif
        }

        /// <summary>
        /// Authenticates the passed change user credentials.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void IAuthenticationPresenter.AuthenticateChange(
            IAuthenticationChangeView view,
            GuiContext viewContext)
        {
#if DEBUG
            // This is a defualt user that exists in the database
            view.OnAuthenticationSuccess("Debug User", new string[] { "Admin" }, "Debug User", "fr-CH", "fr-CH", 4, new int[] { 2 }, 0, string.Empty, 2);
#else
            // Invoke service operation.
            AuthenticateChangeRequest request = new AuthenticateChangeRequest()
            {
                Context = base.GetRequestContext(viewContext),
                Password = view.Password,
                NewPassword = view.NewPassword,
                UserName = view.UserName
            };
            AuthenticateChangeResponse response =
                this.AuthenticationService.AuthenticateChange(request);

            // Process service response.
            if (response.Succeeded)
                OnAuthenticationSuccess(view, viewContext, response.User);
            else
                OnAuthenticationFailure(view, viewContext);
#endif
        }

        /// <summary>
        /// Authenticates the step one forgotten passed user credentials.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void IAuthenticationPresenter.AuthenticateForgottenStepOne(
            IAuthenticationForgottenStepOneView view,
            GuiContext viewContext)
        {
#if DEBUG
            // This is a defualt user that exists in the database
            view.OnAuthenticationSuccess("Debug User", new string[] { "Admin" }, "Debug User", "fr-CH", "fr-CH", 4, new int[] { 2 }, 0, string.Empty, 2);
#else
            // Invoke service operation.
            AuthenticateForgottenStepOneRequest request = new AuthenticateForgottenStepOneRequest()
            {
                Context = base.GetRequestContext(viewContext),
                EmailAddress = view.EmailAddress,
                UserName = view.UserName
            };
            AuthenticateForgottenStepOneResponse response =
                this.AuthenticationService.AuthenticateForgottenStepOne(request);

            // Process service response.
            if (response.Succeeded)
            {
                view.ProceedToStepTwo(response.User.PasswordQuestion);
            }
            else
                OnAuthenticationFailure(view, viewContext);
#endif
        }

        /// <summary>
        /// Authenticates the step two forgotten passed user credentials.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void IAuthenticationPresenter.AuthenticateForgottenStepTwo(
            IAuthenticationForgottenStepTwoView view,
            GuiContext viewContext)
        {
#if DEBUG
            // This is a defualt user that exists in the database
            view.OnAuthenticationSuccess("Debug User", new string[] { "Admin" }, "Debug User", "fr-CH", "fr-CH", 4, new int[] { 2 }, 0, string.Empty, 2);
#else
            // Invoke service operation.
            AuthenticateForgottenStepTwoRequest request = new AuthenticateForgottenStepTwoRequest()
            {
                Context = base.GetRequestContext(viewContext),
                EmailAddress = view.EmailAddress,
                PasswordQuestionAnswer = view.PasswordQuestionAnswer,
                UserName = view.UserName
            };
            AuthenticateForgottenStepTwoResponse response =
                this.AuthenticationService.AuthenticateForgottenStepTwo(request);

            // Process service response.
            if (response.Succeeded)
                OnAuthenticationSuccess(view, viewContext, response.User);
            else
                OnAuthenticationFailure(view, viewContext);
#endif
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Handles the situation when auithentication has succeeded.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        /// <param name="user">The user that has successfully authenticated.</param>
        private void OnAuthenticationSuccess(
            IAuthenticationView view, 
            GuiContext viewContext,
            User user)
        {
            // Reset authentication failure count.
            view.AuthenticationFailureCount = 0;
            
            // Derive culture information.
            SupportedCulture culture = 
                this.Cache.Get<SupportedCulture>(user.RefIdSupportedCulture);
            string cultureCode = culture.Code;
            string uiCultureCode = culture.Code;

            // Derive canton code.
            // TODO move out.
            string userCantonCode = string.Empty;
            if (user.RefIdCanton > 0)
            {
                CodeItem cantonCode = 
                    this.Cache.Get<CodeItem>(user.RefIdCanton);
                if (cantonCode != null)
                    userCantonCode = cantonCode.Value;
            }

            // Derive role(s).
            string[] userRoles; int[] userRoleTypeIds;
            UserRoleType userRoleType =
                this.Cache.Get<UserRoleType>(user.RefIdUserRoleType);
            if (userRoleType != null)
            {
                userRoles = new string[] { userRoleType.Name };
                userRoleTypeIds = new int[] { userRoleType.Id };
            }                
            else
            {
                userRoles = new string[] { };
                userRoleTypeIds = new int[] { };
            }

            // Callback to the gui passing required information.
            // TODO replace parameter list with a class.
            view.OnAuthenticationSuccess(
                user.UserName, 
                userRoles,
                user.GetDisplayName(),
                cultureCode,
                uiCultureCode,
                user.Id,
                userRoleTypeIds, 
                user.RefIdCanton,
                userCantonCode,
                user.RefIdSupportedCulture);
        }

        /// <summary>
        /// Handles the situation when authentication has failed.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        /// <param name="credentials">The credentials unsuccessfully authenticated.</param>
        private void OnAuthenticationFailure(
            IAuthenticationView view,
            GuiContext viewContext)
        {
            // Either lockout or report failure.
            view.AuthenticationFailureCount++;
            if (view.AuthenticationFailureCount >= AuthenticationLockout)
                view.OnAuthenticationLockout();
            else
                view.OnAuthenticationFailure();
        }

        #endregion Private methods
    }
}