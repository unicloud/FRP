#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/04，16:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.Common.ValueObjects;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg
{
    /// <summary>
    ///     联系人聚合根
    /// </summary>
    public class Linkman : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Linkman()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     是否默认联系人
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        ///     电话
        /// </summary>
        public string TelePhone { get; set; }

        /// <summary>
        ///     手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///     传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        ///     邮件账号
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     公司部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        ///     地址
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        ///     源ID
        /// </summary>
        public Guid SourceId { get; private set; }

        public string CustCode { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置联系人所属源
        /// </summary>
        /// <param name="sourceId">联系人源索引ID</param>
        public void SetSourceId(Guid sourceId)
        {
            if (sourceId == null || sourceId == Guid.Empty)
            {
                throw new ArgumentException("联系人所属源参数为空！");
            }

            SourceId = sourceId;
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