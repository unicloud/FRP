#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/04，15:11
// 文件名：PlanAircraft.cs
// 程序集：UniCloud.Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.PlanAircraftAgg
{
    /// <summary>
    ///     计划飞机聚合根
    /// </summary>
    public class PlanAircraft : EntityGuid
    {
        #region 属性

        /// <summary>
        ///     注册号
        /// </summary>
        public string RegNumber { get; set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}