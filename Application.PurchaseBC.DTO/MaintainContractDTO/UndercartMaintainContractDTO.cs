#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/12 15:35:17
// 文件名：UndercartMaintainContractDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System.Data.Services.Common;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    /// 起落架维修合同
    /// </summary>
    [DataServiceKey("UndercartMaintainContractId")]
    public class UndercartMaintainContractDTO : MaintainContractDTO
    {
        /// <summary>
        /// 起落架维修合同主键
        /// </summary>
        public int UndercartMaintainContractId { get; set; }
    }
}
