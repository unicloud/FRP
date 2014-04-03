#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/18，10:11
// 文件名：ISupplierAppService.cs
// 程序集：UniCloud.Application.PaymentBC
// 版本：VVersionNumber
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;

#endregion

namespace UniCloud.Application.PaymentBC.SupplierServices
{
    /// <summary>
    ///     表示用于处理供应商相关信息服务接口。
    /// </summary>
    public interface ISupplierAppService
    {
        /// <summary>
        ///     获取所有供应商信息，包括银行账户，联系人。
        /// </summary>
        /// <returns>所有供应商。</returns>
        IQueryable<SupplierDTO> GetSuppliers();
    }
}