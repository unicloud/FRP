#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:50:31
// 文件名：PlanHistory
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


#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg
{
    /// <summary>
    ///     飞机计划明细
    /// </summary>
    public class PlanHistory : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal PlanHistory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     座位数
        /// </summary>
        public int SeatingCapacity { get; private set; }

        /// <summary>
        ///     商载量
        /// </summary>
        public decimal CarryingCapacity { get; private set; }

        /// <summary>
        ///     执行月份
        /// </summary>
        public int PerformMonth { get; private set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        ///     是否调整项
        /// </summary>
        public bool IsAdjust { get; private set; }

        /// <summary>
        ///     是否提交
        /// </summary>
        public bool IsSubmit { get; internal set; }

        public string Note { get; private set; }

        #endregion

        #region 外键属性
        /// <summary>
        ///     计划飞机外键
        /// </summary>
        public Guid? PlanAircraftID { get; private set; }

        /// <summary>
        ///     计划外键
        /// </summary>
        public Guid PlanID { get; private set; }

        /// <summary>
        ///     活动类型
        /// </summary>
        public Guid ActionCategoryID { get; private set; }

        public Guid TargetCategoryID { get; private set; }

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeID { get; private set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesID { get; private set; }

        /// <summary>
        ///     执行年度
        /// </summary>
        public Guid PerformAnnualID { get; private set; }


        #endregion

        #region 导航属性



        #endregion

        #region 操作



        #endregion
    }
}
