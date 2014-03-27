#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：PlanEngineAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.PlanEngineQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanEngineAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.PlanEngineServices
{
    /// <summary>
    ///     实现计划发动机服务接口。
    ///     用于处理计划发动机相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class PlanEngineAppService : ContextBoundObject, IPlanEngineAppService
    {
        private readonly IPlanEngineQuery _planEngineQuery;
        private readonly IAirlinesRepository _airlinesRepository;
        private readonly IEngineRepository _engineRepository;
        private readonly IEngineTypeRepository _engineTypeRepository;
        private readonly IPlanEngineRepository _planEngineRepository;

        public PlanEngineAppService(IPlanEngineQuery planEngineQuery,
            IAirlinesRepository airlinesRepository,IEngineRepository engineRepository,
            IEngineTypeRepository engineTypeRepository,IPlanEngineRepository planEngineRepository)
        {
            _planEngineQuery = planEngineQuery;
            _airlinesRepository = airlinesRepository;
            _engineRepository = engineRepository;
            _engineTypeRepository = engineTypeRepository;
            _planEngineRepository = planEngineRepository;
        }

        #region PlanEngineDTO

        /// <summary>
        ///     获取所有计划发动机
        /// </summary>
        /// <returns></returns>
        public IQueryable<PlanEngineDTO> GetPlanEngines()
        {
            var queryBuilder =
                new QueryBuilder<PlanEngine>();
            return _planEngineQuery.PlanEngineDTOQuery(queryBuilder);
        }


        /// <summary>
        ///     新增计划发动机。
        /// </summary>
        /// <param name="dto">计划发动机DTO。</param>
        [Insert(typeof(PlanEngineDTO))]
        public void InsertPlanEngine(PlanEngineDTO dto)
        {
            var engineType = _engineTypeRepository.Get(dto.EngineTypeId);
            var airlines = _airlinesRepository.Get(dto.AirlinesId);

            //创建计划发动机
            var newPlanEngine = PlanEngineFactory.CreatePlanEngine();
            newPlanEngine.ChangeCurrentIdentity(dto.Id);
            newPlanEngine.SetEngineType(engineType);
            newPlanEngine.SetAirlines(airlines);

            _planEngineRepository.Add(newPlanEngine);
        }

        /// <summary>
        ///     更新计划发动机。
        /// </summary>
        /// <param name="dto">计划发动机DTO。</param>
        [Update(typeof(PlanEngineDTO))]
        public void ModifyPlanEngine(PlanEngineDTO dto)
        {
            var engine = _engineRepository.Get(dto.EngineId);
            var engineType = _engineTypeRepository.Get(dto.EngineTypeId);
            var airlines = _airlinesRepository.Get(dto.AirlinesId);

            //获取需要更新的对象
            var updatePlanEngine = _planEngineRepository.Get(dto.Id);

            if (updatePlanEngine != null)
            {
                //更新主表：
                updatePlanEngine.SetEngine(engine);
                updatePlanEngine.SetEngineType(engineType);
                updatePlanEngine.SetAirlines(airlines);
            }
            _planEngineRepository.Modify(updatePlanEngine);
        }

        /// <summary>
        ///     删除计划发动机。
        /// </summary>
        /// <param name="dto">计划发动机DTO。</param>
        [Delete(typeof(PlanEngineDTO))]
        public void DeletePlanEngine(PlanEngineDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delPlanEngine = _planEngineRepository.Get(dto.Id);
            //获取需要删除的对象。
            if (delPlanEngine != null)
            {
                _planEngineRepository.Remove(delPlanEngine); //删除计划发动机。
            }
        }
        #endregion
    }
}
