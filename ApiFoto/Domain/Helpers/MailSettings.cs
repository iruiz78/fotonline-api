namespace ApiFoto.Domain
{
    public class MailSettings
    {
        public string Smtp { get; set; }
        public int PortNumber { get; set; }
        public bool EnabledSSL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}
