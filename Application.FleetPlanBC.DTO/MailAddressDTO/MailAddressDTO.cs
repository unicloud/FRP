#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:30:39
// 文件名：MailAddressDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 邮箱账号
    /// </summary>
    [DataServiceKey("Id")]
    public class MailAddressDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// 发送服务器
        /// </summary>
        public string SmtpHost { get; set; }

        /// <summary>
        /// 接收服务器
        /// </summary>
        public string Pop3Host { get; set; }

        /// <summary>
        /// 发送端口
        /// </summary>
        public int SendPort { get; set; }

        /// <summary>
        /// 接收端口
        /// </summary>
        public int ReceivePort { get; set; }

        /// <summary>
        /// 邮箱登陆名
        /// </summary>
        public string LoginUser { get; set; }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string LoginPassword { get; set; }

        /// <summary>
        ///     电子邮件地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 账号显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     发送要求安全连接
        /// </summary>
        public bool SendSSL { get; set; }

        /// <summary>
        ///     使用 StartTLS加密传输
        /// </summary>
        public bool StartTLS { get; set; }

        /// <summary>
        ///     接收要求安全连接
        /// </summary>
        public bool ReceiveSSL { get; set; }

        /// <summary>
        ///     服务器类型
        /// </summary>
        public int ServerType { get; set; }

        #endregion
    }
}
