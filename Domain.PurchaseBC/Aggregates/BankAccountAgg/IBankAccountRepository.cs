#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/04，14:11
// 文件名：IBankAccountRepository.cs
// 程序集：UniCloud.Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System;
using System.Linq.Expressions;

namespace UniCloud.Domain.PurchaseBC.Aggregates.BankAccountAgg
{
    /// <summary>
    ///     银行账户仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{BankAccount}" />
    /// </summary>
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        BankAccount GetBankAccount(Expression<Func<BankAccount, bool>> condition);
    }
}