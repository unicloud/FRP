#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:38:11
// 文件名：ApprovalHistory
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:38:11
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
using UniCloud.Application.FleetPlanBC.DTO.DataTransfer;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO.DataTransfer
{
    [Serializable]
    public class ApprovalHistory
    {
        public Guid ApprovalHistoryID { get; set; }
        public Guid PlanAircraftID { get; set; }
        public Guid RequestID { get; set; }
        public Guid ImportCategoryID { get; set; } // 申请引进方式
        public Guid AirlinesID { get; set; }
        [Display(Name = "座位数")]
        public int SeatingCapacity { get; set; } // 座位数
        [Display(Name = "商载(吨)")]
        public decimal CarryingCapacity { get; set; } // 商载，单位为吨
        [Display(Name = "交付年度")]
        public Guid RequestDeliverAnnualID { get; set; } // 申请交付年度
        [Display(Name = "交付月份")]
        public int RequestDeliverMonth { get; set; } // 申请交付月份
        [Display(Name = "是否批准")]
        public bool IsApproved { get; set; } // 是否批准
        [StringLength(200)]
        public string Note { get; set; }

        public virtual Request Request { get; set; }
        public virtual ManaApprovalHistory ManaApprovalHistory { get; set; }
        public virtual OperationHistory OperationHistory { get; set; }
        public virtual PlanAircraft PlanAircraft { get; set; }
        public virtual Annual Annual { get; set; }
        public virtual ActionCategory ImportCategory { get; set; }
        public virtual Airlines Airlines { get; set; }
    }

}
