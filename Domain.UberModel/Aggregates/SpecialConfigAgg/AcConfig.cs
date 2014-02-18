#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:14:41

// 文件名：AcConfig
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniCloud.Domain.UberModel.Aggregates
{
    /// <summary>
    /// AcConfig聚合根。
    /// </summary>
    public class AcConfig : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<AcConfig> _subAcConfigs;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AcConfig()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// TS号
        /// </summary>
        public string TsNumber
        {
            get;
            set;
        }

        /// <summary>
        /// FI号
        /// </summary>
        public string FiNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 项号
        /// </summary>
        public string ItemNo
        {
            get;
            set;
        }

        /// <summary>
        /// 上层项号
        /// </summary>
        public string ParentItemNo
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
        /// 技术解决方案ID
        /// </summary>
        public int? TsId
        {
            get;
            set;
        }

        /// <summary>
        /// 父项ID
        /// </summary>
        public int? ParentId
        {
            get;
            set;
        }
        #endregion

        #region 导航属性

        /// <summary>
        /// 下层项集合
        /// </summary>
        public virtual ICollection<AcConfig> SubAcConfigs
        {
            get { return _subAcConfigs ?? (_subAcConfigs = new HashSet<AcConfig>()); }
            set { _subAcConfigs = new HashSet<AcConfig>(value); }
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
