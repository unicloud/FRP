#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/06/21，09:06
// 方案：FRP
// 项目：Domain.PortalBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.PortalBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PortalBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PortalBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Domain.PortalBC.Aggregates.AircraftAgg
{
    /// <summary>
    ///     实际飞机聚合根
    /// </summary>
    public class Aircraft : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Aircraft()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     注册号
        /// </summary>
        public string RegNumber { get; protected set; }

        /// <summary>
        ///     序列号
        /// </summary>
        public string SerialNumber { get; protected set; }

        /// <summary>
        ///     运营状态
        /// </summary>
        public bool IsOperation { get; protected set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; protected set; }

        /// <summary>
        ///     出厂日期
        /// </summary>
        public DateTime? FactoryDate { get; protected set; }

        /// <summary>
        ///     引进日期
        /// </summary>
        public DateTime? ImportDate { get; protected set; }

        /// <summary>
        ///     注销日期
        /// </summary>
        public DateTime? ExportDate { get; protected set; }

        /// <summary>
        ///     座位数
        /// </summary>
        public int SeatingCapacity { get; protected set; }

        /// <summary>
        ///     商载量（吨）
        /// </summary>
        public decimal CarryingCapacity { get; protected set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     所有权人外键
        /// </summary>
        public int? SupplierId { get; protected set; }

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeId { get; protected set; }

        /// <summary>
        ///     运营权人外键
        /// </summary>
        public Guid AirlinesId { get; protected set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public Guid ImportCategoryId { get; protected set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     所有权人
        /// </summary>
        public virtual Supplier Supplier { get; protected set; }

        /// <summary>
        ///     机型
        /// </summary>
        public virtual AircraftType AircraftType { get; protected set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public virtual ActionCategory ImportCategory { get; protected set; }

        #endregion
    }
}