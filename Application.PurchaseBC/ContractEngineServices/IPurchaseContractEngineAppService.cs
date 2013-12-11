#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/11 17:48:05
// 文件名：IPurchaseContractEngineAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO.ContractEngineDTO;

#endregion

namespace UniCloud.Application.PurchaseBC.ContractEngineServices
{
    /// <summary>
    ///    购买合同发动机服务接口。
    /// </summary>
    public interface IPurchaseContractEngineAppService
    {
        /// <summary>
        ///     获取所有采购合同发动机
        /// </summary>
        /// <returns></returns>
        IQueryable<PurchaseContractEngineDTO> GetPurchaseContractEngines();
    }
}
