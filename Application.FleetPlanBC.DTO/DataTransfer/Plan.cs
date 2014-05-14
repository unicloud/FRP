#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:34:53
// 文件名：Plan
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:34:53
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
    public class Plan
    {
        public Plan()
        {
            this.PlanHistories = new HashSet<PlanHistory>();
        }

        public Guid PlanID { get; set; }
        public Guid AirlinesID { get; set; }
        public Guid AnnualID { get; set; }
        [StringLength(200), Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "版本")]
        public int VersionNumber { get; set; }
        public bool IsCurrentVersion { get; set; } // 是否当前版本
        [Display(Name = "是否有效")]
        public bool IsValid { get; set; } // 计划是否有效，通过审核的计划均为有效。
        [Display(Name = "创建日期")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "提交日期")]
        public DateTime? SubmitDate { get; set; }
        [Display(Name = "是否完成")]
        public bool IsFinished { get; set; } // 计划是否完成评审流程，计划发送后设为完成。
        [Display(Name = "评审标记")]
        public bool? ManageFlagPnr { get; set; } // 客机评审标记，仅民航局评审时可用。缺省Null为待评审；False为需要重报；True为符合。
        [Display(Name = "评审标记")]
        public bool? ManageFlagCargo { get; set; } // 货机评审标记，仅民航局评审时可用。缺省Null为待评审；False为需要重报；True为符合。
        [StringLength(255), Display(Name = "评审备注")]
        public string ManageNote { get; set; }
        [StringLength(100), Display(Name = "计划文号")]
        public string DocNumber { get; set; }
        [StringLength(100), Display(Name = "计划文件名")]
        public string AttachDocFileName { get; set; }
        [Display(Name = "计划文档")]
        public byte[] AttachDoc { get; set; }

        [Display(Name = "计划编辑处理状态")]
        public int Status { get; set; } // 计划编辑处理状态：包括草稿、待审核、已审核、已提交、退回
        [Display(Name = "计划编辑处理状态")]
        public PlanStatus PlanStatus
        {
            get { return (PlanStatus)Status; }
            set { Status = (int)value; }
        }

        [Display(Name = "发布计划处理状态")]
        public int PublishStatus { get; set; } // 计划发布的处理状态：包括草稿、待审核、已审核、已提交
        [Display(Name = "发布计划处理状态")]
        public PlanPublishStatus PlanPublishStatus
        {
            get { return (PlanPublishStatus)PublishStatus; }
            set { PublishStatus = (int)value; }
        }

        public virtual Airlines Airlines { get; set; }
        public virtual Annual Annual { get; set; }
        public virtual ICollection<PlanHistory> PlanHistories { get; set; }
    }
}
