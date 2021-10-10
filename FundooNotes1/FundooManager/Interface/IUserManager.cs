using FundooModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        string Register(UserModel user);
        string Login(LoginModel loginModel);
        string ForgotPassword(ForgotPasswordModel forgotPasswordModel);
        string ResetPassword(ResetPasswordModel resetPasswordModel);
    }
}
