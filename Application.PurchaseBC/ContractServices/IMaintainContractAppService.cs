#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/15 17:31:53
// 文件名：IMaintainContractAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;

namespace UniCloud.Application.PurchaseBC.ContractServices
{
    /// <summary>
    ///     表示用于维修合同相关信息服务
    /// </summary>
    public interface IMaintainContractAppService
    {
        /// <summary>
        ///     获取所有发动机维修合同
        /// </summary>
        /// <returns>所有发动机维修合同</returns>
        IQueryable<EngineMaintainContractDTO> GetEngineMaintainContracts();

        /// <summary>
        ///     获取所有APU维修合同
        /// </summary>
        /// <returns>所有APU维修合同</returns>
        IQueryable<APUMaintainContractDTO> GetApuMaintainContracts();

        /// <summary>
        /// 获取所有起落架维修合同
        /// </summary>
        /// <returns>所有起落架维修合同</returns>
        IQueryable<UndercartMaintainContractDTO> GetUndercartMaintainContracts();
    }
}
