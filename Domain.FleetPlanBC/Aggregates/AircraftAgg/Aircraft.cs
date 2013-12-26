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
        public string AircraftReg { get; private set; }

        /// <summary>
        ///     序列号
        /// </summary>
        public string SerialNumber { get; private set; }

        /// <summary>
        ///     运营状态
        /// </summary>
        public bool IsOperation { get;internal set; }

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
        public Guid? SupplierID { get; private set; }

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeID { get; private set; }

        /// <summary>
        ///     运营权人外键
        /// </summary>
        public Guid AirlinesID { get; private set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public Guid ImportCategoryID { get; private set; }

        #endregion

        #region 导航属性
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

        #endregion
    }
}
