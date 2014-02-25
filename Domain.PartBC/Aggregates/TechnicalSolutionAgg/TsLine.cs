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

using System;
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
            private set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;
            private set;
        }

        /// <summary>
        /// TS号
        /// </summary>
        public string TsNumber
        {
            get;
            private set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 技术解决方案ID
        /// </summary>
        public int TsId
        {
            get;
            internal set;
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
        /// <summary>
        ///     设置件号
        /// </summary>
        /// <param name="pn">件号</param>
        public void SetPn(string pn)
        {
            if (string.IsNullOrWhiteSpace(pn))
            {
                throw new ArgumentException("件号参数为空！");
            }

            Pn = pn;
        }

        /// <summary>
        ///     设置描述
        /// </summary>
        /// <param name="description">描述</param>
        public void SetDescription(string description)
        {
            Description = description;
        }

        /// <summary>
        ///     设置TS号
        /// </summary>
        /// <param name="tsNumber">TS号</param>
        public void SetTsNumber(string tsNumber)
        {
            if (string.IsNullOrWhiteSpace(tsNumber))
            {
                throw new ArgumentException("TS号参数为空！");
            }

            TsNumber = tsNumber;
        }

        /// <summary>
        /// 新增依赖项
        /// </summary>
        /// <returns></returns>
        public Dependency AddNewDependency()
        {
            var dependency = new Dependency
            {
                TsLineId = Id,
            };

            dependency.GenerateNewIdentity();
            Dependencies.Add(dependency);

            return dependency;
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
