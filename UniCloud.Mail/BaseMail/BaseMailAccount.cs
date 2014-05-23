namespace UniCloud.Mail
{
    public class BaseMailAccount
    {
        public string MailAddress { get; set; }
        public string DisplayName { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        //发送设置
        public string SendHost { get; set; }
        public int SendPort { get; set; }
        public bool SendSsl { get; set; }
        public bool SendStartTLS { get; set; }

        //接收设置
        public string ReceiveHost { get; set; }
        public int ReceivePort { get; set; }
        public bool ReceiveSsl { get; set; }
    }
}