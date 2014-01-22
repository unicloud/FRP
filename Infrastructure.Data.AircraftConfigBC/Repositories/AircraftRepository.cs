﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 9:46:46
// 文件名：AircraftRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftLicenseAgg;
using UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork;
using System.Data.Entity;
#endregion

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.Repositories
{
    /// <summary>
    ///     实际飞机仓储实现
    /// </summary>
    public class AircraftRepository : Repository<Aircraft>, IAircraftRepository
    {
        public AircraftRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载
        public override Aircraft Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as AircraftConfigBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Aircraft>();
            return set.Include(t => t.Licenses).FirstOrDefault(p => p.Id == (Guid)id);
        }
        #endregion

        public void RemoveAircraftLicense(AircraftLicense aircraftLicense)
        {
            var currentUnitOfWork = UnitOfWork as AircraftConfigBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var set = currentUnitOfWork.CreateSet<AircraftLicense>();
            set.Remove(aircraftLicense);
        }
    }
}
