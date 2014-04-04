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
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg;

#endregion

namespace UniCloud.Application.PartBC.SpecialConfigServices
{
    /// <summary>
    ///     实现SpecialConfig的服务接口。
    ///     用于处理SpecialConfig相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class SpecialConfigAppService : ContextBoundObject, ISpecialConfigAppService
    {
        private readonly IContractAircraftRepository _contractAircraftRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ISpecialConfigQuery _specialConfigQuery;
        private readonly ISpecialConfigRepository _specialConfigRepository;

        public SpecialConfigAppService(ISpecialConfigQuery specialConfigQuery,
            ISpecialConfigRepository specialConfigRepository,
            IContractAircraftRepository contractAircraftRepository,
            IItemRepository itemRepository)
        {
            _specialConfigQuery = specialConfigQuery;
            _specialConfigRepository = specialConfigRepository;
            _contractAircraftRepository = contractAircraftRepository;
            _itemRepository = itemRepository;
        }

        #region SpecialConfigDTO

        /// <summary>
        ///     获取所有SpecialConfig。
        /// </summary>
        public IQueryable<SpecialConfigDTO> GetSpecialConfigs()
        {
            var queryBuilder =
                new QueryBuilder<SpecialConfig>();
            return _specialConfigQuery.SpecialConfigDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增SpecialConfig。
        /// </summary>
        /// <param name="dto">SpecialConfigDTO。</param>
        [Insert(typeof (SpecialConfigDTO))]
        public void InsertSpecialConfig(SpecialConfigDTO dto)
        {
            //获取相关数据
            Item item = _itemRepository.Get(dto.ItemId);
            ContractAircraft contractAircraft = _contractAircraftRepository.Get(dto.ContractAircraftId);
            SpecialConfig parentAcConfig = _specialConfigRepository.Get(dto.ParentId);

            SpecialConfig newSpecialConfig = SpecialConfigFactory.CreateSpecialConfig(dto.Position, dto.Description,
                item, parentAcConfig,
                dto.StartDate, dto.EndDate, contractAircraft);

            _specialConfigRepository.Add(newSpecialConfig);
        }

        /// <summary>
        ///     更新SpecialConfig。
        /// </summary>
        /// <param name="dto">SpecialConfigDTO。</param>
        [Update(typeof (SpecialConfigDTO))]
        public void ModifySpecialConfig(SpecialConfigDTO dto)
        {
            //获取相关数据
            Item item = _itemRepository.Get(dto.ItemId);
            ContractAircraft contractAircraft = _contractAircraftRepository.Get(dto.ContractAircraftId);
            SpecialConfig parentAcConfig = _specialConfigRepository.Get(dto.ParentId);

            SpecialConfig updateSpecialConfig = _specialConfigRepository.Get(dto.Id); //获取需要更新的对象。

            if (updateSpecialConfig != null)
            {
                updateSpecialConfig.SetContractAircraf(contractAircraft);
                updateSpecialConfig.SetDescription(dto.Description);
                updateSpecialConfig.SetEndDate(dto.EndDate);
                updateSpecialConfig.SetItem(item);
                updateSpecialConfig.SetParentItem(parentAcConfig);
                updateSpecialConfig.SetPosition(dto.Position);
                updateSpecialConfig.SetStartDate(dto.StartDate);
                _specialConfigRepository.Modify(updateSpecialConfig);
            }
        }

        /// <summary>
        ///     删除SpecialConfig。
        /// </summary>
        /// <param name="dto">SpecialConfigDTO。</param>
        [Delete(typeof (SpecialConfigDTO))]
        public void DeleteSpecialConfig(SpecialConfigDTO dto)
        {
            SpecialConfig delSpecialConfig = _specialConfigRepository.Get(dto.Id); //获取需要删除的对象。
            _specialConfigRepository.Remove(delSpecialConfig); //删除SpecialConfig。
        }

        #endregion
    }
}