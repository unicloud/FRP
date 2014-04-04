#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 17:19:52
// 文件名：Item
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

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ItemAgg
{
    /// <summary>
    ///     Item聚合根。
    ///     附件项
    /// </summary>
    public class Item : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Item()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     项号
        /// </summary>
        public string ItemNo { get; private set; }

        /// <summary>
        ///     功能标识号
        /// </summary>
        public string FiNumber { get; private set; }

        /// <summary>
        ///     是否寿控件
        /// </summary>
        public bool IsLife { get; private set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; private set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置名称
        /// </summary>
        /// <param name="name">名称</param>
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("名称参数为空！");
            }

            Name = name;
        }

        /// <summary>
        ///     设置项号或功能标识号
        /// </summary>
        /// <param name="itemNo">项号</param>
        /// <param name="fiNumber">功能标识号</param>
        public void SetItemNoOrFiNumber(string itemNo, string fiNumber)
        {
            if (string.IsNullOrWhiteSpace(itemNo) && string.IsNullOrWhiteSpace(fiNumber))
            {
                throw new ArgumentException("项号或功能标识号参数为空！");
            }

            ItemNo = itemNo;
            FiNumber = fiNumber;
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
        ///     设置描述
        /// </summary>
        /// <param name="description">描述</param>
        public void SetDescription(string description)
        {
            Description = description;
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
