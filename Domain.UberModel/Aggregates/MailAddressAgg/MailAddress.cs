#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 16:46:44
// 文件名：MailAddress
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.MailAddressAgg
{
    /// <summary>
    ///     邮箱账号聚合根
    /// </summary>
    public class MailAddress : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal MailAddress()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 发送服务器
        /// </summary>
        public string SmtpHost { get; private set; }

        /// <summary>
        /// 接收服务器
        /// </summary>
        public string Pop3Host { get; private set; }

        /// <summary>
        /// 发送端口
        /// </summary>
        public int SendPort { get; private set; }

        /// <summary>
        /// 接收端口
        /// </summary>
        public int ReceivePort { get; private set; }

        /// <summary>
        /// 邮箱登陆名
        /// </summary>
        public string LoginUser { get; private set; }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string LoginPassword { get; private set; }

        /// <summary>
        ///     电子邮件地址
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// 账号显示名称
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        ///     发送要求安全连接
        /// </summary>
        public bool SendSSL { get; private set; }

        /// <summary>
        ///     使用 StartTLS加密传输
        /// </summary>
        public bool StartTLS { get; private set; }

        /// <summary>
        ///     接收要求安全连接
        /// </summary>
        public bool ReceiveSSL { get; private set; }

        /// <summary>
        ///     服务器类型
        /// </summary>
        public int ServerType { get; private set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///   设置发送服务器
        /// </summary>
        /// <param name="smtpHost">发送服务器</param>
        public void SetSmtpHost(string smtpHost)
        {
            if (string.IsNullOrWhiteSpace(smtpHost))
            {
                throw new ArgumentException("发送服务器参数为空！");
            }

            SmtpHost = smtpHost;
        }

        /// <summary>
        ///   设置接收服务器
        /// </summary>
        /// <param name="pop3Host">接收服务器</param>
        public void SetPop3Host(string pop3Host)
        {
            if (string.IsNullOrWhiteSpace(pop3Host))
            {
                throw new ArgumentException("接收服务器参数为空！");
            }

            Pop3Host = pop3Host;
        }

        /// <summary>
        ///   设置发送端口
        /// </summary>
        /// <param name="sendPort">发送端口</param>
        public void SetSendPort(int sendPort)
        {
            SendPort = sendPort;
        }

        /// <summary>
        ///   设置接收端口
        /// </summary>
        /// <param name="receivePort">接收端口</param>
        public void SetReceivePort(int receivePort)
        {
            ReceivePort = receivePort;
        }

        /// <summary>
        ///   设置邮箱登陆名
        /// </summary>
        /// <param name="loginUser">邮箱登陆名</param>
        public void SetLoginUser(string loginUser)
        {
            if (string.IsNullOrWhiteSpace(loginUser))
            {
                throw new ArgumentException("邮箱登陆名参数为空！");
            }

            LoginUser = loginUser;
        }

        /// <summary>
        ///   设置邮箱密码
        /// </summary>
        /// <param name="loginPassword">邮箱密码</param>
        public void SetLoginPassword(string loginPassword)
        {
            if (string.IsNullOrWhiteSpace(loginPassword))
            {
                throw new ArgumentException("邮箱密码参数为空！");
            }

            LoginPassword = loginPassword;
        }

        /// <summary>
        ///   设置电子邮件地址
        /// </summary>
        /// <param name="address">电子邮件地址</param>
        public void SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("电子邮件地址参数为空！");
            }

            Address = address;
        }

        /// <summary>
        ///   设置账号显示名称
        /// </summary>
        /// <param name="displayName">账号显示名称</param>
        public void SetDisplayName(string displayName)
        {
            if (string.IsNullOrWhiteSpace(displayName))
            {
                throw new ArgumentException("账号显示名称参数为空！");
            }

            DisplayName = displayName;
        }

        /// <summary>
        ///   设置发送要求安全连接
        /// </summary>
        /// <param name="sendSSL">发送要求安全连接</param>
        public void SetSendSSL(bool sendSSL)
        {
            SendSSL = sendSSL;
        }

        /// <summary>
        ///   设置使用 StartTLS加密传输
        /// </summary>
        /// <param name="startTLS">使用 StartTLS加密传输</param>
        public void SetStartTLS(bool startTLS)
        {
            StartTLS = startTLS;
        }

        /// <summary>
        ///   设置接收要求安全连接
        /// </summary>
        /// <param name="receiveSSL">接收要求安全连接</param>
        public void SetReceiveSSL(bool receiveSSL)
        {
            ReceiveSSL = receiveSSL;
        }

        /// <summary>
        ///   设置服务器类型
        /// </summary>
        /// <param name="serverType">服务器类型</param>
        public void SetServerType(int serverType)
        {
            ServerType = serverType;
        }

        #endregion
    }
}
