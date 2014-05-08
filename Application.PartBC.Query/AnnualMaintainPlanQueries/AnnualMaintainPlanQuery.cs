#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 11:45:10
// 文件名：AnnualMaintainPlanQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 11:45:10
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.AnnualMaintainPlanAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.AnnualMaintainPlanQueries
{
    public class AnnualMaintainPlanQuery : IAnnualMaintainPlanQuery
    {
         private readonly IQueryableUnitOfWork _unitOfWork;
         public AnnualMaintainPlanQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
         /// EngineMaintainPlan查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
         ///  <returns>EngineMaintainPlanDTO集合</returns>
         public IQueryable<EngineMaintainPlanDTO> EngineMaintainPlanDTOQuery(QueryBuilder<EngineMaintainPlan> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<EngineMaintainPlan>()).Select(p => new EngineMaintainPlanDTO
            {
                Id = p.Id,
                AnnualId = p.AnnualId,
                BudgetManager = p.BudgetManager,
                CompanyLeader = p.CompanyLeader,
                DepartmentLeader = p.DepartmentLeader,
                DollarRate = p.DollarRate,
                MaintainPlanType = p.MaintainPlanType,
                PhoneNumber = p.PhoneNumber,
                EngineMaintainPlanDetails = p.EngineMaintainPlanDetails.Select(q => new EngineMaintainPlanDetailDTO
                {
                    Id = q.Id,
                    ChangeLlpFee = q.ChangeLlpFee,
                    ChangeLlpNumber = q.ChangeLlpNumber,
                    CustomsTax = q.CustomsTax,
                    EngineNumber = q.EngineNumber,
                    FreightFee = q.FreightFee,
                    InMaintainDate = q.InMaintainDate,
                    MaintainLevel = q.MaintainLevel,
                    NonFhaFee = q.NonFhaFee,
                    Note = q.Note,
                    OutMaintainDate = q.OutMaintainDate,
                    PartFee = q.PartFee,
                    TsnCsn = q.TsnCsn,
                    TsrCsr = q.TsrCsr,
                }).ToList(),
            });
        }
    }
}
