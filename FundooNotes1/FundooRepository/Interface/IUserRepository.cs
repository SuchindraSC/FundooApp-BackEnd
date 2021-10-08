using FundooModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Interface
{
    public interface IUserRepository
    {
        string Register(UserModel user);
        string Login(LoginModel user);
    }
}
