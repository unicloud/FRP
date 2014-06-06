#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/23，14:18
// 方案：FRP
// 项目：Application.PartBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.OilMonitorQueries;
using UniCloud.Domain.PartBC.Aggregates.OilMonitorAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;

#endregion

namespace UniCloud.Application.PartBC.OilMonitorServices
{
    /// <summary>
    ///     滑油监控应用服务接口实现
    /// </summary>
    [LogAOP]
    public class OilMonitorAppService : ContextBoundObject, IOilMonitorAppService
    {
        private readonly IOilMonitorQuery _oilMonitorQuery;

        public OilMonitorAppService(IOilMonitorQuery oilMonitorQuery)
        {
            _oilMonitorQuery = oilMonitorQuery;
        }

        #region IOilMonitorAppService 成员

        public IQueryable<EngineOilDTO> GetEngineOils()
        {
            var query = new QueryBuilder<SnReg>();
            return _oilMonitorQuery.EngineOilDTOQuery(query);
        }

        public IQueryable<APUOilDTO> GetAPUOils()
        {
            var query = new QueryBuilder<SnReg>();
            return _oilMonitorQuery.APUOilDTOQuery(query);
        }

        public IQueryable<OilMonitorDTO> GetOilMonitors()
        {
            var query = new QueryBuilder<OilMonitor>();
            return _oilMonitorQuery.OilMonitorDTOQuery(query);
        }

        #endregion
    }
}