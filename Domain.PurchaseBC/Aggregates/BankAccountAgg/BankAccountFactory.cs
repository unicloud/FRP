#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/04，13:11
// 文件名：BankAccountFactory.cs
// 程序集：UniCloud.Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.BankAccountAgg
{
    /// <summary>
    ///     银行账户工厂
    /// </summary>
    public static class BankAccountFactory
    {
        /// <summary>
        ///     创建银行账户
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns>创建的银行账户</returns>
        public static BankAccount CreateBankAccount(string account)
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                throw new ArgumentNullException("account");
            }

            var bankAccount = new BankAccount
            {
                Account = account,
            };

            return bankAccount;
        }
    }
}