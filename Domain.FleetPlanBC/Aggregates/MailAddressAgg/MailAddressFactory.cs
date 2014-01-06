﻿#region 版本信息

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
    }
}