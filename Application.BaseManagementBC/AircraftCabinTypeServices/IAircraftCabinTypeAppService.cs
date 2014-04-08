#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 15:37:31
// 文件名：IAircraftCabinTypeAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 15:37:31
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;

#endregion

namespace UniCloud.Application.BaseManagementBC.AircraftCabinTypeServices
{
    public interface IAircraftCabinTypeAppService
    {
        /// <summary>
        ///     获取所有飞机舱位类型
        /// </summary>
        /// <returns></returns>
        IQueryable<AircraftCabinTypeDTO> GetAircraftCabinTypes();
    }
}
