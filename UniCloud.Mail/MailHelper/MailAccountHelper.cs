using System;
using System.Linq;

namespace UniCloud.Mail
{

    public class MailAccountHelper
    {
        public static BaseMailAccount GetMailAccountFromAddr(string address, string displayName, string loginUser, string originPassword,
            string pop3Host,int receivePort,bool receiveSsl,string sendHost,int sendPort,bool sendSsl,bool startTls)
        {

            var account = new BaseMailAccount
                {
                    MailAddress = address,
                    DisplayName = displayName,
                    UserName = loginUser,
                    Password = originPassword,
                    ReceiveHost = pop3Host,
                    ReceivePort = receivePort,
                    ReceiveSsl = receiveSsl,
                    SendHost = sendHost,
                    SendPort = sendPort,
                    SendSsl = sendSsl,
                    SendStartTLS = startTls
                };

            return account;
        }

        public static BaseMailAccount GetDefaultMailAccount()
        {
            var account = new BaseMailAccount
                {
                    MailAddress = "",
                    DisplayName = "",
                    UserName = "",
                    Password = "",
                    ReceiveHost = "",
                    ReceivePort = 110,
                    ReceiveSsl = false,
                    SendHost = "",
                    SendPort = 25,
                    SendSsl = false,
                    SendStartTLS = false
                };

            return account;
        }
    }
}
