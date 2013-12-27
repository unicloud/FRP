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
        ///     是否提交
        /// </summary>
        public bool IsSubmit { get; internal set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; private set; }

        #endregion

        #region 外键属性
        /// <summary>
        ///     计划飞机外键
        /// </summary>
        public Guid? PlanAircraftId { get; private set; }

        /// <summary>
        ///     计划外键
        /// </summary>
        public Guid PlanId { get; internal set; }

        /// <summary>
        ///     活动类别：包括引进、退出、变更
        /// </summary>
        public Guid ActionCategoryId { get; private set; }

        /// <summary>
        /// 目标类别：具体的引进、退出方式
        /// </summary>
        public Guid TargetCategoryId { get; private set; }

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeId { get; private set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; private set; }

        /// <summary>
        ///     执行年度
        /// </summary>
        public Guid PerformAnnualId { get; private set; }


        #endregion

        #region 导航属性



        #endregion

        #region 操作

        /// <summary>
        /// 设置座位数
        /// </summary>
        /// <param name="seatingCapacity"></param>
        public void SetSeatingCapacity(int seatingCapacity)
        {
            SeatingCapacity = seatingCapacity;
        }

        /// <summary>
        /// 设置商载量
        /// </summary>
        /// <param name="carryingCapacity"></param>
        public void SetCarryingCapacity(decimal carryingCapacity)
        {
            CarryingCapacity = carryingCapacity;
        }

        /// <summary>
        /// 设置执行时间
        /// </summary>
        /// <param name="annualId"></param>
        /// <param name="performMonth"></param>
        public void SetPerformDate(Guid annualId, int performMonth)
        {
            if (annualId == null)
            {
                throw new ArgumentException("执行年度Id参数为空！");
            }
            PerformAnnualId = annualId;
            PerformMonth = performMonth;
        }

        /// <summary>
        ///     设置备注
        /// </summary>
        /// <param name="note">备注</param>
        public void SetNote(string note)
        {
            //if (string.IsNullOrWhiteSpace(note))
            //{
            //    throw new ArgumentException("备注参数为空！");
            //}

            Note = note;
        }

        /// <summary>
        ///     设置计划飞机
        /// </summary>
        /// <param name="planAircraftId">计划飞机</param>
        public void SetPlanAircraft(Guid? planAircraftId)
        {

            PlanAircraftId = planAircraftId;
        }

        /// <summary>
        ///     设置活动类型
        /// </summary>
        /// <param name="actionCategoryId">活动类型</param>
        /// <param name="targetCategoryId">目标类别</param>
        public void SetActionCategory(Guid actionCategoryId, Guid targetCategoryId)
        {
            if (actionCategoryId == null)
            {
                throw new ArgumentException("活动类型Id参数为空！");
            }
            if (targetCategoryId == null)
            {
                throw new ArgumentException("目标类别Id参数为空！");
            }
            ActionCategoryId = actionCategoryId;
            TargetCategoryId = targetCategoryId;
        }

        /// <summary>
        ///     设置机型
        /// </summary>
        /// <param name="aircraftTypeId">机型</param>
        public void SetAircraftType(Guid aircraftTypeId)
        {
            if (aircraftTypeId == null)
            {
                throw new ArgumentException("机型Id参数为空！");
            }

            AircraftTypeId = aircraftTypeId;
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
        #endregion
    }
}
