#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：PlanAppService.cs
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
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.AircraftPlanQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AircraftPlanServices
{
    /// <summary>
    ///     实现运力增减计划服务接口。
    ///     用于处理运力增减计划相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class PlanAppService : ContextBoundObject, IPlanAppService
    {
        private readonly IAirlinesRepository _airlinesRepository;
        private readonly IAnnualRepository _annualRepository;
        private readonly IPlanQuery _planQuery;
        private readonly IPlanRepository _planRepository;

        public PlanAppService(IPlanQuery planQuery, IAirlinesRepository airlinesRepository,
            IAnnualRepository annualRepository, IPlanRepository planRepository)
        {
            _planQuery = planQuery;
            _airlinesRepository = airlinesRepository;
            _annualRepository = annualRepository;
            _planRepository = planRepository;
        }

        #region PlanDTO

        /// <summary>
        ///     获取所有运力增减计划
        /// </summary>
        /// <returns></returns>
        public IQueryable<PlanDTO> GetPlans()
        {
            var queryBuilder =
                new QueryBuilder<Plan>();
            return _planQuery.PlanDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增运力增减计划。
        /// </summary>
        /// <param name="dto">运力增减计划DTO。</param>
        [Insert(typeof (PlanDTO))]
        public void InsertPlan(PlanDTO dto)
        {
            Airlines airlines = _airlinesRepository.Get(dto.AirlinesId); //获取航空公司
            Annual annual = _annualRepository.Get(dto.AnnualId); //获取计划年度

            //创建运力增减计划
            Plan newPlan = PlanFactory.CreatePlan(dto.VersionNumber);
            newPlan.ChangeCurrentIdentity(dto.Id);
            newPlan.SetPlanStatus(PlanStatus.草稿);
            newPlan.SetAirlines(airlines);
            newPlan.SetAnnual(annual);
            newPlan.SetTitle(dto.Title);

            _planRepository.Add(newPlan);
        }

        /// <summary>
        ///     更新运力增减计划。
        /// </summary>
        /// <param name="dto">运力增减计划DTO。</param>
        [Update(typeof (PlanDTO))]
        public void ModifyPlan(PlanDTO dto)
        {
            Airlines airlines = _airlinesRepository.Get(dto.AirlinesId); //获取航空公司
            Annual annual = _annualRepository.Get(dto.AnnualId); //获取计划年度

            //获取需要更新的对象
            Plan updatePlan = _planRepository.Get(dto.Id);

            if (updatePlan != null)
            {
                //更新计划：
                updatePlan.SetPlanStatus((PlanStatus) dto.Status);
                updatePlan.SetPlanPublishStatus((PlanPublishStatus) dto.PublishStatus);
                updatePlan.SetAirlines(airlines);
                updatePlan.SetAnnual(annual);
                updatePlan.SetDocNumber(dto.DocNumber);
                updatePlan.SetDocument(dto.DocumentId, dto.DocName);
                updatePlan.SetTitle(dto.Title);
            }
            _planRepository.Modify(updatePlan);
        }

        /// <summary>
        ///     删除运力增减计划。
        /// </summary>
        /// <param name="dto">运力增减计划DTO。</param>
        [Delete(typeof (PlanDTO))]
        public void DeletePlan(PlanDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            Plan delPlan = _planRepository.Get(dto.Id);
            //获取需要删除的对象。
            if (delPlan != null)
            {
                _planRepository.Remove(delPlan); //删除运力增减计划。
            }
        }

        #endregion

        public PerformPlan PerformPlanQuery(string planHistoryId, string approvalHistoryId, int planType,
            string relatedGuid)
        {
            return _planQuery.PerformPlanQuery(planHistoryId, approvalHistoryId, planType, relatedGuid);
        }
    }
}