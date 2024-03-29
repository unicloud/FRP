﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/15，20:38
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Domain.Common.Enums;

#endregion



namespace UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg
{
    /// <summary>
    ///     聚合根
    /// </summary>
    public class EngineMaintainInvoice : MaintainInvoice
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EngineMaintainInvoice()
        {
        }

        #endregion

        #region 属性
        public EngineMaintainInvoiceType Type { get; internal set; }
        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        public void SetType(int type)
        {
            Type = (EngineMaintainInvoiceType) type;
        }
        #endregion
    }
}