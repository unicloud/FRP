#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 18:42:21
// 文件名：MailAddressFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.MailAddressAgg
{
    /// <summary>
    ///     邮箱账号工厂
    /// </summary>
    public static class MailAddressFactory
    {
        /// <summary>
        ///     创建邮箱账号
        /// </summary>
        /// <returns>邮箱账号</returns>
        public static MailAddress CreateMailAddress()
        {
            var mailAddress = new MailAddress();

            mailAddress.GenerateNewIdentity();
            return mailAddress;
        }

        /// <summary>
        /// 创建邮箱账号
        /// </summary>
        /// <param name="smtpHost">发送服务器</param>
        /// <param name="pop3Host">接收服务器</param>
        /// <param name="sendPort">发送端口</param>
        /// <param name="receivePort">接收端口</param>
        /// <param name="loginUser">邮箱登陆名</param>
        /// <param name="loginPassword">邮箱密码</param>
        /// <param name="address">电子邮件地址</param>
        /// <param name="displayName">账号显示名称</param>
        /// <param name="sendSsl">发送要求安全连接</param>
        /// <param name="startTls">使用 StartTLS加密传输</param>
        /// <param name="receiveSsl">接收要求安全连接</param>
        /// <param name="serverType">服务器类型</param>
        /// <returns>邮箱账号</returns>
        public static MailAddress CreateMailAddress(string smtpHost,string pop3Host,int sendPort,
            int receivePort,string loginUser,string loginPassword,string address,string displayName,
            bool sendSsl,bool startTls,bool receiveSsl,int serverType)
        {
            var mailAddress = new MailAddress();

            mailAddress.GenerateNewIdentity();
            mailAddress.SetAddress(address);
            mailAddress.SetDisplayName(displayName);
            mailAddress.SetLoginPassword(loginPassword);
            mailAddress.SetLoginUser(loginUser);
            mailAddress.SetPop3Host(pop3Host);
            mailAddress.SetReceivePort(receivePort);
            mailAddress.SetReceiveSSL(receiveSsl);
            mailAddress.SetSendPort(sendPort);
            mailAddress.SetServerType(serverType);
            mailAddress.SetSmtpHost(smtpHost);
            mailAddress.SetStartTLS(startTls);
            mailAddress.SetSendSSL(sendSsl);
            return mailAddress;
        }
    }
}