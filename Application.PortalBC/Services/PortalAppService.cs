#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：Application.PortalBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.PortalBC.DTO;
using UniCloud.Application.PortalBC.Query;
using UniCloud.Domain.PortalBC.Aggregates.AircraftSeriesAgg;

#endregion

namespace UniCloud.Application.PortalBC.Services
{
    /// <summary>
    ///     采购应用服务
    /// </summary>
    public class PortalAppService : ContextBoundObject, IPortalAppService
    {
        private readonly IPortalQuery _portalQuery;

        public PortalAppService(IPortalQuery portalQuery)
        {
            _portalQuery = portalQuery;
        }

        #region IPortalAppService 成员

        public IQueryable<AircraftSeriesDTO> GetAircraftSeries()
        {
            var query = new QueryBuilder<AircraftSeries>();
            return _portalQuery.AircraftSeriesDTOQuery(query);
        }

        #endregion
    }
}