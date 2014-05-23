using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace UniCloud.Mail
{
    public class SendContainer
    {
        private List<ISendMail> _SendObjects;

        public SendContainer()
        {
            InitData();
        }

        private void InitData()
        {
            _SendObjects = new List<ISendMail>();
            RegisterSendClass();
        }

        private void RegisterSendClass()
        {
            _SendObjects.Add(new NetSelfSend());
            //_SendObjects.Add(new LumiSoftSend());
        }

        public ISendMail GetSendClassBySender(BaseMailAccount sender)
        {
            foreach (ISendMail ISM in _SendObjects)
            {
                if (ISM.CanSend(sender))
                {
                    return ISM;
                }
            }
            return null;
        }
    }
}
