#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:10:37
// 文件名：ApprovalHistory
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

namespace UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg
{
    /// <summary>
    ///     审批历史（申请明细）
    /// </summary>
    public class ApprovalHistory : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ApprovalHistory()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        ///     是否批准
        /// </summary>
        public bool IsApproved { get; internal set; }

        /// <summary>
        ///     座位数
        /// </summary>
        public int SeatingCapacity { get; private set; }

        /// <summary>
        ///     商载量
        /// </summary>
        public decimal CarryingCapacity { get; private set; }

        /// <summary>
        ///     申请交付月份
        /// </summary>
        public int RequestDeliverMonth { get; private set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; private set; }


        #endregion

        #region 外键属性

        /// <summary>
        ///     申请外键
        /// </summary>
        public Guid RequestID { get; private set; }

        /// <summary>
        ///     计划飞机外键
        /// </summary>
        public Guid PlanAircraftID { get; private set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public Guid ImportCategoryID { get; private set; }

        /// <summary>
        ///     申请交付年度
        /// </summary>
        public Guid RequestDeliverAnnualID { get; private set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesID { get; private set; }

        #endregion

        #region 导航属性



        #endregion

        #region 操作



        #endregion
    }
}
