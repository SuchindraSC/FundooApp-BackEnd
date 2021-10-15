using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using FundooRepository.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace FundooNotes1.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] UserModel user)
        {
            try
            {
                string resultMessage = await this.manager.Register(user);
                if (resultMessage.Equals("Registration Successfull"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            try
            {
                string message = this.manager.Login(loginModel);
                if (message.Equals("Login Successfull"))
                {
                    ConnectionMultiplexer connectionmultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionmultiplexer.GetDatabase();
                    string firstName = database.StringGet("First Name");
                    string lastName = database.StringGet("Last Name");
                    int userId = Convert.ToInt32(database.StringGet("userId"));

                    UserModel data = new UserModel
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        UserId = userId,
                        Emailid = loginModel.Emailid
                    };

                    string tokenString = this.manager.GenerateToken(loginModel.Emailid);
                    return this.Ok(new { Status = true, Message = message , Data = data, tokenString});
                }
                else if (message.Equals("Invalid Password"))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = message });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/forgotpassword")]
        public IActionResult ForgotPassword(string Emailid)
        {
            try
            {
                string message = this.manager.ForgotPassword(Emailid);
                if (message.Equals("Check Your Email"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = message });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            try
            {
                string message = await this.manager.ResetPassword(resetPasswordModel);
                if (message.Equals("Password Reset Successfull"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = message });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
