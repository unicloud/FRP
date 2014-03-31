#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
// 文件名：Forwarder.cs
// 程序集：UniCloud.Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.Common.ValueObjects;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ForwarderAgg
{
    /// <summary>
    ///     承运人聚合根
    /// </summary>
    public class Forwarder : EntityInt, IValidatableObject
    {
        #region 属性

        /// <summary>
        ///     承运人中文名称
        /// </summary>
        public string CnName { get; set; }

        /// <summary>
        ///     承运人英文名称
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        ///     承运人地址
        /// </summary>
        public Address Address { get; set; }


        /// <summary>
        ///     联系电话。
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        ///     传真。
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        ///     联系人。
        /// </summary>
        public string Attn { get; set; }

        /// <summary>
        ///     邮件。
        /// </summary>
        public string Email { get; set; }

        #endregion

        #region 外键属性

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