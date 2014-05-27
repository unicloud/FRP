using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace UniCloud.Mail
{
    public class SendMail
    {
        #region 生成邮件

        /// <summary>
        ///     生成邮件附件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="attName"></param>
        /// <returns></returns>
        private Attachment CreateMessageAttachment(Stream stream, string attName)
        {
            var attach = new Attachment(stream, attName);
            return attach;
        }


        /// <summary>
        ///     生成邮件(只支持单接受者，单附件)
        /// </summary>
        /// <param name="sender">发送的账号</param>
        /// <param name="receiver">接收的账号</param>
        /// <param name="stream">附件内容</param>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="attName">附件名称</param>
        /// <returns></returns>
        public MailMessage GenMail(MailAddress sender, MailAddress receiver, Stream stream,
            string mailSubject,
            string attName)
        {
            // 生成邮件
            var message = new MailMessage();
            message.Sender = new MailAddress(sender.Address);
            message.From = sender;
            message.To.Add(receiver);
            message.Subject = mailSubject;
            message.Body = mailSubject + "\r\n" + "来自" + sender.DisplayName;
            message.Attachments.Add(CreateMessageAttachment(stream, attName));
            message.IsBodyHtml = false;
            message.Priority = MailPriority.High;
            message.SubjectEncoding = Encoding.UTF8;
            return message;
        }

        /// <summary>
        ///     生成不加密的普通邮件(支持群发，群抄送，可添加多附件)
        /// </summary>
        /// <param name="sender">发送的账号</param>
        /// <param name="receivers">接收的账号</param>
        /// <param name="ccs">抄送的账号</param>
        /// <param name="body">内容</param>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="attachments">添加的附件</param>
        /// <returns></returns>
        public MailMessage GenNormalMail(MailAddress sender, List<MailAddress> receivers, List<MailAddress> ccs, string mailSubject,
            string body, List<Attachment> attachments)
        {
            // 生成邮件
            var message = new MailMessage();
            message.Sender = new MailAddress(sender.User);
            message.From = sender;
            if (receivers != null && receivers.Count != 0)
            {
                receivers.ForEach(message.To.Add);
            }
            message.Subject = mailSubject;
            message.Body = body;
            message.Priority = MailPriority.High;
            if (attachments != null && attachments.Count != 0)
            {
                attachments.ForEach(message.Attachments.Add);
            }
            return message;
        }

        #endregion

        #region 发送邮件

        public int SendNormalMail(BaseMailAccount sender, MailMessage message)
        {
            try
            {
                var sc = new SendContainer();
                ISendMail ISM = sc.GetSendClassBySender(sender);
                if (ISM != null)
                {
                    return ISM.SendMail(sender, message);
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

        #endregion

        #region 发送测试邮件

        /// <summary>
        ///     测试邮件发送
        /// </summary>
        /// <param name="sender">发送的账号</param>
        /// <param name="receiver">接收的账号</param>
        public int SendTestMail(BaseMailAccount sender, string receiver)
        {
            try
            {
                var sc = new SendContainer();
                ISendMail ISM = sc.GetSendClassBySender(sender);
                if (ISM != null)
                {
                    MailMessage msg=new MailMessage();
                    msg.From = (new MailAddress(sender.MailAddress));
                    msg.To.Add(new MailAddress(receiver));
                    return ISM.SendTest(sender,msg);
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

        #endregion
    }
}