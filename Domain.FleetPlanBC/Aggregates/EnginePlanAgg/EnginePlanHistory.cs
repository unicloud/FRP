#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:44:54
// 文件名：EnginePlanHistory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.FleetPlanBC.Enums;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg
{
    /// <summary>
    ///     发动机计划明细
    /// </summary>
    public class EnginePlanHistory : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EnginePlanHistory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     执行月份
        /// </summary>
        public int PerformMonth { get; private set; }

        /// <summary>
        ///     执行情况
        /// </summary>
        public PlanStatus Status { get; private set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsFinished { get; internal set; }
        /// <summary>
        ///     最大推力
        /// </summary>
        public decimal MAXThrust { get; private set; }
        
        #endregion

        #region 外键属性

        /// <summary>
        ///     发动机型号
        /// </summary>
        public Guid EngineTypeID { get; private set; }

        /// <summary>
        ///     发动机计划外键
        /// </summary>
        public Guid EnginePlanID { get; private set; }

        /// <summary>
        ///     执行年度
        /// </summary>
        public Guid PerformAnnualID { get; private set; }

        /// <summary>
        ///     计划发动机ID
        /// </summary>
        public Guid? PlanEngineID { get; private set; }

        /// <summary>
        ///     活动类型
        /// </summary>
        public Guid ActionCategoryID { get; private set; }

        #endregion

        #region 导航属性


        #endregion

        #region 操作



        #endregion
    }
}
