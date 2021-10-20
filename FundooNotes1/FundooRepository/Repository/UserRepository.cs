

namespace FundooRepository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using Experimental.System.Messaging;
    using StackExchange.Redis;
    using FundooModel;
    using FundooRepository.Context;
    using FundooRepository.Interface;

    /// <summary>
    /// class UserRepository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// UserContext userContext
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// IConfiguration configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="userContext">UserContext userContext</param>
        /// <param name="configuration">IConfiguration configuration</param>
        public UserRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.configuration = configuration;
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">UserModel user</param>
        /// <returns>Returns string if Register is successful</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<string> Register(UserModel user)
        {
            try
            {
                var users =  this.userContext.Users.Any(x => x.Emailid == user.Emailid);

                if (!users)
                {
                    user.Password = Encryptdata(user.Password);
                    this.userContext.Users.Add(user);
                    await this.userContext.SaveChangesAsync();
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

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">LoginModel loginModel</param>
        /// <returns>returns string if login is successful</returns>
        /// <exception cref="System.Exception"></exception>
        public string Login(LoginModel loginModel)
        {
            try
            {
                var users = this.userContext.Users.Any(x => x.Emailid == loginModel.Emailid);
                
                if (users)
                {
                    loginModel.Password = Encryptdata(loginModel.Password);
                    var user = this.userContext.Users.Where(x => x.Emailid == loginModel.Emailid).SingleOrDefault();

                    ConnectionMultiplexer connectionmultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionmultiplexer.GetDatabase();
                    database.StringSet(key: "First Name", user.FirstName);
                    database.StringSet(key: "Last Name", user.LastName);
                    database.StringSet(key: "userId", user.UserId.ToString());

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
                var users = this.userContext.Users.Any(x => x.Emailid == Emailid);

                if (users)
                {
                    MessageQueue msgqueue;
                    if (MessageQueue.Exists(@".\Private$\MyQueue"))
                    {
                        msgqueue = new MessageQueue(@".\Private$\MyQueue");
                    }
                    else
                    {
                        msgqueue = MessageQueue.Create(@".\Private$\MyQueue");
                    }

                    Message message = new Message();
                    message.Formatter = new BinaryMessageFormatter();
                    message.Body = $"You requested for forgot password of {Emailid}. Please Reset The Password";
                    msgqueue.Label = "Mail";
                    msgqueue.Send(message);
                    SendEmail(Emailid);
                    return "Check Your Email";

                }
                else
                {
                    return "User Doesn't Exist";
                }
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="Emailid">string Emailid</param>
        /// <exception cref="System.Exception"></exception>
        public void SendEmail(string Emailid)
        {
            try
            {
                MessageQueue msgqueue;
                if (MessageQueue.Exists(@".\Private$\MyQueue"))
                {
                    msgqueue = new MessageQueue(@".\Private$\MyQueue");
                }
                else
                {
                    msgqueue = MessageQueue.Create(@".\Private$\MyQueue");
                }

                var receivequeue = new MessageQueue(@".\Private$\MyQueue");
                var receivemsg = receivequeue.Receive();
                receivemsg.Formatter = new BinaryMessageFormatter();

                MailMessage mail = new MailMessage();
                mail.Body = receivemsg.Body.ToString();
                mail.From = new MailAddress("suchindrasc99@gmail.com");
                mail.To.Add("suchindrasc99@gmail.com");
                mail.Subject = "Fundoo Notes";
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("suchindrasc99@gmail.com", "Suchindra@0899");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (MessageQueueException ex)
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
        public async Task<string> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                resetPasswordModel.Password = Encryptdata(resetPasswordModel.Password);
                var users = this.userContext.Users.Any(x => x.Emailid == resetPasswordModel.Emailid);

                if (users)
                {
                    var user = this.userContext.Users.Where(x => x.Emailid == resetPasswordModel.Emailid).SingleOrDefault();
                    user.Password = resetPasswordModel.Password;
                    await this.userContext.SaveChangesAsync();
                    return "Password Reset Successfull";
                }
                else
                {
                    return "User Doesn't Exist";
                }
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Encryptdatas the specified password.
        /// </summary>
        /// <param name="password">string password</param>
        /// <returns></returns>
        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="Emailid">string Emailid)</param>
        /// <returns>Returns the token when user logins</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public string GenerateToken(string Emailid)
        {
            try
            {
                var key = Encoding.UTF8.GetBytes(this.configuration["SecretKey"]);
                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
                SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, Emailid)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                };
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
                return handler.WriteToken(token);
            }

            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }
    }
}
