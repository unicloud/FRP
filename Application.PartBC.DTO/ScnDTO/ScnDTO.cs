#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ScnDTO
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
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    /// Scn
    /// </summary>
    [DataServiceKey("Id")]
    public class ScnDTO
    {
        #region 私有字段

        private List<ApplicableAircraftDTO> _applicableAircrafts;

        #endregion

        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime CheckDate
        {
            get;
            set;
        }

        /// <summary>
        /// 批次号
        /// </summary>
        public string CSCNumber
        {
            get;
            set;
        }

        /// <summary>
        /// MOD号
        /// </summary>
        public string ModNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Rfc号
        /// </summary>
        public string RfcNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 有效期
        /// </summary>
        public string ValidDate
        {
            get;
            set;
        }

        /// <summary>
        /// 费用
        /// </summary>
        public decimal Cost
        {
            get;
            set;
        }

        /// <summary>
        /// SCN号
        /// </summary>
        public string ScnNumber
        {
            get;
            set;
        }

        /// <summary>
        /// SCN类型
        /// </summary>
        public int Type
        {
            get;
            set;
        }

        /// <summary>
        /// SCN状态
        /// </summary>
        public int ScnStatus
        {
            get;
            set;
        }

        /// <summary>
        /// SCN适用类型
        /// </summary>
        public int ScnType
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// SCN文件名称
        /// </summary>
        public string ScnDocName
        {
            get;
            set;
        }

        /// <summary>
        /// 接收日期
        /// </summary>
        public DateTime ReceiveDate
        {
            get;
            set;
        }

        /// <summary>
        /// 审核部门
        /// </summary>
        private string _auditOrganization;
        public string AuditOrganization
        {
            get
            {
                if (string.IsNullOrEmpty(_auditOrganization))
                {
                    switch (ScnStatus)
                    {
                        case 0: return Domain.Common.Enums.ScnStatus.技术标准室审核.ToString().Replace("审核", string.Empty);
                        case 1: return Domain.Common.Enums.ScnStatus.机身系统室审核.ToString().Replace("审核", string.Empty);
                        case 2: return Domain.Common.Enums.ScnStatus.航材计划室审核.ToString().Replace("审核", string.Empty);
                        case 3: return Domain.Common.Enums.ScnStatus.机务工程部审核.ToString().Replace("审核", string.Empty);
                        default: return string.Empty;
                    }
                }
                return _auditOrganization;
            }
            set { _auditOrganization = value; }
        }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Auditor
        {
            get;
            set;
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditTime
        {
            get { return DateTime.Now; }
            set { }
        }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string AuditNotes
        {
            get;
            set;
        }

        /// <summary>
        /// 审核历史
        /// </summary>
        public string AuditHistory
        {
            get;
            set;
        }
        #endregion

        #region 外键属性
        /// <summary>
        /// SCN文件
        /// </summary>
        public Guid ScnDocumentId
        {
            get;
            set;
        }

        #endregion

        #region 导航属性

        /// <summary>
        ///     SCN\MSCN适用飞机集合
        /// </summary>
        public virtual List<ApplicableAircraftDTO> ApplicableAircrafts
        {
            get { return _applicableAircrafts ?? (_applicableAircrafts = new List<ApplicableAircraftDTO>()); }
            set { _applicableAircrafts = value; }
        }
        #endregion
    }
}
