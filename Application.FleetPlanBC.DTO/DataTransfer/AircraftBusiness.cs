#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:25:27
// 文件名：AircraftBusiness
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:25:27
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
    public class AircraftBusiness
    {
        public Guid AircraftBusinessID { get; set; }
        public Guid AircraftID { get; set; }
        public Guid AircraftTypeID { get; set; }
        public Guid ImportCategoryID { get; set; } // 引进方式
        [Display(Name = "座位数")]
        public int SeatingCapacity { get; set; } // 座位数
        [Display(Name = "商载(吨)")]
        public decimal CarryingCapacity { get; set; } // 商载，单位吨
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
        public virtual AircraftType AircraftType { get; set; }
        public virtual ActionCategory ImportCategory { get; set; }
    }

}
