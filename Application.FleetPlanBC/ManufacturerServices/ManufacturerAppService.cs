#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：ManufacturerAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.ManufacturerQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManufacturerAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.ManufacturerServices
{
    /// <summary>
    ///     实现制造商服务接口。
    ///     用于处理制造商相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class ManufacturerAppService : IManufacturerAppService
    {
        private readonly IManufacturerQuery _manufacturerQuery;

        public ManufacturerAppService(IManufacturerQuery manufacturerQuery)
        {
            _manufacturerQuery = manufacturerQuery;
        }

        #region ManufacturerDTO

        /// <summary>
        ///     获取所有制造商
        /// </summary>
        /// <returns></returns>
        public IQueryable<ManufacturerDTO> GetManufacturers()
        {
            var queryBuilder =
                new QueryBuilder<Manufacturer>();
            return _manufacturerQuery.ManufacturerDTOQuery(queryBuilder);
        }

        #endregion
    }
}
