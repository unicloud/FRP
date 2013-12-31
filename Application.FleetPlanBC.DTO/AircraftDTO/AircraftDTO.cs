#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:21:11
// 文件名：AircraftDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 实际飞机
    /// </summary>
    [DataServiceKey("AircraftId")]
    public class AircraftDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid AircraftId { get; set; }

        /// <summary>
        ///     注册号
        /// </summary>
        public string RegNumber { get; set; }

        /// <summary>
        ///     序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        ///     运营状态
        /// </summary>
        public bool IsOperation { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     出厂日期
        /// </summary>
        public DateTime? FactoryDate { get; set; }

        /// <summary>
        ///     引进日期
        /// </summary>
        public DateTime ImportDate { get; set; }

        /// <summary>
        ///     注销日期
        /// </summary>
        public DateTime? ExportDate { get; set; }

        /// <summary>
        ///     座位数
        /// </summary>
        public int SeatingCapacity { get; set; }

        /// <summary>
        ///     商载量（吨）
        /// </summary>
        public decimal CarryingCapacity { get; set; }

        /// <summary>
        /// 所有权人
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        ///     机型
        /// </summary>
        public string AircraftTypeName { get; set; }

        /// <summary>
        ///     运营权人
        /// </summary>
        public string AirlinesName { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public string ImportCategoryName { get; set; }
        /// <summary>
        /// 制造商
        /// </summary>
        public string ManufacturerName { get; set; }
        /// <summary>
        /// 座级
        /// </summary>
        public string Regional { get; set; }
        /// <summary>
        ///     运营权历史
        /// </summary>
        private List<OperationHistoryDTO> _operationHistories;
        public List<OperationHistoryDTO> OperationHistories
        {
            get { return _operationHistories ?? (_operationHistories = new List<OperationHistoryDTO>()); }
            set { _operationHistories = value; }
        }

        /// <summary>
        ///     所有权历史
        /// </summary>
        private List<OwnershipHistoryDTO> _ownershipHistories;
        public List<OwnershipHistoryDTO> OwnershipHistories
        {
            get { return _ownershipHistories ?? (_ownershipHistories = new List<OwnershipHistoryDTO>()); }
            set { _ownershipHistories = value; }
        }

        /// <summary>
        ///     商业数据历史
        /// </summary>
        private List<AircraftBusinessDTO> _aircraftBusinesses;
        public List<AircraftBusinessDTO> AircraftBusinesses
        {
            get { return _aircraftBusinesses ?? (_aircraftBusinesses = new List<AircraftBusinessDTO>()); }
            set { _aircraftBusinesses = value; }
        }
        #endregion

        #region 外键属性

        /// <summary>
        /// 所有权人外键
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeId { get; set; }

        /// <summary>
        ///     运营权人外键
        /// </summary>
        public Guid AirlinesId { get; set; }

        /// <summary>
        ///     引进方式外键
        /// </summary>
        public Guid ImportCategoryId { get; set; }

        #endregion
    }
}
