#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SpecialConfigAppService
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.SpecialConfigQueries;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg;

#endregion

namespace UniCloud.Application.PartBC.SpecialConfigServices
{
    /// <summary>
    /// 实现SpecialConfig的服务接口。
    ///  用于处理SpecialConfig相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class SpecialConfigAppService : ContextBoundObject, ISpecialConfigAppService
    {
        private readonly ISpecialConfigQuery _specialConfigQuery;
        private readonly ISpecialConfigRepository _specialConfigRepository;
        private readonly ITechnicalSolutionRepository _technicalSolutionRepository;
        private readonly IContractAircraftRepository _contractAircraftRepository;
        public SpecialConfigAppService(ISpecialConfigQuery specialConfigQuery,
            ISpecialConfigRepository specialConfigRepository,
            ITechnicalSolutionRepository technicalSolutionRepository,
            IContractAircraftRepository contractAircraftRepository)
        {
            _specialConfigQuery = specialConfigQuery;
            _specialConfigRepository = specialConfigRepository;
            _technicalSolutionRepository = technicalSolutionRepository;
            _contractAircraftRepository = contractAircraftRepository;
        }

        #region SpecialConfigDTO

        /// <summary>
        /// 获取所有SpecialConfig。
        /// </summary>
        public IQueryable<SpecialConfigDTO> GetSpecialConfigs()
        {
            var queryBuilder =
               new QueryBuilder<SpecialConfig>();
            return _specialConfigQuery.SpecialConfigDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增SpecialConfig。
        /// </summary>
        /// <param name="dto">SpecialConfigDTO。</param>
        [Insert(typeof(SpecialConfigDTO))]
        public void InsertSpecialConfig(SpecialConfigDTO dto)
        {
            //获取相关数据
            var ts = _technicalSolutionRepository.Get(dto.TsId);
            var contractAircraft = _contractAircraftRepository.Get(dto.ContractAircraftId);

            var newSpecialConfig = SpecialConfigFactory.CreateSpecialConfig();

            newSpecialConfig.SetEndDate(dto.EndDate);
            newSpecialConfig.SetDescription(dto.Description);
            newSpecialConfig.SetParentItemNo(dto.ParentItemNo);
            newSpecialConfig.SetIsValid(dto.IsValid);
            newSpecialConfig.SetItemNo(dto.ItemNo);
            newSpecialConfig.SetStartDate(dto.StartDate);
            newSpecialConfig.SetParentAcConfigId(dto.ParentId);
            newSpecialConfig.SetTechnicalSolution(ts);
            newSpecialConfig.SetContractAircraf(contractAircraft);
            _specialConfigRepository.Add(newSpecialConfig);
        }

        /// <summary>
        ///  更新SpecialConfig。
        /// </summary>
        /// <param name="dto">SpecialConfigDTO。</param>
        [Update(typeof(SpecialConfigDTO))]
        public void ModifySpecialConfig(SpecialConfigDTO dto)
        {            
            //获取相关数据
            var ts = _technicalSolutionRepository.Get(dto.TsId);
            var contractAircraft = _contractAircraftRepository.Get(dto.ContractAircraftId);

            var updateSpecialConfig = _specialConfigRepository.Get(dto.Id); //获取需要更新的对象。

            //更新。
            updateSpecialConfig.SetEndDate(dto.EndDate);
            updateSpecialConfig.SetDescription(dto.Description);
            updateSpecialConfig.SetParentItemNo(dto.ParentItemNo);
            updateSpecialConfig.SetIsValid(dto.IsValid);
            updateSpecialConfig.SetItemNo(dto.ItemNo);
            updateSpecialConfig.SetStartDate(dto.StartDate);
            updateSpecialConfig.SetParentAcConfigId(dto.ParentId);
            updateSpecialConfig.SetTechnicalSolution(ts);
            updateSpecialConfig.SetContractAircraf(contractAircraft);
            _specialConfigRepository.Modify(updateSpecialConfig);
        }

        /// <summary>
        ///  删除SpecialConfig。
        /// </summary>
        /// <param name="dto">SpecialConfigDTO。</param>
        [Delete(typeof(SpecialConfigDTO))]
        public void DeleteSpecialConfig(SpecialConfigDTO dto)
        {
            var delSpecialConfig = _specialConfigRepository.Get(dto.Id); //获取需要删除的对象。
            _specialConfigRepository.Remove(delSpecialConfig); //删除SpecialConfig。
        }

        #endregion

    }
}
