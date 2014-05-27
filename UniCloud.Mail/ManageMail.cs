using System.Collections.Generic;

namespace UniCloud.Mail
{
   public class ManageMail
    {
        private List<string> _MailIds;

       public ManageMail()
        {
            _MailIds = new List<string>();
        }

        public bool Add(string MailId)
        {
            _MailIds.Add(MailId);
            return true;
        }

        public bool Contains(string MailId)
        {
            return _MailIds.Contains(MailId);
        }

        public void Clear()
        {
            _MailIds.Clear();
        }

    }
}
