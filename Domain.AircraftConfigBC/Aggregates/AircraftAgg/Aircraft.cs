#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，09:11
// 文件名：Aircraft.cs
// 程序集：UniCloud.Domain.AircraftConfigBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using UniCloud.Domain.AircraftConfigBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftLicenseAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg
{
    /// <summary>
    ///     实际飞机聚合根
    /// </summary>
    public class Aircraft : EntityGuid
    {
        private HashSet<AircraftLicense> _licenses;

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
        public string RegNumber { get; internal set; }

        /// <summary>
        ///     序列号
        /// </summary>
        public string SerialNumber { get; internal set; }

        /// <summary>
        ///     运营状态
        /// </summary>
        public bool IsOperation { get; private set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     出厂日期
        /// </summary>
        public DateTime? FactoryDate { get; private set; }

        /// <summary>
        ///     引进日期
        /// </summary>
        public DateTime? ImportDate { get; private set; }

        /// <summary>
        ///     注销日期
        /// </summary>
        public DateTime? ExportDate { get; private set; }

        /// <summary>
        ///     座位数
        /// </summary>
        public int SeatingCapacity { get; private set; }

        /// <summary>
        ///     商载量（吨）
        /// </summary>
        public decimal CarryingCapacity { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     所有权人外键
        /// </summary>
        public int? SupplierId { get; private set; }

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeId { get; internal set; }

        /// <summary>
        ///     运营权人外键
        /// </summary>
        public Guid AirlinesId { get; internal set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public Guid ImportCategoryId { get; internal set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     所有权人
        /// </summary>
        public virtual Supplier Supplier { get; private set; }

        /// <summary>
        ///     机型
        /// </summary>
        public virtual AircraftType AircraftType { get; private set; }

        /// <summary>
        ///     航空公司
        /// </summary>
        public virtual Airlines Airlines { get; private set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public virtual ActionCategory ImportCategory { get; private set; }

        /// <summary>
        ///     飞机证照
        /// </summary>
        public virtual ICollection<AircraftLicense> Licenses
        {
            get { return _licenses ?? (_licenses = new HashSet<AircraftLicense>()); }
            set { _licenses = new HashSet<AircraftLicense>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置飞机运营状态
        /// </summary>
        public void SetOperation(bool isOperation)
        {
            IsOperation = isOperation;
        }

        /// <summary>
        ///     设置出厂日期
        /// </summary>
        /// <param name="date">出厂日期</param>
        public void SetFactoryDate(DateTime? date)
        {
            FactoryDate = date;
        }

        /// <summary>
        ///     设置引进日期
        /// </summary>
        /// <param name="date">引进日期</param>
        public void SetImportDate(DateTime? date)
        {
            ImportDate = date;
        }

        /// <summary>
        ///     设置注销日期
        /// </summary>
        /// <param name="date">注销日期</param>
        public void SetExportDate(DateTime? date)
        {
            ExportDate = date;
        }

        /// <summary>
        ///     设置座位数
        /// </summary>
        /// <param name="seatingCapacity"></param>
        public void SetSeatingCapacity(int seatingCapacity)
        {
            SeatingCapacity = seatingCapacity;
        }

        /// <summary>
        ///     设置商载量
        /// </summary>
        /// <param name="carryingCapacity"></param>
        public void SetCarryingCapacity(decimal carryingCapacity)
        {
            CarryingCapacity = carryingCapacity;
        }

        /// <summary>
        ///     设置所有权人
        /// </summary>
        /// <param name="supplier">所有权人</param>
        public void SetSupplier(Supplier supplier)
        {
            if (supplier == null || supplier.IsTransient())
            {
                throw new ArgumentException("所有权人参数为空！");
            }

            Supplier = supplier;
            SupplierId = supplier.Id;
        }

        /// <summary>
        ///     设置机型
        /// </summary>
        /// <param name="aircraftType">机型</param>
        public void SetAircraftType(AircraftType aircraftType)
        {
            if (aircraftType == null || aircraftType.IsTransient())
            {
                throw new ArgumentException("机型参数为空！");
            }

            AircraftType = aircraftType;
            AircraftTypeId = aircraftType.Id;
        }

        /// <summary>
        ///     设置机型
        /// </summary>
        /// <param name="aircraftTypeId">机型</param>
        public void SetAircraftType(Guid aircraftTypeId)
        {
            AircraftTypeId = aircraftTypeId;
        }

        /// <summary>
        ///     设置运营权人
        /// </summary>
        /// <param name="airlines">运营权人</param>
        public void SetAirlines(Airlines airlines)
        {
            if (airlines == null || airlines.IsTransient())
            {
                throw new ArgumentException("运营权人参数为空！");
            }

            Airlines = airlines;
            AirlinesId = airlines.Id;
        }

        /// <summary>
        ///     设置引进方式
        /// </summary>
        /// <param name="importCategory">引进方式</param>
        public void SetImportCategory(ActionCategory importCategory)
        {
            if (importCategory == null || importCategory.IsTransient())
            {
                throw new ArgumentException("引进方式参数为空！");
            }

            ImportCategory = importCategory;
            ImportCategoryId = importCategory.Id;
        }

        #endregion
    }
}