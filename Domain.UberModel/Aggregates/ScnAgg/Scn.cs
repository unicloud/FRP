#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:14:41

// 文件名：Scn
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.Common.Enums;

namespace UniCloud.Domain.UberModel.Aggregates.ScnAgg
{
    /// <summary>
    /// Scn聚合根。
    /// </summary>
    public class Scn : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<ApplicableAircraft> _applicableAircrafts;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Scn()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime CheckDate
        {
            get;
            private set;
        }

        /// <summary>
        /// 批次号
        /// </summary>
        public string CSCNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// MOD号
        /// </summary>
        public string ModNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// TS号
        /// </summary>
        public string TsNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// 费用
        /// </summary>
        public decimal Cost
        {
            get;
            private set;
        }

        /// <summary>
        /// SCN号
        /// </summary>
        public string ScnNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// SCN状态
        /// </summary>
        public ScnStatus ScnStatus
        {
            get;
            private set;
        }

        /// <summary>
        /// SCN适用类型
        /// </summary>
        public ScnApplicableType ScnType
        {
            get;
            private set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;
            private set;
        }

        /// <summary>
        /// Scn文档名称
        /// </summary>
        public string ScnDocName
        {
            get;
            private set;
        }

        /// <summary>
        /// 审核部门
        /// </summary>
        public string AuditOrganization
        {
            get;
            private set;
        }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Auditor
        {
            get;
            private set;
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string AuditNotes
        {
            get;
            private set;
        }

        /// <summary>
        /// 审核历史
        /// </summary>
        public string AuditHistory
        {
            get;
            private set;
        }
        #endregion

        #region 外键属性
        /// <summary>
        /// SCN文件
        /// </summary>
        public Guid ScnDocumentId
        {
            get;
            private set;
        }

        #endregion

        #region 导航属性

        /// <summary>
        /// 适用飞机
        /// </summary>
        public virtual ICollection<ApplicableAircraft> ApplicableAircrafts
        {
            get { return _applicableAircrafts ?? (_applicableAircrafts = new HashSet<ApplicableAircraft>()); }
            set { _applicableAircrafts = new HashSet<ApplicableAircraft>(value); }
        }
        #endregion

        #region 操作
        /// <summary>
        ///     设置确认日期
        /// </summary>
        /// <param name="date">确认日期</param>
        public void SetCheckDate(DateTime date)
        {
            CheckDate = date;
        }

        /// <summary>
        ///     设置批次号
        /// </summary>
        /// <param name="cscNumber">批次号</param>
        public void SetCscNumber(string cscNumber)
        {
            if (string.IsNullOrWhiteSpace(cscNumber))
            {
                throw new ArgumentException("批次号参数为空！");
            }

            CSCNumber = cscNumber;
        }

        /// <summary>
        ///     设置MOD号
        /// </summary>
        /// <param name="modNumber">MOD号</param>
        public void SetModNumber(string modNumber)
        {
            if (string.IsNullOrWhiteSpace(modNumber))
            {
                throw new ArgumentException("MOD号参数为空！");
            }

            ModNumber = modNumber;
        }

        /// <summary>
        ///     设置TS号
        /// </summary>
        /// <param name="tsNumber">TS号</param>
        public void SetTsNumber(string tsNumber)
        {
            if (string.IsNullOrWhiteSpace(tsNumber))
            {
                throw new ArgumentException("TS号参数为空！");
            }

            TsNumber = tsNumber;
        }

        /// <summary>
        /// 设置费用
        /// </summary>
        /// <param name="cost"></param>
        public void SetCost(decimal cost)
        {
            Cost = cost;
        }

        /// <summary>
        ///     设置SCN号
        /// </summary>
        /// <param name="scnNumber">SCN号</param>
        public void SetScnNumber(string scnNumber)
        {
            if (string.IsNullOrWhiteSpace(scnNumber))
            {
                throw new ArgumentException("SCN号参数为空！");
            }

            ScnNumber = scnNumber;
        }

        /// <summary>
        ///     设置SCN状态
        /// </summary>
        /// <param name="scnStatus">SCN状态</param>
        public void SetScnStatus(ScnStatus scnStatus)
        {
            ScnStatus = scnStatus;
        }

        /// <summary>
        ///     设置SCN适用类型
        /// </summary>
        /// <param name="scnType">SCN适用类型</param>
        public void SetScnType(ScnApplicableType scnType)
        {
            switch (scnType)
            {
                case ScnApplicableType.个体:
                    ScnType = ScnApplicableType.个体;
                    break;
                case ScnApplicableType.批量:
                    ScnType = ScnApplicableType.批量;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scnType");
            }
        }

        /// <summary>
        ///     设置描述
        /// </summary>
        /// <param name="description">描述</param>
        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("描述参数为空！");
            }

            Description = description;
        }

        /// <summary>
        ///     设置SCN文件
        /// </summary>
        /// <param name="scnDocName">Scn文件名称</param>
        /// <param name="scnDocumentId">Scn文件ID</param>
        public void SetScnDocument(string scnDocName, Guid scnDocumentId)
        {
            ScnDocName = scnDocName;
            ScnDocumentId = scnDocumentId;
        }

        /// <summary>
        /// 设置审核信息
        /// </summary>
        /// <param name="auditOrganization">审核部门</param>
        /// <param name="auditor">审核人</param>
        /// <param name="auditTime">审核时间</param>
        /// <param name="auditNotes">审核意见</param>
        public void SetAuditMsg(string auditOrganization, string auditor, DateTime? auditTime, string auditNotes)
        {
            AuditOrganization = auditOrganization;
            Auditor = auditor;
            AuditTime = auditTime;
            AuditNotes = auditNotes;
            AuditHistory = auditOrganization + "-" + auditor + "  " + auditTime.ToString() + "\r\n" + auditNotes + "\r\n\r\n" + AuditHistory;
        }

        /// <summary>
        /// 新增适用飞机
        /// </summary>
        /// <returns></returns>
        public ApplicableAircraft AddNewApplicableAircraft()
        {
            var applicableAircraft = new ApplicableAircraft
            {
                ScnId = Id,
            };

            applicableAircraft.GenerateNewIdentity();
            ApplicableAircrafts.Add(applicableAircraft);

            return applicableAircraft;
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
