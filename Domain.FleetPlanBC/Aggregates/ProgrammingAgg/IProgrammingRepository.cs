#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:13:50
// 文件名：IProgrammingRepository
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

namespace UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg
{
    /// <summary>
    ///     规划期间仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{Programming}" />
    /// </summary>
    public interface IProgrammingRepository : IRepository<Programming>
    {
    }
}
