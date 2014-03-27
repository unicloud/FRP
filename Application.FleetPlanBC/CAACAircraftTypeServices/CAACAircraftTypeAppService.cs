#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/03/25，14:03
// 文件名：CAACAircraftTypeAppService.cs
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
using UniCloud.Application.FleetPlanBC.DTO.CAACAircraftTypeDTO;
using UniCloud.Application.FleetPlanBC.Query.CAACAircraftTypeQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.CAACAircraftTypeAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.CAACAircraftTypeServices
{
    /// <summary>
    ///     实现民航机型服务接口。
    ///     用于处理民航机型相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class CAACAircraftTypeAppService : ContextBoundObject, ICAACAircraftTypeAppService
    {
        private readonly ICAACAircraftTypeQuery _caacAircraftTypeQuery;

        public CAACAircraftTypeAppService(ICAACAircraftTypeQuery caacAircraftTypeQuery)
        {
            _caacAircraftTypeQuery = caacAircraftTypeQuery;
        }

        #region CAACAircraftTypeDTO

        /// <summary>
        ///     获取所有民航机型
        /// </summary>
        /// <returns></returns>
        public IQueryable<CAACAircraftTypeDTO> GetCAACAircraftTypes()
        {
            var queryBuilder =
                new QueryBuilder<CAACAircraftType>();
            return _caacAircraftTypeQuery.CAACAircraftTypeDTOQuery(queryBuilder);
        }

        #endregion
    }
}