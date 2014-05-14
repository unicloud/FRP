#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/14 9:07:51
// 文件名：Agreement
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/14 9:07:51
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO.DataTransfer
{
    public class Agreement
    {
        public Agreement()
        {
            this.AgreementDetails = new HashSet<AgreementDetail>();
        }
        public Guid AgreementID { get; set; }
        public Guid SupplierID { get; set; }
        public Guid AirlinesID { get; set; }
        public Guid? PlanAgreeAnnualID { get; set; } // 谈判计划启动年度
        public int PlanAgreeMonth { get; set; } // 谈判计划启动月份
        public Guid? ActualAgreeAnnualID { get; set; } // 谈判实际启动年度
        public int ActualAgreeMonth { get; set; } // 谈判实际启动月份
        public DateTime? SignedDate { get; set; } // 签约日期
        [StringLength(200)]
        public string Note { get; set; } // 备注

        public int Type { get; set; } // 协议类型：包括意向、合同
        public AgreementType AgreementType
        {
            get { return (AgreementType)Type; }
            set { Type = (int)value; }
        }

        public int Phase { get; set; } // 协议阶段：包括计划、谈判、签约
        public AgreementPhase AgreementPhase
        {
            get { return (AgreementPhase)Phase; }
            set { Phase = (int)value; }
        }

        public int Status { get; set; } // 处理状态：包括草稿、待审核、已审核、已提交。
        public OpStatus OpStatus
        {
            get { return (OpStatus)Status; }
            set { Status = (int)value; }
        }

        public virtual Owner Owner { get; set; }
        public virtual Annual PlanAnnual { get; set; }
        public virtual Annual ActualAnnual { get; set; }
        public virtual Airlines Airlines { get; set; }
        public virtual ICollection<AgreementDetail> AgreementDetails { get; set; }
    }

}
