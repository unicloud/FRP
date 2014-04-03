#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/23 16:02:00
// 文件名：IFlightLogRepository
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

namespace UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg
{
    /// <summary>
    ///     飞行日志仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{FlightLog}" />
    /// </summary>
    public interface IFlightLogRepository : IRepository<FlightLog>
    {
    }
}
