#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 9:36:30
// 文件名：IAircraftConfigurationRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 9:36:30
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AircraftConfigurationAgg
{
    /// <summary>
    ///     飞机配置仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{AircraftConfiguration}" />
    /// </summary>
    public interface IAircraftConfigurationRepository : IRepository<AircraftConfiguration>
    {
    }
}
