#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/13 11:47:07
// 文件名：IAircraftConfigurationRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
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

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftConfigurationAgg
{
    /// <summary>
    ///     飞机配置仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{AircraftConfiguration}" />
    /// </summary>
    public interface IAircraftConfigurationRepository : IRepository<AircraftConfiguration>
    {
    }
}
