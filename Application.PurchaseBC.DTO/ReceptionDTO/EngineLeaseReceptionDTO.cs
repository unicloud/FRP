﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/11 13:44:43
// 文件名：EngineLeaseReceptionDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///  租赁发动机接收项目
    /// </summary>
    [DataServiceKey("EngineLeaseReceptionId")]
    public partial class EngineLeaseReceptionDTO
    {
        public EngineLeaseReceptionDTO()
        {
            ReceptionLines = new List<EngineLeaseReceptionLineDTO>();
            ReceptionSchedules = new List<ReceptionScheduleDTO>();
        }

        #region 属性
        /// <summary>
        /// 租赁发动机接收项目主键
        /// </summary>
        public int EngineLeaseReceptionId { get; set; }

        /// <summary>
        /// 接机编号
        /// </summary>
        public string ReceptionNumber { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 交付起始时间
        /// </summary>
 
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 交付截止时间
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool IsClosed { get; set; }
        /// <summary>
        /// 关闭日期
        /// </summary>
        public DateTime? CloseDate { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 源ID
        /// </summary>
        public Guid SourceId { get; set; }
        #endregion

        #region 外键属性

        /// <summary>
        /// 供应商外键
        /// </summary>
        public int SupplierId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     接收行
        /// </summary>
        public List<EngineLeaseReceptionLineDTO> ReceptionLines { get; set; }

        /// <summary>
        ///     交付日程
        /// </summary>
        public List<ReceptionScheduleDTO> ReceptionSchedules { get; set; }
        #endregion
    }
}
