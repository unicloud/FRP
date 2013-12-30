#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：EngineQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.EngineQueries
{
    public class EngineQuery : IEngineQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public EngineQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     发动机查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>发动机DTO集合。</returns>
        public IQueryable<EngineDTO> EngineDTOQuery(
            QueryBuilder<Engine> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Engine>()).Select(p => new EngineDTO
            {
                Id = p.Id,
                AirlinesId = p.AirlinesId,
                CreateDate = p.CreateDate,
                EngineTypeId = p.EngineTypeId,
                ExportDate = p.ExportDate,
                FactoryDate = p.FactoryDate,
                ImportCategoryId = p.ImportCategoryId,
                ImportDate = p.ImportDate,
                MaxThrust = p.MaxThrust,
                SupplierId = p.SupplierId,
                SerialNumber = p.SerialNumber,
                EngineBusinessHistories = p.EngineBusinessHistories.Select(q=>new EngineBusinessHistoryDTO
                {
                    Id = q.Id,
                    EndDate = q.EndDate,
                    EngineId = q.EngineId,
                    EngineTypeId = q.EngineTypeId,
                    ImportCategoryId = q.ImportCategoryId,
                    MaxThrust = q.MaxThrust,
                    StartDate = q.StartDate,
                }).ToList(),
                EngineOwnerShipHistories = p.EngineOwnerShipHistories.Select(q=>new EngineOwnershipHistoryDTO
                {
                    Id = q.Id,
                    EndDate = q.EndDate,
                    EngineId = q.EngineId,
                    StartDate = q.StartDate,
                    SupplierId = q.SupplierId,
                }).ToList(),
            });
        }
    }
}