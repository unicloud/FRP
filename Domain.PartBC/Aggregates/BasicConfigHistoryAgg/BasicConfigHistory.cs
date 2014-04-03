#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 14:02:24
// 文件名：BasicConfigHistory
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
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.BasicConfigHistoryAgg
{
    /// <summary>
    ///     BasicConfigHistory聚合根。
    ///     基本构型历史
    /// </summary>
    public class BasicConfigHistory : EntityInt, IValidatableObject
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal BasicConfigHistory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime? EndDate { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     合同飞机外键
        /// </summary>
        public int ContractAircraftId { get; private set; }

        /// <summary>
        ///     基本构型组外键
        /// </summary>
        public int BasicConfigGroupId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     基本构型组
        /// </summary>
        public BasicConfigGroup BasicConfigGroup { get; set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置开始日期
        /// </summary>
        /// <param name="startDate">开始日期</param>
        public void SetStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }

        /// <summary>
        ///     设置结束日期
        /// </summary>
        /// <param name="endDate">结束日期</param>
        public void SetEndDate(DateTime? endDate)
        {
            EndDate = endDate;
        }

        /// <summary>
        ///     设置合同飞机
        /// </summary>
        /// <param name="contractAircraft">合同飞机</param>
        public void SetContractAircraft(ContractAircraft contractAircraft)
        {
            if (contractAircraft == null || contractAircraft.IsTransient())
            {
                throw new ArgumentException("合同飞机参数为空！");
            }

            ContractAircraftId = contractAircraft.Id;
        }

        /// <summary>
        ///     设置基本构型组
        /// </summary>
        /// <param name="basicConfigGroup">基本构型组</param>
        public void SetBasicConfigGroup(BasicConfigGroup basicConfigGroup)
        {
            if (basicConfigGroup == null || basicConfigGroup.IsTransient())
            {
                throw new ArgumentException("基本构型组参数为空！");
            }

            BasicConfigGroup = basicConfigGroup;
            BasicConfigGroupId = basicConfigGroup.Id;
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