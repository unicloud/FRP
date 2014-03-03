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
using UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg;

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
        ///     TS号
        /// </summary>
        public string TsNumber { get; private set; }

        /// <summary>
        ///     FI号
        /// </summary>
        public string FiNumber { get; private set; }

        /// <summary>
        ///     项号
        /// </summary>
        public string ItemNo { get; private set; }

        /// <summary>
        ///     上层项号
        /// </summary>
        public string ParentItemNo { get; private set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     技术解决方案ID
        /// </summary>
        public int? TsId { get; private set; }

        /// <summary>
        ///     父项ID
        /// </summary>
        public int? ParentId { get; private set; }

        /// <summary>
        ///   根节点ID
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
        ///     设置项号
        /// </summary>
        /// <param name="itemNo">项号</param>
        public void SetItemNo(string itemNo)
        {
            if (string.IsNullOrWhiteSpace(itemNo))
            {
                throw new ArgumentException("项号参数为空！");
            }

            ItemNo = itemNo;
        }

        /// <summary>
        ///     设置父项项号
        /// </summary>
        /// <param name="parentItemNo">父项项号</param>
        public void SetParentItemNo(string parentItemNo)
        {
            ParentItemNo = parentItemNo;
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
        ///     设置当前飞机
        /// </summary>
        /// <param name="ts">当前飞机</param>
        public void SetTechnicalSolution(TechnicalSolution ts)
        {
            TsId = ts.Id;
            TsNumber = ts.TsNumber;
            FiNumber = ts.FiNumber;
        }

        /// <summary>
        ///     设置父项构型
        /// </summary>
        /// <param name="parentId">父项构型ID</param>
        public void SetParentAcConfigId(int? parentId)
        {
            ParentId = parentId;
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