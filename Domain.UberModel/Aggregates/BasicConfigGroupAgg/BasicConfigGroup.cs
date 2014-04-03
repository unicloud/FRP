#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:32:33

// 文件名：BasicConfigGroup
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
using UniCloud.Domain.UberModel.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.BasicConfigAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.BasicConfigGroupAgg
{
    /// <summary>
    ///     BasicConfigGroup聚合根。
    ///     基本构型组
    /// </summary>
    public class BasicConfigGroup : EntityInt, IValidatableObject
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal BasicConfigGroup()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        ///     基本构型组号
        /// </summary>
        public string GroupNo { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     川航机型外键
        /// </summary>
        public Guid AircraftTypeId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     机型
        /// </summary>
        public AircraftType AircraftType { get; set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置描述
        /// </summary>
        /// <param name="description">描述</param>
        public void SetDescription(string description)
        {
            Description = description;
        }

        /// <summary>
        ///     设置基本构型组号
        /// </summary>
        /// <param name="groupNo">基本构型组号</param>
        public void SetGroupNo(string groupNo)
        {
            if (string.IsNullOrWhiteSpace(groupNo))
            {
                throw new ArgumentException("基本构型组号参数为空！");
            }

            GroupNo = groupNo;
        }

        /// <summary>
        ///     设置机型
        /// </summary>
        /// <param name="aircraftType">机型</param>
        public void SetAircraftType(AircraftType aircraftType)
        {
            if (aircraftType == null || aircraftType.IsTransient())
            {
                throw new ArgumentException("机型参数为空！");
            }

            AircraftType = aircraftType;
            AircraftTypeId = aircraftType.Id;
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