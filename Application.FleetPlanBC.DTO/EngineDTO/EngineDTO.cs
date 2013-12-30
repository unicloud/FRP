#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:28:36
// 文件名：EngineDTO
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

namespace UniCloud.Application.FleetPlanBC.DTO.EngineDTO
{
    /// <summary>
    /// 发动机
    /// </summary>
    [DataServiceKey("Id")]
    public class EngineDTO
    {
        #region 私有字段

        private List<EngineOwnershipHistoryDTO> _engineOwnerShipHistories;
        private List<EngineBusinessHistoryDTO> _engineBusinessHistories;

        #endregion

        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

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
        public DateTime? ImportDate { get; set; }

        /// <summary>
        ///     注销日期
        /// </summary>
        public DateTime? ExportDate { get; set; }

        /// <summary>
        ///     生产序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        ///     最大推力
        /// </summary>
        public decimal MaxThrust { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     发动机型号外键
        /// </summary>
        public Guid EngineTypeId { get; set; }

        /// <summary>
        ///     发动机所有权人
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        ///  航空公司外键
        /// </summary>
        public Guid AirlinesId { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public Guid ImportCategoryId { get; set; }

        #endregion

        #region 导航属性
        /// <summary>
        ///     所有权历史
        /// </summary>
        public virtual List<EngineOwnershipHistoryDTO> EngineOwnerShipHistories
        {
            get { return _engineOwnerShipHistories ?? (_engineOwnerShipHistories = new List<EngineOwnershipHistoryDTO>()); }
            set { _engineOwnerShipHistories = value; }
        }

        /// <summary>
        ///     商业数据历史
        /// </summary>
        public virtual List<EngineBusinessHistoryDTO> EngineBusinessHistories
        {
            get { return _engineBusinessHistories ?? (_engineBusinessHistories = new List<EngineBusinessHistoryDTO>()); }
            set { _engineBusinessHistories = value; }
        }


        #endregion
    }
}
