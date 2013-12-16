#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/11，16:51
// 方案：FRP
// 项目：Application.PurchaseBC.DTO
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     币种DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class CurrencyDTO
    {
        /// <summary>
        ///     币种ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     币种名称
        /// </summary>
        public string Name { get; set; }
    }
}