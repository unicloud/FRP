#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.FunctionItemAgg
{
    public class FunctionItem : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal FunctionItem()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     父亲节点
        /// </summary>
        public  int? ParentItemId { get; internal set; }

        /// <summary>
        ///     是否是叶子节点
        /// </summary>
        public bool IsLeaf { get; internal set; }

        /// <summary>
        ///     排序
        /// </summary>
        public int Sort { get; internal set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        ///     导航的Url
        /// </summary>
        public string NaviUrl { get; internal set; }

        /// <summary>
        ///     是否启用
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime? CreateDate { get; internal set; }

        /// <summary>
        ///     图片地址
        /// </summary>
        public  string ImageUrl { get; internal set; }

        /// <summary>
        /// 是否是按钮
        /// </summary>
        public  bool IsButton { get; internal set; }


        private ICollection<FunctionItem> _subFunctionItems;
        /// <summary>
        ///     子项集合
        /// </summary>
        public ICollection<FunctionItem> SubFunctionItems
        {
            get { return _subFunctionItems ?? (_subFunctionItems = new HashSet<FunctionItem>()); }
            set { _subFunctionItems = new HashSet<FunctionItem>(value); }
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