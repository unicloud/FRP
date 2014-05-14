#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:34:39
// 文件名：SubOperationHistory
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:34:39
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
    public class SubOperationHistory
    {
        public Guid SubOperationHistoryID { get; set; }
        public Guid OperationHistoryID { get; set; }
        public Guid SubAirlinesID { get; set; }  //分公司
        [Display(Name = "运营日期")]
        public DateTime? StartDate { get; set; } // 运营日期
        [Display(Name = "退出日期")]
        public DateTime? EndDate { get; set; } // 退出日期

        [Display(Name = "处理状态")]
        public int Status { get; set; } // 处理状态：包括草稿、待审核、已审核。
        [Display(Name = "处理状态")]
        public OpStatus OpStatus
        {
            get { return (OpStatus)Status; }
            set { Status = (int)value; }
        }

        public virtual OperationHistory OperationHistory { get; set; }
        public virtual Airlines Airlines { get; set; }
    }

}
