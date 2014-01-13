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
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;

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
        public Guid RequestId { get; internal set; }

        /// <summary>
        ///     计划飞机外键
        /// </summary>
        public Guid PlanAircraftId { get; private set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public Guid ImportCategoryId { get; private set; }

        /// <summary>
        ///     申请交付年度
        /// </summary>
        public Guid RequestDeliverAnnualId { get; private set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; private set; }

        #endregion

        #region 导航属性
        /// <summary>
        ///  航空公司
        /// </summary>
        public virtual Airlines Airlines { get; set; }

        /// <summary>
        /// 计划飞机
        /// </summary>
        public virtual PlanAircraft PlanAircraft { get; set; }

        /// <summary>
        ///  引进方式
        /// </summary>
        public virtual ActionCategory ImportCategory { get; set; }

        /// <summary>
        ///  申请交付年度
        /// </summary>
        public virtual Annual RequestDeliverAnnual { get; set; }

        /// <summary>
        /// 运营权历史，共享主键
        /// </summary>
        public virtual OperationHistory OperationHistory { get; set; }
        #endregion

        #region 操作

        /// <summary>
        ///     设置座位数
        /// </summary>
        /// <param name="seatingCapacity">座位数</param>
        public void SetSeatingCapacity(int seatingCapacity)
        {
            SeatingCapacity = seatingCapacity;
        }

        /// <summary>
        ///     设置商载量
        /// </summary>
        /// <param name="carryingCapacity">商载量</param>
        public void SetCarryingCapacity(decimal carryingCapacity)
        {

            CarryingCapacity = carryingCapacity;
        }

        /// <summary>
        /// 设置申请交付时间
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public void SetDeliverDate(Guid year, int month)
        {
            if (year == null)
            {
                throw new ArgumentException("交付年度Id参数为空！");
            }
            RequestDeliverAnnualId = year;
            RequestDeliverMonth = month;
        }

        /// <summary>
        /// 设置申请交付时间
        /// </summary>
        /// <param name="year"></param>
        public void SetDeliverDate(Guid year)
        {
            if (year == null)
            {
                throw new ArgumentException("交付年度Id参数为空！");
            }
            RequestDeliverAnnualId = year;
        }

        /// <summary>
        /// 设置申请交付时间
        /// </summary>
        /// <param name="month"></param>
        public void SetDeliverDate(int month)
        {
            RequestDeliverMonth = month;

        }

        /// <summary>
        ///     设置备注
        /// </summary>
        /// <param name="note">备注</param>
        public void SetNote(string note)
        {
            Note = note;
        }

        /// <summary>
        ///     设置申请
        /// </summary>
        /// <param name="requestId">申请</param>
        public void SetRequest(Guid requestId)
        {
            if (requestId == null)
            {
                throw new ArgumentException("申请Id参数为空！");
            }

            RequestId = requestId;
        }

        /// <summary>
        ///     设置计划飞机
        /// </summary>
        /// <param name="planAircraftId">计划飞机</param>
        public void SetPlanAircraft(Guid planAircraftId)
        {
            if (planAircraftId == null)
            {
                throw new ArgumentException("计划飞机Id参数为空！");
            }

            PlanAircraftId = planAircraftId;
        }

        /// <summary>
        ///     设置引进方式
        /// </summary>
        /// <param name="importCategoryId">引进方式</param>
        public void SetImportCategory(Guid importCategoryId)
        {
            if (importCategoryId == null)
            {
                throw new ArgumentException("引进方式Id参数为空！");
            }

            ImportCategoryId = importCategoryId;
        }

        /// <summary>
        ///     设置航空公司
        /// </summary>
        /// <param name="airlinesId">航空公司</param>
        public void SetAirlines(Guid airlinesId)
        {
            if (airlinesId == null)
            {
                throw new ArgumentException("航空公司Id参数为空！");
            }

            AirlinesId = airlinesId;
        }

        /// <summary>
        /// 设置是否批准
        /// </summary>
        /// <param name="isApproved"></param>
        public void SetIsApproved(bool isApproved)
        {
            IsApproved = isApproved;
        }
        #endregion
    }
}
