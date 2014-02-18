#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:14:41

// 文件名：TechnicalSolution
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg
{
    /// <summary>
    /// TechnicalSolution聚合根。
    /// </summary>
    public class TechnicalSolution : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<TsLine> _tsLines;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal TechnicalSolution()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// FI号
        /// </summary>
        public string FiNumber
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
        /// 位置
        /// </summary>
        public string Position
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        /// <summary>
        /// 方案明细
        /// </summary>
        public virtual ICollection<TsLine> TsLines
        {
            get { return _tsLines ?? (_tsLines = new HashSet<TsLine>()); }
            set { _tsLines = new HashSet<TsLine>(value); }

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
