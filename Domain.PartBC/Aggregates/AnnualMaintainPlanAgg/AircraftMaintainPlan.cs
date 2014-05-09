#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/9 9:08:54
// 文件名：AircraftMaintainPlan
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/9 9:08:54
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.AnnualMaintainPlanAgg
{
    public class AircraftMaintainPlan: AnnualMaintainPlan
    {
        #region 私有字段
        private HashSet<AircraftMaintainPlanDetail> _aircraftMaintainPlanDetails;
        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftMaintainPlan()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 上半年计划（总计架次）
        /// </summary>
        public int FirstHalfYear { get; internal set; }

        /// <summary>
        /// 下半年计划（总计架次）
        /// </summary>
        public int SecondHalfYear { get; internal set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; internal set; }
        #endregion

        #region 外键属性

        #endregion

        #region 导航属性
        public virtual ICollection<AircraftMaintainPlanDetail> AircraftMaintainPlanDetails
        {
            get { return _aircraftMaintainPlanDetails ?? (_aircraftMaintainPlanDetails = new HashSet<AircraftMaintainPlanDetail>()); }
            set { _aircraftMaintainPlanDetails = new HashSet<AircraftMaintainPlanDetail>(value); }
        }
        #endregion

        #region 操作

        #endregion
    }
}
