using FundooModel;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Mail;

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

        public string ForgotPassword(ForgotPasswordModel forgotPasswordModel)
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
                    mail.Subject = "Test Mail";
                    mail.Body = "This is for testing SMTP mail from GMAIL";

                    SmtpServer.Port = 587;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("suchindrasc99@gmail.com", "Suchindra@0899");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                    return "Email Sent";
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

        public string ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var users = this.userContext.Users.Any(x => x.Emailid == resetPasswordModel.Emailid);

                if (users)
                {
                    var user = this.userContext.Users.Where(x => x.Emailid == resetPasswordModel.Emailid).FirstOrDefault();
                    user.Password = resetPasswordModel.Password;
                    this.userContext.SaveChanges();
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
    }
}
