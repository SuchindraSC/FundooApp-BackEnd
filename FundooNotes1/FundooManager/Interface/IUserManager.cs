using FundooModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        Task<string> Register(UserModel user);
        string Login(LoginModel loginModel);
        string ForgotPassword(string Emailid);
        Task<string> ResetPassword(ResetPasswordModel resetPasswordModel);
    }
}
