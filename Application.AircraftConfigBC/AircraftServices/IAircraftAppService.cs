#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 12:06:01
// 文件名：IAircraftAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 12:06:01
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;

#endregion

namespace UniCloud.Application.AircraftConfigBC.AircraftServices
{
    /// <summary>
    ///     表示用于实际飞机相关信息服务
    /// </summary>
    public interface IAircraftAppService
    {
        /// <summary>
        ///     获取所有实际飞机
        /// </summary>
        /// <returns>所有实际飞机</returns>
        IQueryable<AircraftDTO> GetAircrafts();

         void ModifyAircraft(AircraftDTO dto);
    }
}
