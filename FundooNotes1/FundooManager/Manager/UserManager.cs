// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Suchindra Chitnis"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using global::FundooManager.Interface;
    using global::FundooModel;
    using global::FundooRepository.Interface;

    /// <summary>
    /// class UserManager
    /// </summary>
    public class UserManager : IUserManager
    {
        /// <summary>
        /// IUserRepository repository
        /// </summary>
        private readonly IUserRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="repository">IUserRepository repository</param>
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">UserModel user</param>
        /// <returns>Returns string if Register is successful</returns>
        /// <exception cref="System.Exception"></exception>
        public Task<string> Register(UserModel user)
        {
            try
            {
                return this.repository.Register(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">LoginModel loginModel</param>
        /// <returns>returns string if login is successfull</returns>
        /// <exception cref="System.Exception"></exception>
        public string Login(LoginModel loginModel)
        {
            try
            {
                return this.repository.Login(loginModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="Emailid">string Emailid</param>
        /// <returns>Returns string if mail sent successful else false</returns>
        /// <exception cref="System.Exception"></exception>
        public string ForgotPassword(string Emailid)
        {
            try
            {
                return this.repository.ForgotPassword(Emailid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">ResetPasswordModel resetPasswordModel</param>
        /// <returns>Returns string if the password is successfully reset</returns>
        /// <exception cref="System.Exception"></exception>
        public Task<string> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                return this.repository.ResetPassword(resetPasswordModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="Emailid">string Emailid)</param>
        /// <returns>Returns the token when user logins</returns>
        /// <exception cref="System.Exception"></exception>
        public string GenerateToken(string Emailid)
        {
            try
            {
                return this.repository.GenerateToken(Emailid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
