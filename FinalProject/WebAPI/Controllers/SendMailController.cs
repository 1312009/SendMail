using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace SendMail.Controllers
{
    [RoutePrefix("api/SendMail")]
    public class SendMailController : ApiController
    {
        [Route("Register")]
        [HttpGet]
        public string Register(string email)
        {
            string code = CreatePassword() ;
            MailMessage mailMessag = new MailMessage(ConfigurationManager.AppSettings.Get("Email"), email);
            mailMessag.Subject = "Gửi mã xác nhận";
            mailMessag.Body = "Mã xác nhận của bạn là: " + code;
            SmtpClient client = new SmtpClient();
            client.Send(mailMessag);
            return code;
        }
        [Route("Forget")]
        [HttpGet]
        public string Forget(string email)
        {
            string code = CreatePassword();
            MailMessage mailMessag = new MailMessage(ConfigurationManager.AppSettings.Get("Email"), email);
            mailMessag.Subject = "Gửi mật khẩu mới";
            mailMessag.Body = "Mật khẩu mới của bạn là: " + code;
            SmtpClient client = new SmtpClient();
            client.Send(mailMessag);
            return code;
        }
        public string CreatePassword()
        {
            string _allowedChars = "ABCDEFGHIJKMNOPQRSTUVWXYZ0123456789";

            Random randNum = new Random();

            char[] chars = new char[6];

            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < 6; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
                if (chars[i] == '0' || chars[i] == '1' || chars[i] == '2' || chars[i] == '3' || chars[i] == '4'
              || chars[i] == '5' || chars[i] == '6' || chars[i] == '7' || chars[i] == '8' || chars[i] == '9')
                {
                    _allowedChars = "ABCDEFGHIJKMNOPQRSTUVWXYZ";
                }
            }
            return new string(chars);
        }

    }
}
