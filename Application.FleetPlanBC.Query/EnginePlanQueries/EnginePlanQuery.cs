#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：EnginePlanQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.EnginePlanQueries
{
    public class EnginePlanQuery : IEnginePlanQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public EnginePlanQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     备发计划查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>备发计划DTO集合。</returns>
        public IQueryable<EnginePlanDTO> EnginePlanDTOQuery(
            QueryBuilder<EnginePlan> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<EnginePlan>()).Select(p => new EnginePlanDTO
            {
                Id = p.Id,
                AirlinesId = p.AirlinesId,
                AnnualId = p.AnnualId,
                CreateDate = p.CreateDate,
                DocName = p.DocName,
                DocNumber = p.DocNumber,
                DocumentId = p.DocumentId,
                IsFinished = p.IsFinished,
                IsValid = p.IsValid,
                Note = p.Note,
                Status = (int)p.Status,
                Title = p.Title,
                VersionNumber = p.VersionNumber,
                EnginePlanHistories = p.EnginePlanHistories.Select(q=>new EnginePlanHistoryDTO
                {
                    Id = q.Id,
                    ActionCategoryId = q.ActionCategoryId,
                    EnginePlanId = q.EnginePlanId,
                    EngineTypeId = q.EngineTypeId,
                    IsFinished = q.IsFinished,
                    MaxThrust = q.MaxThrust,
                    PerformAnnualId = q.PerformAnnualId,
                    PerformMonth = q.PerformMonth,
                    PlanEngineId = q.PlanEngineId,
                    Status = (int)q.Status,
                }).ToList(),
                
            });
        }
    }
}