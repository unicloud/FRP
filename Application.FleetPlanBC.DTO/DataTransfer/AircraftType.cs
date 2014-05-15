#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:25:59
// 文件名：AircraftType
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:25:59
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
    public class AircraftType
    {
        public AircraftType()
        {
            this.Aircrafts = new HashSet<Aircraft>();
        }

        public Guid AircraftTypeID { get; set; }
        public Guid ManufacturerID { get; set; }
        public Guid AircraftCategoryID { get; set; }
        [StringLength(16), Display(Name = "机型")]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual AircraftCategory AircraftCategory { get; set; }
        public virtual ICollection<Aircraft> Aircrafts { get; set; }
    }
}
