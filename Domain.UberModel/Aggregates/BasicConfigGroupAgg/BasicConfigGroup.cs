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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.UberModel.Aggregates.AircraftTypeAgg;

namespace UniCloud.Domain.UberModel.Aggregates.BasicConfigGroupAgg
{
    /// <summary>
    /// BasicConfigGroup聚合根。
    /// 基本构型组
    /// </summary>
    public class BasicConfigGroup : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<BasicConfig> _basicConfigs;

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
        /// 启用日期
        /// </summary>
        public DateTime StartDate
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

        /// <summary>
        /// 基本构型组号
        /// </summary>
        public string GroupNo
        {
            get;
            set;
        }
        #endregion

        #region 外键属性

        /// <summary>
        /// 机型外键
        /// </summary>
        public Guid AircraftTypeId
        {
            get;
            set;
        }
        #endregion

        #region 导航属性

        /// <summary>
        /// 机型
        /// </summary>
        public AircraftType AircraftType { get; set; }

        /// <summary>
        /// 基本构型集合
        /// </summary>
        public virtual ICollection<BasicConfig> BasicConfigs
        {
            get { return _basicConfigs ?? (_basicConfigs = new HashSet<BasicConfig>()); }
            set { _basicConfigs = new HashSet<BasicConfig>(value); }
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
