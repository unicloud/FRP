using System;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography;


namespace UniCloud.Mail
{
    public class NetSelfSend : ISendMail
    {


        private SmtpClient _Client;

        private string ErrorMessage;

        public NetSelfSend()
        {
            _Client = new SmtpClient();
        }

        private void EncodeErrorMessage(Exception ex)
        {
            string CharReturn = "\n";
            if (ex.InnerException != null)
            {
                ErrorMessage = ex.InnerException.Message + CharReturn + ex.Message;
            }
            else
            {
                ErrorMessage = ex.Message;
            }
        }

        private MailMessage CreateTestMail(MailMessage message)
        {
            //MailMessage msg = new MailMessage(Sender.MailAddress, Receiver.MailAddress);
            MailMessage msg = message;
            msg.Subject = "Test Send Mail By Net.Mail";
            msg.Body = "这是一份测试邮件，来自厦门至正测试程序";

            return msg;
        }

        #region 发送邮箱设置
        private void InitSmtpClient(BaseMailAccount sender)
        {
            // 指定 smtp 服务器地址
            _Client.Host = sender.SendHost;
            // 指定 smtp 服务器的端口，默认是25，如果采用默认端口，可省去
            _Client.Port = sender.SendPort;
            // 将smtp的出站方式设为 Network
            _Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            // SMTP服务器需要身份认证
            _Client.UseDefaultCredentials = true;
            // smtp服务器是否启用SSL加密
            _Client.EnableSsl = sender.SendSsl;
            //设置发信凭据
            _Client.Credentials = new NetworkCredential(sender.UserName, sender.Password);
        }
        #endregion


        #region 发送邮件
        private int SendMessage(MailMessage message)
        {
            try
            {
                _Client.Send(message);
                return 0;
            }
            catch (Exception ex)
            {
                this.EncodeErrorMessage(ex);
                return -1;
            }
        }
        #endregion

        #region 断开连接
        private void CloseConnection()
        {
            try
            {
                _Client.Dispose();
            }
            catch (Exception ex)
            {
                this.EncodeErrorMessage(ex);
            }
        }
        #endregion


        public bool CanSend(BaseMailAccount Sender)
        {
            return Sender.SendSsl == true;
        }

        public int SendTest(BaseMailAccount sender, MailMessage message)
        {
            try
            {
                //生成邮件
                MailMessage msg = CreateTestMail(message);
                //邮箱设置
                InitSmtpClient(sender);
                //发送邮件
                int intSend = SendMessage(msg);
                //断开连接
                CloseConnection();

                return intSend;
            }
            catch (Exception ex)
            {
                this.EncodeErrorMessage(ex);
                return -3;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="sender">发件账户</param>
        /// <param name="message"></param>
        public int SendMail(BaseMailAccount sender, MailMessage message)
        {
            try
            {
                //邮箱设置
                InitSmtpClient(sender);
                //发送邮件
                int intSend = SendMessage(message);
                //断开连接
                CloseConnection();
                return intSend;
            }
            catch (Exception ex)
            {
                this.EncodeErrorMessage(ex);
                return -3;
            }
        }

        public string GetLastErrorMsg()
        {
            return ErrorMessage;
        }

    }
}
