#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/4 10:26:52
// 文件名：IAcTypeRepository
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

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AcTypeAgg
{
    /// <summary>
    ///     飞机系列仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{AcType}" />
    /// </summary>
    public interface IAcTypeRepository : IRepository<AcType>
    {
    }
}
