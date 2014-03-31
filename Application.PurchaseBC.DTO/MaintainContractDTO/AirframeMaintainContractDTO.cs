#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/14 15:21:14
// 文件名：AirframeMaintainContractDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/14 15:21:14
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    /// 机身维修合同
    /// </summary>
    [DataServiceKey("AirframeMaintainContractId")]
    public class AirframeMaintainContractDTO : MaintainContractDTO
    {
        /// <summary>
        /// 机身维修合同主键
        /// </summary>
        public int AirframeMaintainContractId { get; set; }
    }
}
