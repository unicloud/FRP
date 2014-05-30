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

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates
{
    /// <summary>
    ///     AcConfig聚合根。
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
        ///     创建时间
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     位置信息
        /// </summary>
        public string Position { get; private set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     项(Item)ID
        /// </summary>
        public int ItemId { get; private set; }

        /// <summary>
        ///     父项(AcConfig)ID
        /// </summary>
        public int? ParentId { get; private set; }

        /// <summary>
        ///     根节点(AcConfig)ID
        /// </summary>
        public int RootId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     下层项集合
        /// </summary>
        public virtual ICollection<AcConfig> SubAcConfigs
        {
            get { return _subAcConfigs ?? (_subAcConfigs = new HashSet<AcConfig>()); }
            set { _subAcConfigs = new HashSet<AcConfig>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置附件项
        /// </summary>
        /// <param name="item">附件项</param>
        public void SetItem(Item item)
        {
            if (item == null || item.IsTransient())
            {
                throw new ArgumentException("附件项参数为空！");
            }

            ItemId = item.Id;
        }

        /// <summary>
        ///     设置父项
        /// </summary>
        /// <param name="parentAcConfig">父项</param>
        public void SetParentItem(AcConfig parentAcConfig)
        {
            if (parentAcConfig != null)
            {
                ParentId = parentAcConfig.Id;
                SetRootId(parentAcConfig);
            }
            else
            {
                ParentId = null;
                SetRootId(null);
            }
        }

        /// <summary>
        ///     设置位置信息
        /// </summary>
        /// <param name="position">位置信息</param>
        public void SetPosition(string position)
        {
            if (string.IsNullOrWhiteSpace(position))
            {
                throw new ArgumentException("位置信息参数为空！");
            }
            Position = position;
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
        ///     设置根节点
        /// </summary>
        /// <param name="parentAcConfig">父项构型</param>
        public void SetRootId(AcConfig parentAcConfig)
        {
            RootId = parentAcConfig != null ? parentAcConfig.RootId : Id;
            //如果有父项，则从父项中取根节点ID；如果没父项，则根节点为自身
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