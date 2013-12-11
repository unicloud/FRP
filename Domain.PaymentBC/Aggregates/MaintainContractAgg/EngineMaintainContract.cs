#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，11:12
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.MaintainContractAgg
{
    /// <summary>
    ///     维修合同聚合根
    ///     发动机维修合同
    /// </summary>
    public class EngineMaintainContract : MaintainContract
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EngineMaintainContract()
        {
        }

        #endregion
    }
}