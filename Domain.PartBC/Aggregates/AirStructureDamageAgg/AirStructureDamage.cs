#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/28 11:28:10
// 文件名：AirStructureDamage
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/28 11:28:10
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;

namespace UniCloud.Domain.PartBC.Aggregates.AirStructureDamageAgg
{
    /// <summary>
    /// AirBusScn聚合根。
    /// </summary>
    public class AirStructureDamage : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AirStructureDamage()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 飞机注册号
        /// </summary>
        public string AircraftReg
        {
            get;
            private set;
        }

        /// <summary>
        /// 飞机机型
        /// </summary>
        public string AircraftType
        {
            get;
            private set;
        }

        /// <summary>
        /// 飞机系列
        /// </summary>
        public string AircraftSeries
        {
            get;
            private set;
        }

        /// <summary>
        /// 报告号
        /// </summary>
        public string ReportNo
        {
            get;
            private set;
        }

        /// <summary>
        /// 文件类型（结构/腐蚀）报告
        /// </summary>
        public AirStructureReportType ReportType
        {
            get;
            private set;
        }

        /// <summary>
        /// 报告日期
        /// </summary>
        public DateTime? ReportDate
        {
            get;
            private set;
        }

        /// <summary>
        /// 损伤/腐蚀描述
        /// </summary>
        public string Description
        {
            get;
            private set;
        }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalCost
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否保留
        /// </summary>
        public bool IsDefer
        {
            get;
            private set;
        }

        /// <summary>
        /// 技术评估
        /// </summary>
        public string TecAssess
        {
            get;
            private set;
        }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string TreatResult
        {
            get;
            private set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public AirStructureDamageStatus Status
        {
            get;
            private set;
        }

        /// <summary>
        /// 关闭日期
        /// </summary>
        public DateTime? CloseDate
        {
            get;
            private set;
        }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source
        {
            get;
            private set;
        }

        /// <summary>
        /// 腐蚀级别
        /// </summary>
        public AircraftDamageLevel Level
        {
            get;
            private set;
        }

        /// <summary>
        /// 修理期限
        /// </summary>
        public string RepairDeadline
        {
            get;
            private set;
        }

        public string DocumentName
        {
            get;
            private set;
        }
        #endregion

        #region 外键属性
        /// <summary>
        /// 飞机ID
        /// </summary>
        public Guid AircraftId
        {
            get;
            private set;
        }

        /// <summary>
        /// 文档ID
        /// </summary>
        public Guid DocumentId
        {
            get;
            private set;
        }
        #endregion

        #region 导航属性
        /// <summary>
        /// 飞机
        /// </summary>
        public virtual Aircraft Aircraft
        {
            get;
            set;
        }
        #endregion

        #region 操作

        /// <summary>
        /// 设置飞机相关信息
        /// </summary>
        /// <param name="aircraftId">飞机Id</param>
        /// <param name="aircraftReg">飞机注册号</param>
        /// <param name="aircraftType">机型</param>
        /// <param name="aircraftSeries">系列</param>
        public void SetAircraftInfo(Guid aircraftId, string aircraftReg, string aircraftType, string aircraftSeries)
        {
            AircraftId = aircraftId;
            AircraftReg = aircraftReg;
            AircraftType = aircraftType;
            AircraftSeries = aircraftSeries;
        }

        /// <summary>
        /// 设置报告日期
        /// </summary>
        /// <param name="reportTime">报告日期</param>
        /// <param name="closeTime">关闭日期</param>
        /// <param name="repairDeadline">修理期限</param>
        public void SetReportDate(DateTime? reportTime, DateTime? closeTime, string repairDeadline)
        {
            ReportDate = reportTime;
            CloseDate = closeTime;
            RepairDeadline = repairDeadline;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="status">状态</param>
        public void SetStatus(AirStructureDamageStatus status)
        {
            Status = status;
        }

        /// <summary>
        /// 设置腐蚀级别
        /// </summary>
        /// <param name="level">腐蚀级别</param>
        public void SetLevel(AircraftDamageLevel level)
        {
            Level = level;
        }

        /// <summary>
        /// 设置文档
        /// </summary>
        /// <param name="documentId">文档Id</param>
        /// <param name="documentName">文档名</param>
        public void SetDocument(Guid documentId, string documentName)
        {
            DocumentId = documentId;
            DocumentName = documentName;
        }

        /// <summary>
        /// 设置是否保留
        /// </summary>
        /// <param name="defer">是否保留</param>
        public void SetDefer(bool defer)
        {
            IsDefer = defer;
        }

        /// <summary>
        /// 设置总金额
        /// </summary>
        /// <param name="totalCost">总金额</param>
        public void SetCost(decimal totalCost)
        {
            TotalCost = totalCost;
        }

        /// <summary>
        /// 设置报告
        /// </summary>
        /// <param name="source">来源</param>
        /// <param name="reportNo">报告号</param>
        /// <param name="reportType">报告类型</param>
        /// <param name="description">描述</param>
        public void SetReport(string source, string reportNo, AirStructureReportType reportType, string description)
        {
            Source = source;
            ReportNo = reportNo;
            ReportType = reportType;
            Description = description;
        }

        /// <summary>
        /// 设置结果
        /// </summary>
        /// <param name="tecAssess">技术评估</param>
        /// <param name="treatResult">处理结果</param>
        public void SetResult(string tecAssess, string treatResult)
        {
            TecAssess = tecAssess;
            TreatResult = treatResult;
        }
        #endregion

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}
