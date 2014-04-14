#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/13，21:04
// 文件名：AcConfigAppService.cs
// 程序集：UniCloud.Application.PartBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PartBC.Query.AcConfigQueries;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates;
using UniCloud.Domain.PartBC.Aggregates.AdSbAgg;

#endregion

namespace UniCloud.Application.PartBC.AcConfigServices
{
    /// <summary>
    ///     实现飞机构型服务接口。
    ///     用于处理飞机构型相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AcConfigAppService : IAcConfigAppService
    {
        private readonly IAcConfigQuery _acConfigQuery;

        public AcConfigAppService(IAcConfigQuery acConfigQuery)
        {
            _acConfigQuery = acConfigQuery;
        }

        #region AcConfigDTO

        /// <summary>
        /// 获取所有AcConfig。
        /// </summary>
        public IQueryable<AcConfigDTO> GetAcConfigs()
        {
            var queryBuilder =
               new QueryBuilder<AcConfig>();
            return _acConfigQuery.AcConfigDTOQuery(queryBuilder);
        }

        /// <summary>
        /// 获取所有飞机构型
        /// </summary>
        /// <param name="contractAircraftId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<AcConfigDTO> QueryAcConfigs(int contractAircraftId, DateTime date)
        {
            return _acConfigQuery.QueryAcConfigs(contractAircraftId, date);
        }

        #endregion
    }
}
