#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/28 14:14:43
// 文件名：AirStructureDamageDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/28 14:14:43
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    /// AirStructureDamage
    /// </summary>
    [DataServiceKey("Id")]
    public class AirStructureDamageDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 飞机ID
        /// </summary>
        public Guid AircraftId
        {
            get;
            set;
        }

        /// <summary>
        /// 飞机注册号
        /// </summary>
        public string AircraftReg
        {
            get;
            set;
        }

        /// <summary>
        /// 飞机机型
        /// </summary>
        public string AircraftType
        {
            get;
            set;
        }

        /// <summary>
        /// 飞机系列
        /// </summary>
        public string AircraftSeries
        {
            get;
            set;
        }

        /// <summary>
        /// 报告号
        /// </summary>
        public string ReportNo
        {
            get;
            set;
        }

        /// <summary>
        /// 文件类型（结构/腐蚀）报告
        /// </summary>
        public int ReportType
        {
            get;
            set;
        }

        /// <summary>
        /// 报告日期
        /// </summary>
        public DateTime? ReportDate
        {
            get;
            set;
        }

        /// <summary>
        /// 损伤/腐蚀描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalCost
        {
            get;
            set;
        }

        /// <summary>
        /// 是否保留
        /// </summary>
        public bool IsDefer
        {
            get;
            set;
        }

        /// <summary>
        /// 技术评估
        /// </summary>
        public string TecAssess
        {
            get;
            set;
        }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string TreatResult
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get;
            set;
        }

        /// <summary>
        /// 关闭日期
        /// </summary>
        public DateTime? CloseDate
        {
            get;
            set;
        }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source
        {
            get;
            set;
        }

        /// <summary>
        /// 腐蚀级别
        /// </summary>
        public int Level
        {
            get;
            set;
        }

        /// <summary>
        /// 修理期限
        /// </summary>
        public string RepairDeadline
        {
            get;
            set;
        }

        public Guid DocumentId
        { get; set; }

        public string DocumentName
        {
            get;
            set;
        }
        #endregion
    }
}
