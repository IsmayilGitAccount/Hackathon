﻿using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using WebApplication1.Helper;
using WebApplication1.Services.Abstrations;

namespace WebApplication1.Services.Implements
{
    public class EmailService : IEmailService
    {

        readonly SmtpClient _smtp;
        readonly MailAddress _from;
        readonly HttpContext Context;
        public EmailService(IOptions<SmtpOption> option, IHttpContextAccessor acc)
        {
            var opt = option.Value;
            _smtp = new(opt.Host, opt.Port);
            _smtp.Credentials = new NetworkCredential(opt.Username, opt.Password);
            _smtp.EnableSsl = true;
            _from = new MailAddress(opt.Username, "Admin");
            Context = acc.HttpContext;
        }
        public void SendEmailConfirmation(string receiver, string name, string token)
        {
            MailAddress to = new(receiver);
            MailMessage msg = new MailMessage(_from, to);
            msg.Subject = "Confirm your email address";
            msg.Body = EmailTemplates.VerifyEmail.Replace("___$name", name).Replace("___$link", token);
            string url = Context.Request.Scheme + "://" + Context.Request.Host + "/Account/VerifyEmail?token=" + token + "&user=" + name;
            msg.IsBodyHtml = true;
            _smtp.Send(msg);
        }
    }
}

