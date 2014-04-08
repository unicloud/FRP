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

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftConfigurationAgg
{
    /// <summary>
    ///     飞机配置仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{AircraftConfiguration}" />
    /// </summary>
    public interface IAircraftConfigurationRepository : IRepository<AircraftConfiguration>
    {
        /// <summary>
        /// 删除飞机配置
        /// </summary>
        /// <param name="aircraftConfiguration"></param>
        void DeleteAircraftConfiguration(AircraftConfiguration aircraftConfiguration);

        /// <summary>
        /// 删除舱位
        /// </summary>
        /// <param name="aircraftCabin"></param>
        void DeleteAircraftCabin(AircraftCabin aircraftCabin);
    }
}
