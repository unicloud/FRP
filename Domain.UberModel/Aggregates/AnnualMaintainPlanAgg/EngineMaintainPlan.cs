#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 10:34:47
// 文件名：EngineExceedMaintainPlan
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 10:34:47
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AnnualMaintainPlanAgg
{
    /// <summary>
    /// 发动机
    /// </summary>
    public class EngineMaintainPlan : AnnualMaintainPlan
    {
        #region 私有字段
        private HashSet<EngineMaintainPlanDetail> _engineMaintainPlanDetails;
        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EngineMaintainPlan()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 0 是 发动机非FHA；1是 发动机超包修
        /// </summary>
        public int MaintainPlanType { get; internal set; }

        /// <summary>
        /// 美元汇率
        /// </summary>
        public decimal DollarRate { get; internal set; }
        /// <summary>
        /// 公司分管领导
        /// </summary>
        public string CompanyLeader { get; internal set; }
        /// <summary>
        /// 部门领导
        /// </summary>
        public string DepartmentLeader { get; internal set; }
        /// <summary>
        /// 预算管理员
        /// </summary>
        public string BudgetManager { get; internal set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PhoneNumber { get; internal set; }
        #endregion

        #region 外键属性

        #endregion

        #region 导航属性
        public virtual ICollection<EngineMaintainPlanDetail> EngineMaintainPlanDetails
        {
            get { return _engineMaintainPlanDetails ?? (_engineMaintainPlanDetails = new HashSet<EngineMaintainPlanDetail>()); }
            set { _engineMaintainPlanDetails = new HashSet<EngineMaintainPlanDetail>(value); }
        }
        #endregion

        #region 操作

        #endregion
    }
}
