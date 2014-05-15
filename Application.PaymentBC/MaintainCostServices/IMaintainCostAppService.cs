#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 13:52:05
// 文件名：IMaintainCostAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 13:52:05
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;

#endregion

namespace UniCloud.Application.PaymentBC.MaintainCostServices
{
    public interface IMaintainCostAppService
    {
        /// <summary>
        ///     获取所有定检维修成本
        /// </summary>
        /// <returns>所有定检维修成本</returns>
        IQueryable<RegularCheckMaintainCostDTO> GetRegularCheckMaintainCosts();

        /// <summary>
        ///     获取所有起落架维修成本
        /// </summary>
        /// <returns>所有起落架维修成本</returns>
        IQueryable<UndercartMaintainCostDTO> GetUndercartMaintainCosts();
    }
}
