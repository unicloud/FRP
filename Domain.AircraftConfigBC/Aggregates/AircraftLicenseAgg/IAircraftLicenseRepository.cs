﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 9:36:30
// 文件名：IAircraftLicenseRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 9:36:30
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftLicenseAgg
{
    /// <summary>
    ///     飞机证照仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{Aircraft}" />
    /// </summary>
    public interface IAircraftLicenseRepository : IRepository<AircraftLicense>
    {
    }
}
