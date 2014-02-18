#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:23:21

// 文件名：TsLine
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
    /// TsLine
    /// </summary>
    public class TsLine : EntityInt, IValidatableObject
    {

        #region 私有字段

        private HashSet<Dependency> _dependencies;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal TsLine()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 件号
        /// </summary>
        public string Pn
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
        /// TS号
        /// </summary>
        public string TsNumber
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 技术解决方案ID
        /// </summary>
        public int TsId
        {
            get;
            set;
        }
        #endregion

        #region 导航属性

        /// <summary>
        /// 依赖项
        /// </summary>
        public virtual ICollection<Dependency> Dependencies
        {
            get { return _dependencies ?? (_dependencies = new HashSet<Dependency>()); }
            set { _dependencies = new HashSet<Dependency>(value); }
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
