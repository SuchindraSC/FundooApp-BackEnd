using FundooModel;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;
        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public string Register(UserModel user)
        {
            try
            {
                var users = this.userContext.Users.Any(x => x.Emailid == user.Emailid);

                if (!users)
                {
                    this.userContext.Users.Add(user);
                    this.userContext.SaveChanges();
                    return "Registration Successfull";
                }
                else
                {
                    return "User Already Registered";
                }
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Login(LoginModel loginModel)
        {
            try
            {
                var users = this.userContext.Users.Any(x => x.Emailid == loginModel.Emailid);
                
                if (users)
                {
                    var user = this.userContext.Users.Where(x => x.Emailid == loginModel.Emailid).FirstOrDefault();
                    if (user.Password == loginModel.Password)
                    {
                        return "Login Successfull";
                    }
                    else
                    {
                        return "Invalid Password";
                    }
                }
                else
                {
                    return "User Doesn't Exist";
                }
            }
            catch(ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
