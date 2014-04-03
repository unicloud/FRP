#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:23:21

// 文件名：PnReg
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;

namespace UniCloud.Domain.PartBC.Aggregates.PnRegAgg
{
    /// <summary>
    ///     PnReg聚合根。
    /// </summary>
    public class PnReg : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<Dependency> _dependencies;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal PnReg()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     件号
        /// </summary>
        public string Pn { get; private set; }

        /// <summary>
        ///     是否寿控
        /// </summary>
        public bool IsLife { get; private set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     最近一次更新日期
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        /// 项外键
        /// </summary>
        public int? ItemId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     依赖项集合
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
        ///     设置附件项外键
        /// </summary>
        /// <param name="item">附件项</param>
        public void SetItem(Item item)
        {
            if (item!=null)
            {
                ItemId = item.Id;
            }
        }

        /// <summary>
        ///     设置是否寿控
        /// </summary>
        /// <param name="isLife">是否寿控</param>
        public void SetIsLife(bool isLife)
        {
            IsLife = isLife;
        }

        /// <summary>
        ///     新增依赖项
        /// </summary>
        /// <param name="dependencyPnReg">依赖项附件</param>
        /// <returns>依赖项</returns>
        public Dependency AddNewDependency(PnReg dependencyPnReg)
        {
            var dependency = new Dependency
            {
                PnRegId = Id,
            };
            dependency.GenerateNewIdentity();
            dependency.SetPnReg(dependencyPnReg);

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
