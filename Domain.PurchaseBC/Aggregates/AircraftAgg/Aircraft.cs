#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.AircraftAgg
{
    /// <summary>
    ///     飞机聚合根
    /// </summary>
    public class Aircraft : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Aircraft()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     机号
        /// </summary>
        public string AircraftReg { get; protected set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     机型ID
        /// </summary>
        public Guid AircraftTypeId { get; protected set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     机型
        /// </summary>
        public virtual AircraftType AircraftType { get; protected set; }

        #endregion

        #region 操作

        #endregion
    }
}