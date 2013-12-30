﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:03:19
// 文件名：Annual
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg
{
    /// <summary>
    ///     年度聚合根
    /// </summary>
    public class Annual : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Annual()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     年度
        /// </summary>
        public int Year { get; protected set; }

        /// <summary>
        ///     是否打开年度
        /// </summary>
        public bool IsOpen { get; internal set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     五年规划期间ID
        /// </summary>
        public Guid ProgrammingId { get; protected set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 五年规划期间
        /// </summary>
        public virtual Programming Programming { get; set; }

        #endregion

        #region 操作



        #endregion
    }
}
