using System.Net.Mail;
using System.Net;

namespace WebApplication1.Services;
public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendEmail(string toEmail, string userName)
    {
        var smtpSettings = _configuration.GetSection("smtp");

        string subject = "Congratulations!";
        string body = $"<p>You have been hired. Username: `{userName}`, Password: `Salam123!`</p>";

        string host = smtpSettings["Host"];
        int port = int.Parse(smtpSettings["Port"]);
        string username = smtpSettings["Username"];
        string password = smtpSettings["Password"];

        using (SmtpClient smtpClient = new SmtpClient())
        {
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(username, password);

            MailAddress from = new MailAddress(username, "Admin");
            MailAddress to = new MailAddress(toEmail);

            using (MailMessage msg = new MailMessage(from, to))
            {
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = true;

                smtpClient.Send(msg);
            }
        }
    }
}

