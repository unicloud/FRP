#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:26:48
// 文件名：ActionCategory
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:26:48
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO.DataTransfer
{
    public class ActionCategory
    {
        public Guid ActionCategoryID { get; set; }
        [StringLength(6), Display(Name = "活动类型")]
        public string ActionType { get; set; } // 活动类型：包括引进、退出、变更
        [StringLength(16), Display(Name = "活动名称")]
        public string ActionName { get; set; } // 活动名称
        [Display(Name = "需要审批")]
        public bool NeedRequest { get; set; }
    }

}
