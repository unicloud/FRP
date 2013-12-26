#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 11:17:56
// 文件名：MailAddress
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.MailAddressAgg
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
        /// </summary>
        public string SmtpHost { get; private set; }

        /// <summary>
        /// </summary>
        public string Pop3Host { get; private set; }

        /// <summary>
        /// </summary>
        public int SendPort { get; private set; }

        /// <summary>
        /// </summary>
        public int ReceivePort { get; private set; }

        /// <summary>
        /// </summary>
        public string LoginUser { get; private set; }

        /// <summary>
        /// </summary>
        public string LoginPassword { get; private set; }

        /// <summary>
        ///     电子邮件账号
        /// </summary>
        public string Address { get; private set; }

        /// 账号名称
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



        #endregion
    }
}
