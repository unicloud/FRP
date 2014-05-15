#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:27:45
// 文件名：Manufacturer
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:27:45
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO.DataTransfer
{
    [Serializable]
    public class Manufacturer : Owner
    {
        public Manufacturer()
        {
            this.AircraftTypes = new HashSet<AircraftType>();
        }

        public virtual ICollection<AircraftType> AircraftTypes { get; set; }
    }

}
