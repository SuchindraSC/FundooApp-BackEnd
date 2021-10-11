using FundooModel;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Experimental.System.Messaging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;

        private MessageQueue msgqueue;

        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

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

        public string Login(LoginModel loginModel)
        {
            try
            {
                var users = this.userContext.Users.Any(x => x.Emailid == loginModel.Emailid);
                
                if (users)
                {
                    loginModel.Password = Encryptdata(loginModel.Password);
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

        public async Task<string> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                var users = this.userContext.Users.Any(x => x.Emailid == forgotPasswordModel.Emailid);

                if (users)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("suchindrasc99@gmail.com");
                    mail.To.Add("suchindrasc99@gmail.com");
                    mail.Subject = "Fundoo Notes";
                    mail.Body = $"You requested for forgot password of {forgotPasswordModel.Emailid}. Please Reset The Password";

                    SmtpServer.Port = 587;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("suchindrasc99@gmail.com", "Suchindra@0899");
                    SmtpServer.EnableSsl = true;

                    await SmtpServer.SendMailAsync(mail);
                    return "Email Sent Successsfully";
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

        public async Task<string> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                resetPasswordModel.Password = Encryptdata(resetPasswordModel.Password);
                var users = this.userContext.Users.Any(x => x.Emailid == resetPasswordModel.Emailid);

                if (users)
                {
                    var user = this.userContext.Users.Where(x => x.Emailid == resetPasswordModel.Emailid).FirstOrDefault();
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

        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

        public void sendMessageQueue(ForgotPasswordModel resetLink)
        {
            try
            {
                if (MessageQueue.Exists(@".\Private$\MyQueue"))
                {
                    msgqueue = new MessageQueue(@".\Private$\MyQueue");
                }
                else
                {
                    msgqueue = MessageQueue.Create(@".\Private$\MyQueue");
                }
                msgqueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                Message message = new Message
                {
                    Label = "password reset link",
                    Body = JsonConvert.SerializeObject(resetLink)
                };
                msgqueue.Send(message);
                msgqueue.ReceiveCompleted += msgqueue_ReceiveCompleted;
                msgqueue.BeginReceive();
                msgqueue.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void msgqueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue msgqueue = (MessageQueue)sender;
                Message msg = msgqueue.EndReceive(e.AsyncResult);
                ForgotPasswordModel model = JsonConvert.DeserializeObject<ForgotPasswordModel>(msg.Body.ToString());
                //ForgotPassword(model);
                msgqueue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
