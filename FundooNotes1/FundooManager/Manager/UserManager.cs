using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        public string Login(LoginModel user)
        {
            try
            {
                return this.repository.Login(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Register(UserModel user)
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
    }
}
