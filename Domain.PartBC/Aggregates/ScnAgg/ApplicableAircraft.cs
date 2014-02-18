#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:59:26
// 文件名：ApplicableAircraft
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
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.ScnAgg
{
    /// <summary>
    /// ScnAgg聚合根。
    /// ApplicableAircraft
    /// 适用飞机集合
    /// </summary>
    public class ApplicableAircraft : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ApplicableAircraft()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 完成日期
        /// </summary>
        public DateTime CompleteDate
        {
            get;
            set;
        }

        /// <summary>
        /// 费用
        /// </summary>
        public decimal Cost
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 合同飞机外键
        /// </summary>
        public int ContractAircraftId
        {
            get;
            set;
        }

        /// <summary>
        /// SCN外键
        /// </summary>
        public int ScnId
        {
            get;
            set;
        }
        #endregion

        #region 导航属性

        /// <summary>
        /// 合同飞机
        /// </summary>
        public virtual ContractAircraft ContractAircraft
        {
            get;
            set;
        }

        #endregion

        #region 操作

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
