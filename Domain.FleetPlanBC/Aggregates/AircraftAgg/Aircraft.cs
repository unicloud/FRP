#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 9:33:37
// 文件名：Aircraft
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftConfigurationAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg
{
    /// <summary>
    ///     实际飞机聚合根
    /// </summary>
    public class Aircraft : EntityGuid
    {
        #region 私有字段

        private HashSet<OperationHistory> _operationHistories;
        private HashSet<OwnershipHistory> _ownershipHistories;
        private HashSet<AircraftBusiness> _aircraftBusinesses;
        private HashSet<AcConfigHistory> _acConfigHistories;
        #endregion

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
        public string RegNumber { get; private set; }

        /// <summary>
        ///     序列号
        /// </summary>
        public string SerialNumber { get; private set; }

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
        /// 所有权人外键
        /// </summary>
        public int? SupplierId { get; private set; }

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeId { get; private set; }

        /// <summary>
        ///     运营权人外键
        /// </summary>
        public Guid AirlinesId { get; private set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public Guid ImportCategoryId { get; private set; }

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
        ///     运营权历史
        /// </summary>
        public virtual ICollection<OperationHistory> OperationHistories
        {
            get { return _operationHistories ?? (_operationHistories = new HashSet<OperationHistory>()); }
            set { _operationHistories = new HashSet<OperationHistory>(value); }
        }
        /// <summary>
        ///     所有权历史
        /// </summary>
        public virtual ICollection<OwnershipHistory> OwnershipHistories
        {
            get { return _ownershipHistories ?? (_ownershipHistories = new HashSet<OwnershipHistory>()); }
            set { _ownershipHistories = new HashSet<OwnershipHistory>(value); }
        }
        /// <summary>
        ///     商业数据历史
        /// </summary>
        public virtual ICollection<AircraftBusiness> AircraftBusinesses
        {
            get { return _aircraftBusinesses ?? (_aircraftBusinesses = new HashSet<AircraftBusiness>()); }
            set { _aircraftBusinesses = new HashSet<AircraftBusiness>(value); }
        }

        /// <summary>
        ///     飞机配置历史
        /// </summary>
        public virtual ICollection<AcConfigHistory> AcConfigHistories
        {
            get { return _acConfigHistories ?? (_acConfigHistories = new HashSet<AcConfigHistory>()); }
            set { _acConfigHistories = new HashSet<AcConfigHistory>(value); }
        }
        #endregion

        #region 操作
        /// <summary>
        ///     设置飞机机号
        /// </summary>
        /// <param name="regNumber">注册号</param>
        public void SetRegNumber(string regNumber)
        {
            if (string.IsNullOrWhiteSpace(regNumber))
            {
                throw new ArgumentException("注册号参数为空！");
            }

            RegNumber = regNumber;
        }

        /// <summary>
        ///     设置飞机序列号
        /// </summary>
        /// <param name="serialNumber">注册号</param>
        public void SetSerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                throw new ArgumentException("序列号参数为空！");
            }

            SerialNumber = serialNumber;
        }

        /// <summary>
        /// 设置飞机运营状态
        /// </summary>
        public void SetOperation()
        {
            // TODO：待完善
            IsOperation = true;
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
        /// 设置座位数
        /// </summary>
        /// <param name="seatingCapacity"></param>
        public void SetSeatingCapacity(int seatingCapacity)
        {
            SeatingCapacity = seatingCapacity;
        }

        /// <summary>
        /// 设置商载量
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

        /// <summary>
        /// 新增飞机商业数据历史
        /// </summary>
        /// <returns></returns>
        public AircraftBusiness AddNewAircraftBusiness()
        {
            var aircraftBusiness = new AircraftBusiness
            {
                AircraftId = Id,
            };

            aircraftBusiness.GenerateNewIdentity();
            AircraftBusinesses.Add(aircraftBusiness);

            return aircraftBusiness;
        }

        /// <summary>
        /// 新增飞运营权历史
        /// </summary>
        /// <returns></returns>
        public OperationHistory AddNewOperationHistory()
        {
            var operationHistory = new OperationHistory
            {
                AircraftId = Id,
            };

            operationHistory.GenerateNewIdentity();
            OperationHistories.Add(operationHistory);

            return operationHistory;
        }


        /// <summary>
        /// 新增所有权历史
        /// </summary>
        /// <returns></returns>
        public OwnershipHistory AddNewOwnershipHistory(int supplierId, DateTime starDate, DateTime? endatDate, OperationStatus status)
        {
            var ownershipHistory = new OwnershipHistory
            {
                AircraftId = Id,

            };
            ownershipHistory.SetSupplier(supplierId);
            ownershipHistory.SetStartDate(starDate);
            ownershipHistory.SetEndDate(endatDate);
            ownershipHistory.GenerateNewIdentity();
            OwnershipHistories.Add(ownershipHistory);
            ownershipHistory.SetOperationStatus(status);
            return ownershipHistory;
        }

        /// <summary>
        /// 新增飞机配置历史
        /// </summary>
        /// <returns></returns>
        public AcConfigHistory AddNewAcConfigHistory(AircraftConfiguration aircraftConfiguration, DateTime starDate, DateTime? endatDate)
        {
            var acConfigHistory = new AcConfigHistory
            {
                AircraftId = Id,
            };
            acConfigHistory.SetAircraftConfiguration(aircraftConfiguration);
            acConfigHistory.SetStartDate(starDate);
            acConfigHistory.SetEndDate(endatDate);
            acConfigHistory.GenerateNewIdentity();

            AcConfigHistories.Add(acConfigHistory);
            return acConfigHistory;
        }
        #endregion
    }
}
