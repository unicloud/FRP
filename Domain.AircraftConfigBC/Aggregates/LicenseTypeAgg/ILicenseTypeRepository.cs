﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 9:55:49
// 文件名：ILicenseTypeRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 9:55:49
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.LicenseTypeAgg
{
    /// <summary>
    ///     证照类型仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{LicenseType}" />
    /// </summary>
    public interface ILicenseTypeRepository : IRepository<LicenseType>
    {
    }
}