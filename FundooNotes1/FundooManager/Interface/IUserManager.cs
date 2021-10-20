// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Suchindra Chitnis"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using global::FundooModel;

    /// <summary>
    /// interface IUserManager
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">UserModel user</param>
        /// <returns>Returns string if Register is successful</returns>
        public Task<string> Register(UserModel user);

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">LoginModel loginModel</param>
        /// <returns>returns string if login is successful</returns>
        public string Login(LoginModel loginModel);

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="Emailid">string Emailid</param>
        /// <returns>Returns string if mail sent successful else false</returns>
        public string ForgotPassword(string Emailid);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">ResetPasswordModel resetPasswordModel</param>
        /// <returns>Returns string if the password is successfully reset</returns>
        public Task<string> ResetPassword(ResetPasswordModel resetPasswordModel);

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="Emailid">string Emailid)</param>
        /// <returns>Returns the token when user logins</returns>
        public string GenerateToken(string Emailid);
    }
}
