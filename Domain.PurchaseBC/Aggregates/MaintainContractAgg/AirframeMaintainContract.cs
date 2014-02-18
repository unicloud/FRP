#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/17 15:17:14
// 文件名：AirframeMaintainContract
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/17 15:17:14
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg
{
    /// <summary>
    ///     维修合同聚合根
    ///     机身维修合同
    /// </summary>
    public class AirframeMaintainContract : MaintainContract
    {
          #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AirframeMaintainContract()
        {
        }

        #endregion
    }
}
