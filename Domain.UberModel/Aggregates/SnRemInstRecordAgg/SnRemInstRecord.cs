#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/15 22:33:57
// 文件名：SnRemInstRecord
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.UberModel.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.SnRemInstRecordAgg
{
    /// <summary>
    ///     SnRemInstRecord聚合根。
    ///     序号件拆装记录
    /// </summary>
    public class SnRemInstRecord : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal SnRemInstRecord()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     拆装指令号
        /// </summary>
        public string ActionNo { get; private set; }

        /// <summary>
        ///     拆换开始日期
        /// </summary>
        public DateTime ActionDate { get; private set; }

        /// <summary>
        ///     拆换类型
        /// </summary>
        public ActionType ActionType { get; private set; }

        /// <summary>
        ///     拆装位置
        /// </summary>
        public string Position { get; private set; }

        /// <summary>
        ///     拆装原因
        /// </summary>
        public string Reason { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机ID
        /// </summary>
        public Guid AircraftId { get; private set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置拆装指令号
        /// </summary>
        /// <param name="actionNo">拆装指令号</param>
        public void SetActionNo(string actionNo)
        {
            if (string.IsNullOrWhiteSpace(actionNo))
            {
                throw new ArgumentException("拆装指令号参数为空！");
            }

            ActionNo = actionNo;
        }

        /// <summary>
        ///     设置拆换开始日期
        /// </summary>
        /// <param name="date">拆换开始日期</param>
        public void SetActionDate(DateTime date)
        {
            ActionDate = date;
        }

        /// <summary>
        ///     设置拆换类型
        /// </summary>
        /// <param name="actionType">拆换类型</param>
        public void SetActionType(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.拆下:
                    ActionType = ActionType.拆下;
                    break;
                case ActionType.装上:
                    ActionType = ActionType.装上;
                    break;
                case ActionType.拆换:
                    ActionType = ActionType.拆换;
                    break;
                case ActionType.非拆换:
                    ActionType = ActionType.非拆换;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("actionType");
            }
        }

        /// <summary>
        ///     设置拆装位置
        /// </summary>
        /// <param name="position">拆装位置</param>
        public void SetPosition(string position)
        {
            Position = position;
        }

        /// <summary>
        ///     设置拆装原因
        /// </summary>
        /// <param name="reason">拆装原因</param>
        public void SetReason(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new ArgumentException("拆装原因参数为空！");
            }

            Reason = reason;
        }

        /// <summary>
        ///     设置飞机
        /// </summary>
        /// <param name="aircraft">飞机</param>
        public void SetAircraft(Aircraft aircraft)
        {
            if (aircraft == null || aircraft.IsTransient())
            {
                throw new ArgumentException("飞机参数为空！");
            }

            AircraftId = aircraft.Id;
        }

        #endregion

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}
