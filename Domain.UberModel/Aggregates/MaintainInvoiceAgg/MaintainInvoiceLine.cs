#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/15，21:12
// 方案：FRP
// 项目：Domain.UberModel
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
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.UberModel.Aggregates.PartAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.MaintainInvoiceAgg
{
    /// <summary>
    ///     维修发票聚合根
    ///     维修发票行
    /// </summary>
    public class MaintainInvoiceLine : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal MaintainInvoiceLine()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     维修项
        /// </summary>
        public MaintainItem MaintainItem { get; private set; }

        /// <summary>
        ///     项名称
        /// </summary>
        public string ItemName { get; internal set; }

        /// <summary>
        ///     单价
        /// </summary>
        public decimal UnitPrice { get; internal set; }

        /// <summary>
        ///     数量
        /// </summary>
        public decimal Amount { get; internal set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     维修发票ID
        /// </summary>
        public int MaintainInvoiceId { get; internal set; }

        /// <summary>
        ///     附件ID
        /// </summary>
        public int? PartID { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     附件
        /// </summary>
        public virtual Part Part { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置维修项
        /// </summary>
        /// <param name="item">维修项</param>
        public void SetMaintainItem(MaintainItem item)
        {
            switch (item)
            {
                case MaintainItem.更换附件:
                    MaintainItem = MaintainItem.更换附件;
                    break;
                case MaintainItem.耗材费:
                    MaintainItem = MaintainItem.耗材费;
                    break;
                case MaintainItem.工时费:
                    MaintainItem = MaintainItem.工时费;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("item");
            }
        }

        /// <summary>
        ///     设置附件
        /// </summary>
        /// <param name="part">附件</param>
        public void SetPart(Part part)
        {
            if (part == null || part.IsTransient())
            {
                throw new ArgumentException("附件参数为空！");
            }

            Part = part;
            PartID = part.Id;
        }

        /// <summary>
        ///     设置附件
        /// </summary>
        /// <param name="partId">附件ID</param>
        public void SetPart(int partId)
        {
            if (partId == 0)
            {
                throw new ArgumentException("附件ID参数为空！");
            }

            PartID = partId;
        }

        /// <summary>
        ///     设置维修发票行说明
        /// </summary>
        /// <param name="note">说明</param>
        public void SetNote(string note)
        {
            Note = note;
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