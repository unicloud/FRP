#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/14 9:06:35
// 文件名：AgreementDetail
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/14 9:06:35
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Application.FleetPlanBC.DTO.DataTransfer;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO.DataTransfer
{
    public class AgreementDetail
    {
        public Guid AgreementDetailID { get; set; }
        public Guid AgreementID { get; set; }
        public Guid PlanAircraftID { get; set; }
        public Guid DeliveryRiskID { get; set; }
        public Guid ImportCategoryID { get; set; } // 协议引进方式
        public int SeatingCapacity { get; set; } // 座位数
        public decimal CarryingCapacity { get; set; } // 商载，单位为吨
        public Guid? PlanDeliverAnnualID { get; set; } // 协议交付年度
        public int PlanDeliverMonth { get; set; } // 协议交付月份

        public virtual Agreement Agreement { get; set; }
        public virtual PlanAircraft PlanAircraft { get; set; }
        public virtual DeliveryRisk DeliveryRisk { get; set; }
        public virtual Annual Annual { get; set; }
        public virtual ActionCategory ImportCategory { get; set; }
    }

}
