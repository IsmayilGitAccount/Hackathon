namespace WebApplication1.Helper
{
    public class SmtpOption
    {
        public const string Name = "smtpName";
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
