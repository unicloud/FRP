#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：ManufacturerQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManufacturerAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.ManufacturerQueries
{
    public class ManufacturerQuery : IManufacturerQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public ManufacturerQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     制造商查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>制造商DTO集合。</returns>
        public IQueryable<ManufacturerDTO> ManufacturerDTOQuery(
            QueryBuilder<Manufacturer> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Manufacturer>()).Select(p => new ManufacturerDTO
            {
                Id = p.Id,
                CnShortName = p.CnShortName,
                CnName = p.CnName,
                EnName = p.EnName,
                EnShortName = p.EnShortName,
                Note = p.Note,
                Type = p.Type,
            });
        }
    }
}