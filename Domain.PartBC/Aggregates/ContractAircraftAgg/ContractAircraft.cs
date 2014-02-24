#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 10:10:40

// 文件名：ContractAircraft
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;

namespace UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg
{
    /// <summary>
    /// ContractAircraft聚合根。
    /// 合同飞机
    /// </summary>
    public class ContractAircraft : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ContractAircraft()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        ///     合同名称
        /// </summary>
        public string ContractName { get; protected set; }

        /// <summary>
        ///     合同编号
        /// </summary>
        public string ContractNumber { get; protected set; }

        /// <summary>
        ///     合同Rank号
        /// </summary>
        public string RankNumber { get; protected set; }

        /// <summary>
        ///     飞机批次号
        /// </summary>
        public string CSCNumber { get; protected set; }

        /// <summary>
        ///     飞机序列号
        /// </summary>
        public string SerialNumber { get; protected set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; protected set; }
        #endregion

        #region 外键属性

        /// <summary>
        ///     基本构型组
        /// </summary>
        public int? BasicConfigGroupId { get; private set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置基本构型组
        /// </summary>
        /// <param name="basicConfigGroupId">基本构型组Id</param>
        public void SetBasicConfigGroup(int? basicConfigGroupId)
        {
            BasicConfigGroupId = basicConfigGroupId;
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
