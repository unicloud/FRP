#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 16:32:28
// 文件名：Engine
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using UniCloud.Domain.UberModel.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.UberModel.Aggregates.AirlinesAgg;
using UniCloud.Domain.UberModel.Aggregates.EngineTypeAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.EngineAgg
{
    /// <summary>
    ///     发动机聚合根
    /// </summary>
    public class Engine : EntityGuid
    {
        #region 私有字段

        private HashSet<EngineOwnershipHistory> _engineOwnerShipHistories;
        private HashSet<EngineBusinessHistory> _engineBusinessHistories;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Engine()
        {
        }

        #endregion

        #region 属性

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
        ///     生产序列号
        /// </summary>
        public string SerialNumber { get; private set; }

        /// <summary>
        ///     最大推力
        /// </summary>
        public decimal MaxThrust { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     发动机型号外键
        /// </summary>
        public Guid EngineTypeId { get; private set; }

        /// <summary>
        ///     发动机所有权人
        /// </summary>
        public int? SupplierId { get; private set; }

        /// <summary>
        ///  航空公司外键
        /// </summary>
        public Guid AirlinesId { get; private set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public Guid ImportCategoryId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 发动机型号
        /// </summary>
        public virtual EngineType EngineType { get; set; }

        /// <summary>
        ///     发动机所有权人
        /// </summary>
        public virtual Supplier Supplier { get; set; }

        /// <summary>
        /// 航空公司
        /// </summary>
        public virtual Airlines Airlines { get; set; }

        /// <summary>
        /// 引进方式
        /// </summary>
        public virtual ActionCategory ImportCategory { get; set; }


        /// <summary>
        ///     所有权历史
        /// </summary>
        public virtual ICollection<EngineOwnershipHistory> EngineOwnerShipHistories
        {
            get { return _engineOwnerShipHistories ?? (_engineOwnerShipHistories = new HashSet<EngineOwnershipHistory>()); }
            set { _engineOwnerShipHistories = new HashSet<EngineOwnershipHistory>(value); }
        }

        /// <summary>
        ///     商业数据历史
        /// </summary>
        public virtual ICollection<EngineBusinessHistory> EngineBusinessHistories
        {
            get { return _engineBusinessHistories ?? (_engineBusinessHistories = new HashSet<EngineBusinessHistory>()); }
            set { _engineBusinessHistories = new HashSet<EngineBusinessHistory>(value); }
        }


        #endregion

        #region 操作

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
        ///     设置发动机生产序列号
        /// </summary>
        /// <param name="serialNumber">生产序列号</param>
        public void SetSerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                throw new ArgumentException("生产序列号参数为空！");
            }

            SerialNumber = serialNumber;
        }

        /// <summary>
        ///     设置发动机最大推力
        /// </summary>
        /// <param name="maxThrust">最大推力</param>
        public void SetMaxThrust(decimal maxThrust)
        {
            MaxThrust = maxThrust;
        }

        /// <summary>
        ///     设置发动机型号
        /// </summary>
        /// <param name="engineTypeId">发动机型号</param>
        public void SetEngineType(Guid engineTypeId)
        {
            if (engineTypeId == null)
            {
                throw new ArgumentException("发动机型号Id参数为空！");
            }

            EngineTypeId = engineTypeId;
        }

        /// <summary>
        ///     设置发动机所有权人
        /// </summary>
        /// <param name="supplierId">发动机所有权人</param>
        public void SetSupplier(int? supplierId)
        {
            SupplierId = supplierId;
        }

        /// <summary>
        ///     设置航空公司
        /// </summary>
        /// <param name="airlinesId">航空公司</param>
        public void SetAirlines(Guid airlinesId)
        {
            if (airlinesId == null)
            {
                throw new ArgumentException("航空公司Id参数为空！");
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
        public EngineBusinessHistory AddNewEngineBusinessHistory()
        {
            var engineBusinessHistory = new EngineBusinessHistory
            {
                EngineId = Id,
            };

            engineBusinessHistory.GenerateNewIdentity();
            EngineBusinessHistories.Add(engineBusinessHistory);

            return engineBusinessHistory;
        }

        /// <summary>
        /// 新增发动机所有权历史
        /// </summary>
        /// <returns></returns>
        public EngineOwnershipHistory AddNewEngineOwnershipHistory()
        {
            var engineOwnershipHistory = new EngineOwnershipHistory
            {
                EngineId = Id,
            };

            engineOwnershipHistory.GenerateNewIdentity();
            EngineOwnerShipHistories.Add(engineOwnershipHistory);

            return engineOwnershipHistory;
        }

        #endregion
    }
}
