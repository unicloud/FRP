#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:24:21
// 文件名：OwnershipHistory
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:24:21
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
    public class OwnershipHistory
    {
        public Guid OwnershipHistoryID { get; set; }
        public Guid AircraftID { get; set; }
        [Display(Name = "所有权人")]
        public Guid OwnerID { get; set; }
        [Display(Name = "开始日期")]
        public DateTime? StartDate { get; set; } // 开始日期
        [Display(Name = "结束日期")]
        public DateTime? EndDate { get; set; } // 结束日期

        [Display(Name = "处理状态")]
        public int Status { get; set; } // 处理状态：包括草稿、待审核、已审核、已提交。
        [Display(Name = "处理状态")]
        public OpStatus OpStatus
        {
            get { return (OpStatus)Status; }
            set { Status = (int)value; }
        }

        public virtual Aircraft Aircraft { get; set; }
        public virtual Owner Owner { get; set; }
    }

}
