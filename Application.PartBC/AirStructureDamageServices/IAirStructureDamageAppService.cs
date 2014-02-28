#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/28 14:29:38
// 文件名：IAirStructureDamageAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/28 14:29:38
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;

#endregion

namespace UniCloud.Application.PartBC.AirStructureDamageServices
{
    /// <summary>
    /// AirStructureDamage的服务接口。
    /// </summary>
    public interface IAirStructureDamageAppService
    {
        /// <summary>
        /// 获取所有AirStructureDamage。
        /// </summary>
        IQueryable<AirStructureDamageDTO> GetAirStructureDamages();
    }
}
