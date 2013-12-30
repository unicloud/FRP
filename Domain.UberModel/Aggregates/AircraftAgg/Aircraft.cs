#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
// 文件名：Aircraft.cs
// 程序集：UniCloud.Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using UniCloud.Domain.UberModel.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.UberModel.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.AirlinesAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AircraftAgg
{
    /// <summary>
    ///     飞机聚合根
    /// </summary>
    public class Aircraft : EntityGuid
    {
        #region 私有字段

        private HashSet<OperationHistory> _operationHistories;
        private HashSet<OwnershipHistory> _ownershipHistories;
        private HashSet<AircraftBusiness> _aircraftBusinesses;
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
        public DateTime ImportDate { get; private set; }

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
        public virtual Supplier Supplier { get; set; }

        /// <summary>
        ///     机型
        /// </summary>
        public virtual AircraftType AircraftType { get; set; }

        /// <summary>
        ///     航空公司
        /// </summary>
        public virtual Airlines Airlines { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public virtual ActionCategory ImportCategory { get; set; }


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
        public void SetImportDate(DateTime date)
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
        /// <param name="supplierId">所有权人</param>
        public void SetSupplier(int supplierId)
        {
            if (supplierId == 0)
            {
                throw new ArgumentException("所有权人Id参数为空！");
            }

            SupplierId = supplierId;
        }

        /// <summary>
        ///     设置机型
        /// </summary>
        /// <param name="aircraftTypeId">机型</param>
        public void SetAircraftType(Guid aircraftTypeId)
        {
            if (aircraftTypeId == null)
            {
                throw new ArgumentException("机型Id参数为空！");
            }

            AircraftTypeId = aircraftTypeId;
        }

        /// <summary>
        ///     设置运营权人
        /// </summary>
        /// <param name="airlinesId">运营权人</param>
        public void SetAirlines(Guid airlinesId)
        {
            if (airlinesId == null)
            {
                throw new ArgumentException("运营权人Id参数为空！");
            }

            AirlinesId = airlinesId;
        }

        /// <summary>
        ///     设置引进方式
        /// </summary>
        /// <param name="importCategoryId">引进方式</param>
        public void SetImportCategory(Guid importCategoryId)
        {
            if (importCategoryId == null)
            {
                throw new ArgumentException("引进方式Id参数为空！");
            }

            ImportCategoryId = importCategoryId;
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
        public OwnershipHistory AddNewOwnershipHistory()
        {
            var ownershipHistory = new OwnershipHistory
            {
                AircraftId = Id,
            };

            ownershipHistory.GenerateNewIdentity();
            OwnershipHistories.Add(ownershipHistory);

            return ownershipHistory;
        }
        #endregion
    }
}