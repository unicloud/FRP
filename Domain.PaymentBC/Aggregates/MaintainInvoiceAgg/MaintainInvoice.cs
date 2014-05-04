#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/15，14:37
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg
{
    /// <summary>
    ///     维修发票聚合根
    /// </summary>
    public abstract class MaintainInvoice : Invoice
    {
        #region 私有字段

        private HashSet<MaintainInvoiceLine> _lines;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal MaintainInvoice()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     序列号
        /// </summary>
        public string SerialNumber { get; private set; }

        /// <summary>
        ///     文档名称
        /// </summary>
        public string DocumentName { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     文档ID
        /// </summary>
        public Guid DocumentId { get; set; }

        #endregion

        #region 导航属性
        /// <summary>
        ///     维修发票行
        /// </summary>
        public virtual ICollection<MaintainInvoiceLine> InvoiceLines
        {
            get { return _lines ?? (_lines = new HashSet<MaintainInvoiceLine>()); }
            set { _lines = new HashSet<MaintainInvoiceLine>(value); }
        }
        #endregion

        #region 操作

        /// <summary>
        ///     设置序列号
        /// </summary>
        /// <param name="serialNumber">序列号</param>
        public void SetSerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                throw new ArgumentException("序列号参数为空！");
            }

            SerialNumber = serialNumber;
        }

        /// <summary>
        ///     设置发票金额
        /// </summary>
        public void SetInvoiceValue()
        {
            SetInvoiceValue(InvoiceLines.Sum(i => i.Amount * i.UnitPrice));
        }
        #endregion

        #region IValidatableObject 成员


        #endregion
    }
}