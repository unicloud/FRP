#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:36:46
// 文件名：PlanHistory
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:36:46
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Application.FleetPlanBC.DTO.DataTransfer;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO.DataTransfer
{
    [KnownType(typeof(OperationPlan))]
    [KnownType(typeof(ChangePlan))]
    [Serializable]
    public abstract class PlanHistory
    {
        public Guid PlanHistoryID { get; set; }
        public Guid? PlanAircraftID { get; set; }
        public Guid PlanID { get; set; }
        public Guid? ApprovalHistoryID { get; set; }
        public Guid ActionCategoryID { get; set; } // 活动类别：包括引进、退出、变更
        public Guid TargetCategoryID { get; set; } // 目标类别：具体的引进、退出方式
        public Guid AircraftTypeID { get; set; } // 计划机型
        public Guid AirlinesID { get; set; }
        [Display(Name = "执行年度")]
        public Guid PerformAnnualID { get; set; } // 执行年度
        [Display(Name = "执行月份")]
        public int PerformMonth { get; set; }
        [Display(Name = "净增座位")]
        public int SeatingCapacity { get; set; }
        [Display(Name = "净增商载(吨)")]
        public decimal CarryingCapacity { get; set; }
        public bool IsValid { get; set; } // 是否有效，确认计划时将计划相关条目置为有效，只有有效的条目才能执行。已有申请、批文的始终有效。
        public bool IsAdjust { get; set; } // 是否调整项
        [StringLength(200)]
        public string Note { get; set; }
        public bool IsSubmit { get; set; } // 是否上报

        public virtual PlanAircraft PlanAircraft { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual ActionCategory ActionCategory { get; set; }
        public virtual ActionCategory TargetCategory { get; set; }
        public virtual AircraftType AircraftType { get; set; }
        public virtual ApprovalHistory ApprovalHistory { get; set; }
        public virtual Annual Annual { get; set; }
        public virtual Airlines Airlines { get; set; }

    }

}
