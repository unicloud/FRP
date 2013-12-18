#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：linxw 时间：2013/12/17，15:42
// 方案：FRP
// 项目：Application.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;

#endregion

namespace UniCloud.Application.PaymentBC
{
    /// <summary>
    ///     静态数据加载接口
    /// </summary>
    public interface IStaticLoad
    {
        /// <summary>
        ///     设置刷新货币
        /// </summary>
        void RefreshCurrency();

        /// <summary>
        ///     设置刷新供应商
        /// </summary>
        void RefreshSupplier();

        /// <summary>
        ///     获取币种静态集合
        /// </summary>
        /// <returns>币种静态集合</returns>
        IQueryable<CurrencyDTO> GetCurrencies();

        /// <summary>
        ///     获取供应商静态集合
        /// </summary>
        /// <returns>供应商静态集合</returns>
        IQueryable<SupplierDTO> GetSuppliers();
    }
}