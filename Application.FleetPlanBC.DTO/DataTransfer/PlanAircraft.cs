#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:25:44
// 文件名：PlanAircraft
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:25:44
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
    public class PlanAircraft
    {
        public PlanAircraft()
        {
            this.PlanHistories = new HashSet<PlanHistory>();
            this.ApprovalHistories = new HashSet<ApprovalHistory>();
            this.AgreementDetails = new HashSet<AgreementDetail>();
        }

        public Guid PlanAircraftID { get; set; }
        public Guid? AircraftID { get; set; }
        public Guid AircraftTypeID { get; set; }
        public Guid AirlinesID { get; set; }
        public bool IsLock { get; set; } // 是否锁定，确定计划时锁定相关飞机。一旦锁定，对应的引进计划明细不能修改机型。
        public bool IsOwn { get; set; } // 是否自有，用以区分PlanAircraft，民航局均为False。

        [Display(Name = "管理状态")]
        public int Status { get; set; } // 管理状态：包括预备、计划、申请、批文、签约、运营、停场待退、退役，申请未批准的回到预备阶段
        [Display(Name = "管理状态")]

        public virtual Aircraft Aircraft { get; set; }
        public virtual Airlines Airlines { get; set; }
        public virtual AircraftType AircraftType { get; set; }
        public virtual ICollection<PlanHistory> PlanHistories { get; set; }
        public virtual ICollection<ApprovalHistory> ApprovalHistories { get; set; }
        public virtual ICollection<AgreementDetail> AgreementDetails { get; set; }
    }

}
