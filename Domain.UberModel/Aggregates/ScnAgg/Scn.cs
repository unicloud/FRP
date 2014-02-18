#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:14:41

// 文件名：Scn
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniCloud.Domain.UberModel.Aggregates.ScnAgg
{
    /// <summary>
    /// Scn聚合根。
    /// </summary>
    public class Scn : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<ApplicableAircraft> _applicableAircrafts;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Scn()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime CheckDate
        {
            get;
            set;
        }

        /// <summary>
        /// 批次号
        /// </summary>
        public string CSCNumber
        {
            get;
            set;
        }

        /// <summary>
        /// MOD号
        /// </summary>
        public string ModNumber
        {
            get;
            set;
        }

        /// <summary>
        /// TS号
        /// </summary>
        public string TsNumber
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

        /// <summary>
        /// SCN号
        /// </summary>
        public string ScnNumber
        {
            get;
            set;
        }

        /// <summary>
        /// SCN适用类型
        /// </summary>
        public int ScnType
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        #endregion

        #region 外键属性
        /// <summary>
        /// SCN文件
        /// </summary>
        public Guid ScnDocumentId
        {
            get;
            set;
        }

        #endregion

        #region 导航属性

        /// <summary>
        /// 适用飞机
        /// </summary>
        public virtual ICollection<ApplicableAircraft> ApplicableAircrafts
        {
            get { return _applicableAircrafts ?? (_applicableAircrafts = new HashSet<ApplicableAircraft>()); }
            set { _applicableAircrafts = new HashSet<ApplicableAircraft>(value); }
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
