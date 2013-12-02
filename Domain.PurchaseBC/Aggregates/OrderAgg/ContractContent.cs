#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/23，21:21
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg
{
    public class ContractContent : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能通过工厂方法去创建新实例
        /// </summary>
        internal ContractContent()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     内容标签
        ///     <remarks>
        ///         用“|”分隔
        ///     </remarks>
        /// </summary>
        public string ContentTags { get; set; }

        /// <summary>
        ///     内容文档
        /// </summary>
        public byte[] ContentDoc { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     订单ID
        /// </summary>
        public int OrderId { get; internal set; }

        #endregion

        #region 导航属性

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