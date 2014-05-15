#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:37:46
// 文件名：ApprovalDoc
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:37:46
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
    [Serializable]
    public class ApprovalDoc
    {
        public ApprovalDoc()
        {
            this.Requests = new HashSet<Request>();
        }

        public Guid ApprovalDocID { get; set; }
        public Guid DispatchUnitID { get; set; }
        [Display(Name = "审批日期")]
        public DateTime? ExamineDate { get; set; } // 审批日期
        [StringLength(100), Display(Name = "批文文号")]
        public string ApprovalNumber { get; set; } // 批文文号
        [StringLength(100), Display(Name = "批文文件名")]
        public string ApprovalDocFileName { get; set; } // 批文文档文件名
        [Display(Name = "批文文档")]
        public byte[] AttachDoc { get; set; } // 批文文档

        [Display(Name = "处理状态")]
        public int Status { get; set; } // 处理状态：包括草稿、待审核、已审核、已提交。
        [Display(Name = "处理状态")]
        public OpStatus OpStatus
        {
            get { return (OpStatus)Status; }
            set { Status = (int)value; }
        }

        public virtual Manager Manager { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }

}
