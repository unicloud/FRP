#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:35:06
// 文件名：Request
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:35:06
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
    public class Request
    {
        public Request()
        {
            this.ApprovalHistories = new HashSet<ApprovalHistory>();
        }

        public Guid RequestID { get; set; }
        public Guid AirlinesID { get; set; }
        public Guid? ApprovalDocID { get; set; }
        [StringLength(100), Display(Name = "标题")]
        public string Title { get; set; } // 申请的标题
        [Display(Name = "创建日期")]
        public DateTime? CreateDate { get; set; } // 创建日期
        [Display(Name = "提交日期")]
        public DateTime? SubmitDate { get; set; } // 提交日期
        [Display(Name = "是否完成")]
        public bool IsFinished { get; set; } // 是否完成，申请中批准的项都完成后申请完成
        [Display(Name = "评审标记")]
        public bool? ManageFlag { get; set; } // 仅民航局评审时可用。缺省Null为待评审；False为需要重报；True为符合。
        [StringLength(100), Display(Name = "申请文号")]
        public string DocNumber { get; set; } // 航空公司申请的文号
        [StringLength(100), Display(Name = "申请文件名")]
        public string AttachDocFileName { get; set; } // 申请文档文件名
        [Display(Name = "申请文档")]
        public byte[] AttachDoc { get; set; } // 申请文档

        [Display(Name = "处理状态")]
        public int Status { get; set; } // 申请状态：包括草稿、待审核、已审核、已提交、已审批。
        [Display(Name = "处理状态")]
        public ReqStatus ReqStatus
        {
            get { return (ReqStatus)Status; }
            set { Status = (int)value; }
        }

        public virtual Airlines Airlines { get; set; }
        public virtual ApprovalDoc ApprovalDoc { get; set; }
        public virtual ICollection<ApprovalHistory> ApprovalHistories { get; set; }
    }

}
