#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：ManagerAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.ManagerQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.ManagerServices
{
    /// <summary>
    ///     实现管理者服务接口。
    ///     用于处理管理者相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class ManagerAppService : ContextBoundObject, IManagerAppService
    {
        private readonly IManagerQuery _managerQuery;

        public ManagerAppService(IManagerQuery managerQuery)
        {
            _managerQuery = managerQuery;
        }

        #region ManagerDTO

        /// <summary>
        ///     获取所有管理者
        /// </summary>
        /// <returns></returns>
        public IQueryable<ManagerDTO> GetManagers()
        {
            var queryBuilder =
                new QueryBuilder<Manager>();
            return _managerQuery.ManagerDTOQuery(queryBuilder);
        }

        #endregion
    }
}
