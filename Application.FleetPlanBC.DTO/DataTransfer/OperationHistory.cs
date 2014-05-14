#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:24:37
// 文件名：OperationHistory
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:24:37
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
    public class OperationHistory
    {
        public OperationHistory()
        {
            this.SubOperationHistories = new HashSet<SubOperationHistory>();
        }
        public Guid OperationHistoryID { get; set; }
        public Guid AirlinesID { get; set; }
        public Guid AircraftID { get; set; }
        public Guid ImportCategoryID { get; set; } // 实际引进方式
        public Guid? ExportCategoryID { get; set; } // 实际退出方式
        [StringLength(10), Display(Name = "机号")]
        //[RegularExpression(@"^[B,C]-\d{4}$", ErrorMessage = "格式有误，正确应类似B-9999")]
        public string RegNumber { get; set; } // 飞机注册号
        [Display(Name = "技术接收日期")]
        public DateTime? TechReceiptDate { get; set; } // 技术接收日期
        [Display(Name = "接收日期")]
        public DateTime? ReceiptDate { get; set; } // 接收日期
        [Display(Name = "运营日期")]
        public DateTime? StartDate { get; set; } // 运营日期
        [Display(Name = "退出停场日期")]
        public DateTime? StopDate { get; set; } // 退出停场日期
        [Display(Name = "技术交付日期")]
        public DateTime? TechDeliveryDate { get; set; } // 技术交付日期
        [Display(Name = "退出日期")]
        public DateTime? EndDate { get; set; } // 退出日期
        [Display(Name = "起租日期")]
        public DateTime? OnHireDate { get; set; } // 起租日期，适用于租赁
        [StringLength(200), Display(Name = "说明")]
        public string Note { get; set; }

        [Display(Name = "处理状态")]
        public int Status { get; set; } // 处理状态：包括草稿、待审核、已审核、已提交。
        [Display(Name = "处理状态")]
        public OpStatus OpStatus
        {
            get { return (OpStatus)Status; }
            set { Status = (int)value; }
        }

        public virtual ApprovalHistory ApprovalHistory { get; set; }
        public virtual Airlines Airlines { get; set; }
        public virtual Aircraft Aircraft { get; set; }
        public virtual ActionCategory ImportCategory { get; set; }
        public virtual ActionCategory ExportCategory { get; set; }
        public virtual ICollection<SubOperationHistory> SubOperationHistories { get; set; }
    }
}
