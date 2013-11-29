﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ReceptionAgg
{
    /// <summary>
    ///     接收聚合根
    ///     接收行
    /// </summary>
    public abstract class ReceptionLine : EntityInt, IValidatableObject
    {
        #region 属性

        /// <summary>
        ///     接收数量
        /// </summary>
        public int ReceivedAmount { get; set; }

        /// <summary>
        ///     接收数量
        /// </summary>
        public int AcceptedAmount { get; set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     接收ID
        /// </summary>
        public int ReceptionId { get; internal set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置完成
        /// </summary>
        public void SetCompleted()
        {
            // TODO：待完善
            IsCompleted = true;
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