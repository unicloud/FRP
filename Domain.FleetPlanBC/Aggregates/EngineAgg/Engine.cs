#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:05:33
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

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg
{
    /// <summary>
    ///     发动机聚合根
    /// </summary>
    public class Engine : EntityGuid
    {
        #region 私有字段

        private HashSet<EngineOwnerShipHistory> _engineOwnerShipHistories;
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
        ///     退出日期
        /// </summary>
        public DateTime? ExportDate { get; private set; }

        /// <summary>
        ///     出厂日期
        /// </summary>
        public DateTime? FactoryDate { get; private set; }

        /// <summary>
        ///     引进日期
        /// </summary>
        public DateTime? ImportDate { get; private set; }

        /// <summary>
        ///     生产序列号
        /// </summary>
        public string SerialNumber { get; private set; }

        /// <summary>
        ///     最大推力
        /// </summary>
        public decimal MAXThrust { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     发动机型号外键
        /// </summary>
        public Guid EngineTypeID { get; private set; }

        /// <summary>
        ///     发动机所有权人
        /// </summary>
        public Guid? SupplierID { get; private set; }

        /// <summary>
        ///  航空公司外键
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
        public virtual ICollection<EngineOwnerShipHistory> OperationHistories
        {
            get { return _engineOwnerShipHistories ?? (_engineOwnerShipHistories = new HashSet<EngineOwnerShipHistory>()); }
            set { _engineOwnerShipHistories = new HashSet<EngineOwnerShipHistory>(value); }
        }

        /// <summary>
        ///     所有权历史
        /// </summary>
        public virtual ICollection<EngineBusinessHistory> OwnershipHistories
        {
            get { return _engineBusinessHistories ?? (_engineBusinessHistories = new HashSet<EngineBusinessHistory>()); }
            set { _engineBusinessHistories = new HashSet<EngineBusinessHistory>(value); }
        }


        #endregion

        #region 操作



        #endregion
    }
}
