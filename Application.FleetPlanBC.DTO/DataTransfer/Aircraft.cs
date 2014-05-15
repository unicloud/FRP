#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:22:22
// 文件名：Aircraft
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:22:22
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
    [Serializable]
    public class Aircraft
    {
        public Aircraft()
        {
            this.OwnershipHistorys = new HashSet<OwnershipHistory>();
            this.OperationHistories = new HashSet<OperationHistory>();
            this.AircraftBusinesses = new HashSet<AircraftBusiness>();
            this.PlanAircrafts = new HashSet<PlanAircraft>();
        }

        public Guid AircraftID { get; set; }
        public Guid AircraftTypeID { get; set; }
        public Guid? OwnerID { get; set; }
        public Guid AirlinesID { get; set; }
        public Guid ImportCategoryID { get; set; } // 引进方式
        [StringLength(10), Display(Name = "机号")]
        public string RegNumber { get; set; } // 飞机注册号
        [StringLength(20), Display(Name = "序列号")]
        [Required(ErrorMessage = "序列号不能为空")]
        public string SerialNumber { get; set; } // 飞机序列号
        [Display(Name = "创建日期")]
        public DateTime? CreateDate { get; set; } // 开始日期
        [Display(Name = "出厂日期")]
        public DateTime? FactoryDate { get; set; } // 出厂日期
        [Display(Name = "引进日期")]
        public DateTime? ImportDate { get; set; } // 引进日期
        [Display(Name = "注销日期")]
        public DateTime? ExportDate { get; set; } // 注销日期
        [Display(Name = "运营状态")]
        public bool IsOperation { get; set; } // 是否在运营

        [Display(Name = "座位数")]
        public int SeatingCapacity { get; set; } // 当前座位数
        [Display(Name = "商载(吨)")]
        public decimal CarryingCapacity { get; set; } // 当前商载，单位吨

        public virtual AircraftType AircraftType { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual Airlines Airlines { get; set; }
        public virtual ActionCategory ImportCategory { get; set; }
        public virtual ICollection<PlanAircraft> PlanAircrafts { get; set; }
        public virtual ICollection<OwnershipHistory> OwnershipHistorys { get; set; }
        public virtual ICollection<OperationHistory> OperationHistories { get; set; }
        public virtual ICollection<AircraftBusiness> AircraftBusinesses { get; set; }
    }
}
