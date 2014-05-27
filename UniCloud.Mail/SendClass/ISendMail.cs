
using System.Net.Mail;

namespace UniCloud.Mail
{
    public interface ISendMail
    {
        int SendTest(BaseMailAccount sender,MailMessage message);
        int SendMail(BaseMailAccount sender, MailMessage message);
        bool CanSend(BaseMailAccount sender);
        string GetLastErrorMsg();
    }
}
