#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:28:03
// 文件名：AircraftCategory
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:28:03
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
    /// <summary>
    /// 飞机类别
    /// </summary>
    [Serializable]
    public class AircraftCategory
    {
        public AircraftCategory()
        {
            this.AircraftTypes = new HashSet<AircraftType>();
        }

        public Guid AircraftCategoryID { get; set; }
        [StringLength(6), Display(Name = "飞机类别")]
        public string Category { get; set; } // 客机、货机
        [StringLength(30), Display(Name = "座级")]
        public string Regional { get; set; } //座级：250座以上客机、100-200座客机、100座以下客机；大型货机、中型货机

        public virtual ICollection<AircraftType> AircraftTypes { get; set; }
    }
}
