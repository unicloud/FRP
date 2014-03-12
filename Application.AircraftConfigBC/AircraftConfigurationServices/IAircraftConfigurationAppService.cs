#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 14:23:12
// 文件名：IAircraftConfigurationAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 14:23:12
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;

namespace UniCloud.Application.AircraftConfigBC.AircraftConfigurationServices
{
    /// <summary>
    ///     飞机配置服务接口。
    /// </summary>
    public interface IAircraftConfigurationAppService
    {
        /// <summary>
        ///     获取所有飞机配置
        /// </summary>
        /// <returns></returns>
        IQueryable<AircraftConfigurationDTO> GetAircraftConfigurations();
    }
}
