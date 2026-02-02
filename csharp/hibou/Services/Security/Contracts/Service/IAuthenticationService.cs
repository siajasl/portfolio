using System.ServiceModel;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using Keane.CH.Framework.Services.Security.Contracts.Message;

namespace Keane.CH.Framework.Services.Security.Contracts.Service
{
    /// <summary>
    /// Service operations for authenticating a user.
    /// </summary>
    [ServiceContract(Namespace = "www.Keane.com/CH/2009/01/Services/Security")]
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticates against the passed connection credentials.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        /// <remarks>
        /// This supports simple username only authentication scenarios.
        /// </remarks>
        [OperationContract()]
        AuthenticateConnectResponse
            AuthenticateConnect(AuthenticateConnectRequest request);

        /// <summary>
        /// Authenticates against the passed login user credentials.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        [OperationContract()]
        AuthenticateLoginResponse
            AuthenticateLogin(AuthenticateLoginRequest request);

        /// <summary>
        /// Authenticates against the passed initial user credentials.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        [OperationContract()]
        AuthenticateInitialisationResponse
            AuthenticateInitialisation(AuthenticateInitialisationRequest request);

        /// <summary>
        /// Step one of forgotten user credentials authentication.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        [OperationContract()]
        AuthenticateForgottenStepOneResponse
            AuthenticateForgottenStepOne(AuthenticateForgottenStepOneRequest request);

        /// <summary>
        /// Step two of forgotten user credentials authentication.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        [OperationContract()]
        AuthenticateForgottenStepTwoResponse
            AuthenticateForgottenStepTwo(AuthenticateForgottenStepTwoRequest request);

        /// <summary>
        /// Authenticates against the passed changed user credentials.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        [OperationContract()]
        AuthenticateChangeResponse
            AuthenticateChange(AuthenticateChangeRequest request);
    }
}