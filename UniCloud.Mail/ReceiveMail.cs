using System;
using System.Collections.Generic;
using LumiSoft.Net.Mime;
using LumiSoft.Net.POP3.Client;

namespace UniCloud.Mail
{
    public class ReceiverMail
    {
        private readonly BaseMailAccount _receiveAccount;
        private readonly ManageMail _invalidMails;
        private readonly POP3_Client _pop3Client;
        private List<POP3_ClientMessage> _ValidMails;

        public ReceiverMail(BaseMailAccount receiver)
        {
            _pop3Client = new POP3_Client();
            _receiveAccount = receiver;//需要提前设置好接收账号用户密码及接收服务器，接收端口，是否ssl
            _invalidMails = new ManageMail();
        }

        /// <summary>
        /// 是否已经接收过了
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        private bool IsReceived(string messageId)
        {
            return _invalidMails.Contains(messageId);
        }

        private bool AddInValidMailId(string messageId)
        {
            return _invalidMails.Add(messageId);
        }

        public void ClearReceiveMails()
        {
            try
            {
                foreach (ReceiverObject ro in _ReceiverObjects)
                {
                    if (ro.Saved && ro.Message != null)
                    {
                        ro.Message.MarkForDeletion();
                    }
                }
            }
            catch (Exception ex)
            {
                // UniCloud.Log.WindowsLog.Write(ex.Message);
            }
            try
            {
                _Pop3Client.Disconnect();
            }
            catch
            {
            }
            try
            {
                _ReceiverObjects.Clear();
            }
            catch
            {
            }
        }

        private bool IsValidAttachment(string FileName)
        {
            return FileName != null && (FileName.Contains("Request") ||
                                        FileName.Contains("Plan") ||
                                        FileName.Contains("ApprovalDoc") ||
                                        FileName.Contains("OperationHistory") ||
                                        FileName.Contains("AircraftBusiness") ||
                                        FileName.Contains("OwnershipHistory"));
        }

        private bool DecodeMessageAttachment(POP3_ClientMessage message)
        {
            bool _IsValid = false;
            var ro = new ReceiverObject();
            //获取这封邮件的内容
            byte[] bytes = message.MessageToByte();
            //解析从Pop3服务器发送过来的邮件附件
            Mime m = Mime.Parse(bytes);

            //二个月前的不接收了
            if (m.MainEntity.Date < DateTime.Now.AddDays(-60))
            {
                return false;
            }
            //遍历所有附件
            foreach (MimeEntity me in m.Attachments)
            {
                //附件有效
                if (!IsValidAttachment(me.ContentType_Name))
                {
                    continue;
                }
                //解压邮件
                object obj = DeSerialObj(me.Data);
                if (obj != null)
                {
                    ro.Objects.Add(obj);
                    _IsValid = true;
                }
            }
            if (_IsValid)
            {
                _ReceiverObjects.Add(ro);
                ro.Message = message;
            }
            return _IsValid;
        }

        public bool Receive()
        {
            //与Pop3服务器建立连接
            _pop3Client.Connect(_receiveAccount.ReceiveHost, _receiveAccount.ReceivePort);
            //验证身份
            _pop3Client.Authenticate(_receiveAccount.UserName, _receiveAccount.Password, true);
            //获取邮件信息列表
            POP3_ClientMessageCollection infos = _pop3Client.Messages;

            //POP3_ClientMessage info;
            //for (int i = infos.Count-1; i>=0; i--)
            foreach (POP3_ClientMessage info in infos)
            {
                // info = infos[i];
                //每封Email会有一个在Pop3服务器范围内唯一的Id,检查这个Id是否存在就可以知道以前有没有接收过这封邮件
                if (!IsReceived(info.UID))
                {
                    if (DecodeMessageAttachment(info))
                    {
                        if (_ReceiverObjects.Count() >= 20)
                        {
                            break;
                        }
                    }
                    else
                    {
                        AddInValidMailID(info.UID);
                    }
                }
            }
            return _ReceiverObjects.Count() > 0;
        }
    }
}