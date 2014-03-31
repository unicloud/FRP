#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/16 14:36:24
// 文件名：IAircraftLicenseAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/16 14:36:24
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;

#endregion

namespace UniCloud.Application.AircraftConfigBC.AircraftLicenseServices
{
    /// <summary>
    ///     飞机证照服务接口。
    /// </summary>
    public interface IAircraftLicenseAppService
    {

        /// <summary>
        ///     获取所有证照类型
        /// </summary>
        /// <returns></returns>
        IQueryable<LicenseTypeDTO> GetLicenseTypes();
    }
}
