#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 11:01:21
// 文件名：RegularCheckMaintainCost
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 11:01:21
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.MaintainCostAgg
{
    /// <summary>
    /// 定检
    /// </summary>
    public class RegularCheckMaintainCost : MaintainCost
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal RegularCheckMaintainCost()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 类别
        /// </summary>
        public RegularCheckType RegularCheckType { get; internal set; }
        /// <summary>
        /// 定检级别
        /// </summary>
        public string RegularCheckLevel { get; internal set; }
        #endregion

        #region 外键属性
        public Guid AircraftId { get; internal set; }
        public Guid ActionCategoryId { get; internal set; }
        public Guid AircraftTypeId { get; internal set; }
        #endregion

        #region 导航属性

        #endregion

        #region 操作
        #endregion
    }
}
