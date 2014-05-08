#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 11:52:24
// 文件名：AnnualMaintainPlanAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 11:52:24
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.AnnualMaintainPlanQueries;
using UniCloud.Domain.PartBC.Aggregates.AnnualMaintainPlanAgg;

#endregion

namespace UniCloud.Application.PartBC.AnnualMaintainPlanServices
{
    [LogAOP]
    public class AnnualMaintainPlanAppService : ContextBoundObject, IAnnualMaintainPlanAppService
    {
        private readonly IAnnualMaintainPlanQuery _annualMaintainPlanQuery;
        private readonly IAnnualMaintainPlanRepository _aunualMaintainPlanRepository;
        public AnnualMaintainPlanAppService(IAnnualMaintainPlanQuery annualMaintainPlanQuery, IAnnualMaintainPlanRepository aunualMaintainPlanRepository)
        {
            _annualMaintainPlanQuery = annualMaintainPlanQuery;
            _aunualMaintainPlanRepository = aunualMaintainPlanRepository;
        }

        #region EngineMaintainPlanDTO
        /// <summary>
        ///     获取所有发动机维修计划。
        /// </summary>
        /// <returns>所有发动机维修计划。</returns>
        public IQueryable<EngineMaintainPlanDTO> GetEngineMaintainPlans()
        {
            var queryBuilder = new QueryBuilder<EngineMaintainPlan>();
            return _annualMaintainPlanQuery.EngineMaintainPlanDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增发动机维修计划。
        /// </summary>
        /// <param name="engineMaintainPlan">发动机维修计划DTO。</param>
        [Insert(typeof(EngineMaintainPlanDTO))]
        public void InsertEngineMaintainPlan(EngineMaintainPlanDTO engineMaintainPlan)
        {
            var newEngineMaintainPlan = AnnualMaintainPlanFactory.CreatEngineMaintainPlan();
            AnnualMaintainPlanFactory.SetEngineMaintainPlan(newEngineMaintainPlan, engineMaintainPlan.MaintainPlanType, engineMaintainPlan.DollarRate, engineMaintainPlan.CompanyLeader, engineMaintainPlan.DepartmentLeader, engineMaintainPlan.BudgetManager,
                engineMaintainPlan.PhoneNumber);
            if (engineMaintainPlan.EngineMaintainPlanDetails != null)
            {
                foreach (var engineMaintainPlanLine in engineMaintainPlan.EngineMaintainPlanDetails)
                {
                    var newEngineMaintainPlanLine = AnnualMaintainPlanFactory.CreatEngineMaintainPlanDetail();
                    AnnualMaintainPlanFactory.SetEngineMaintainPlanDetail(newEngineMaintainPlanLine, engineMaintainPlanLine.ChangeLlpFee, engineMaintainPlanLine.ChangeLlpNumber, engineMaintainPlanLine.CustomsTax, engineMaintainPlanLine.EngineNumber,
                        engineMaintainPlanLine.FreightFee, engineMaintainPlanLine.InMaintainDate, engineMaintainPlanLine.MaintainLevel, engineMaintainPlanLine.NonFhaFee, engineMaintainPlanLine.Note, engineMaintainPlanLine.OutMaintainDate, engineMaintainPlanLine.PartFee,
                        engineMaintainPlanLine.TsnCsn, engineMaintainPlanLine.TsrCsr);
                    newEngineMaintainPlan.EngineMaintainPlanDetails.Add(newEngineMaintainPlanLine);
                }
            }
            _aunualMaintainPlanRepository.Add(newEngineMaintainPlan);
        }


        /// <summary>
        ///     更新发动机维修计划。
        /// </summary>
        /// <param name="engineMaintainPlan">发动机维修计划DTO。</param>
        [Update(typeof(EngineMaintainPlanDTO))]
        public void ModifyEngineMaintainPlan(EngineMaintainPlanDTO engineMaintainPlan)
        {
            var updateEngineMaintainPlan = _aunualMaintainPlanRepository.GetEngineMaintainPlan(engineMaintainPlan.Id); //获取需要更新的对象。
            AnnualMaintainPlanFactory.SetEngineMaintainPlan(updateEngineMaintainPlan, engineMaintainPlan.MaintainPlanType, engineMaintainPlan.DollarRate, engineMaintainPlan.CompanyLeader, engineMaintainPlan.DepartmentLeader, engineMaintainPlan.BudgetManager,
                 engineMaintainPlan.PhoneNumber);
            UpdateEngineMaintainPlanDetails(engineMaintainPlan.EngineMaintainPlanDetails, updateEngineMaintainPlan);
            _aunualMaintainPlanRepository.Modify(updateEngineMaintainPlan);
        }

        /// <summary>
        ///     删除发动机维修计划。
        /// </summary>
        /// <param name="engineMaintainPlan">发动机维修计划DTO。</param>
        [Delete(typeof(EngineMaintainPlanDTO))]
        public void DeleteEngineMaintainPlan(EngineMaintainPlanDTO engineMaintainPlan)
        {
            var deleteEngineMaintainPlan = _aunualMaintainPlanRepository.GetEngineMaintainPlan(engineMaintainPlan.Id); //获取需要删除的对象。
            UpdateEngineMaintainPlanDetails(new List<EngineMaintainPlanDetailDTO>(), deleteEngineMaintainPlan);
            _aunualMaintainPlanRepository.Remove(deleteEngineMaintainPlan); //删除发动机维修计划。
        }
        #endregion

        #region 更新发动机维修计划行集合
        /// <summary>
        /// 更新发动机维修计划行集合
        /// </summary>
        /// <param name="sourceEngineMaintainPlanDetails">客户端集合</param>
        /// <param name="dstEngineMaintainPlan">数据库集合</param>
        private void UpdateEngineMaintainPlanDetails(IEnumerable<EngineMaintainPlanDetailDTO> sourceEngineMaintainPlanDetails, EngineMaintainPlan dstEngineMaintainPlan)
        {
            var engineMaintainPlanLines = new List<EngineMaintainPlanDetail>();
            foreach (var sourceEngineMaintainPlanLine in sourceEngineMaintainPlanDetails)
            {
                var result = dstEngineMaintainPlan.EngineMaintainPlanDetails.FirstOrDefault(p => p.Id == sourceEngineMaintainPlanLine.Id);
                if (result == null)
                {
                    result = AnnualMaintainPlanFactory.CreatEngineMaintainPlanDetail();
                    result.ChangeCurrentIdentity(sourceEngineMaintainPlanLine.Id);
                }
                AnnualMaintainPlanFactory.SetEngineMaintainPlanDetail(result, sourceEngineMaintainPlanLine.ChangeLlpFee, sourceEngineMaintainPlanLine.ChangeLlpNumber, sourceEngineMaintainPlanLine.CustomsTax, sourceEngineMaintainPlanLine.EngineNumber,
                        sourceEngineMaintainPlanLine.FreightFee, sourceEngineMaintainPlanLine.InMaintainDate, sourceEngineMaintainPlanLine.MaintainLevel, sourceEngineMaintainPlanLine.NonFhaFee, sourceEngineMaintainPlanLine.Note, sourceEngineMaintainPlanLine.OutMaintainDate, sourceEngineMaintainPlanLine.PartFee,
                        sourceEngineMaintainPlanLine.TsnCsn, sourceEngineMaintainPlanLine.TsrCsr);
                engineMaintainPlanLines.Add(result);
            }
            dstEngineMaintainPlan.EngineMaintainPlanDetails.ToList().ForEach(p =>
            {
                if (engineMaintainPlanLines.FirstOrDefault(t => t.Id == p.Id) == null)
                {
                    _aunualMaintainPlanRepository.RemoveEngineMaintainPlanDetail(p);
                }
            });
            dstEngineMaintainPlan.EngineMaintainPlanDetails = engineMaintainPlanLines;
        }
        #endregion
    }
}
