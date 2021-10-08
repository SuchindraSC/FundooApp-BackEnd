using FundooModel;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;
        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public string Login(LoginModel user)
        {
            throw new NotImplementedException();
        }

        public string Register(UserModel user)
        {
            try
            {
                this.userContext.Users.Add(user);
                this.userContext.SaveChanges();
                return "Registration Successfull";
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
