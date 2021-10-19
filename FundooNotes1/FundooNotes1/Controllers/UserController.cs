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
using Microsoft.Extensions.Logging;

namespace FundooNotes1.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;

        private readonly ILogger<UserController> logger;
        
        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.logger = logger;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] UserModel user)
        {
            try
            {
                this.logger.LogInformation(user.FirstName + " " + user.LastName + " is trying to register");
                string resultMessage = await this.manager.Register(user);
                if (resultMessage.Equals("Registration Successfull"))
                {
                    this.logger.LogInformation(user.FirstName + " " + user.LastName + " "+resultMessage);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    this.logger.LogWarning(resultMessage);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            try
            {
                this.logger.LogInformation(loginModel.Emailid+ " is trying to login");
                string tokenString = this.manager.GenerateToken(loginModel.Emailid);
                string message = this.manager.Login(loginModel);
                if (message.Equals("Login Successfull"))
                {
                    this.logger.LogInformation(loginModel.Emailid + " logged in successfully and token generated is "+tokenString);
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
                    return this.Ok(new { Status = true, Message = message , Data = data, tokenString});
                }
                else if (message.Equals("Invalid Password"))
                {
                    this.logger.LogWarning("Password is Invalid for"+loginModel.Emailid);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = message });
                }
                else
                {
                    this.logger.LogWarning(message);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/forgotpassword")]
        public IActionResult ForgotPassword(string Emailid)
        {
            try
            {
                this.logger.LogInformation(Emailid + " is using forgot password");
                string message = this.manager.ForgotPassword(Emailid);
                if (message.Equals("Check Your Email"))
                {
                    this.logger.LogInformation(message);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = message });
                }
                else
                {
                    this.logger.LogWarning(message);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            try
            {
                this.logger.LogInformation(resetPasswordModel.Emailid + " is trying to reset the password");
                string message = await this.manager.ResetPassword(resetPasswordModel);
                if (message.Equals("Password Reset Successfull"))
                {
                    this.logger.LogInformation("Reset Password Successfull for " + resetPasswordModel.Emailid);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = message });
                }
                else
                {
                    this.logger.LogWarning("Reset Password Failed for " + resetPasswordModel.Emailid);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
