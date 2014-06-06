#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/03，10:04
// 文件名：BasicConfigHistoryAppService.cs
// 程序集：UniCloud.Application.PartBC
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
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.BasicConfigHistoryQueries;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;

#endregion

namespace UniCloud.Application.PartBC.BasicConfigHistoryServices
{
    /// <summary>
    ///     实现基本构型历史服务接口。
    ///     用于处理基本构型历史相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class BasicConfigHistoryAppService : IBasicConfigHistoryAppService
    {
        private readonly IBasicConfigGroupRepository _basicConfigGroupRepository;
        private readonly IBasicConfigHistoryQuery _basicConfigHistoryQuery;
        private readonly IBasicConfigHistoryRepository _basicConfigHistoryRepository;
        private readonly IContractAircraftRepository _contractAircraftRepository;

        public BasicConfigHistoryAppService(IBasicConfigHistoryQuery basicConfigHistoryQuery,
            IBasicConfigGroupRepository basicConfigGroupRepository,
            IBasicConfigHistoryRepository basicConfigHistoryRepository,
            IContractAircraftRepository contractAircraftRepository
            )
        {
            _basicConfigHistoryQuery = basicConfigHistoryQuery;
            _basicConfigGroupRepository = basicConfigGroupRepository;
            _basicConfigHistoryRepository = basicConfigHistoryRepository;
            _contractAircraftRepository = contractAircraftRepository;
        }

        #region BasicConfigHistoryDTO

        /// <summary>
        ///     获取所有基本构型历史
        /// </summary>
        /// <returns></returns>
        public IQueryable<BasicConfigHistoryDTO> GetBasicConfigHistories()
        {
            var queryBuilder =
                new QueryBuilder<BasicConfigHistory>();
            return _basicConfigHistoryQuery.BasicConfigHistoryDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增BasicConfigHistory。
        /// </summary>
        /// <param name="dto">BasicConfigHistoryDTO。</param>
        [Insert(typeof (BasicConfigHistoryDTO))]
        public void InsertBasicConfigHistory(BasicConfigHistoryDTO dto)
        {
            BasicConfigGroup basicConfigGroup = _basicConfigGroupRepository.Get(dto.BasicConfigGroupId); //获取基本构型组
            ContractAircraft contractAircraft = _contractAircraftRepository.Get(dto.ContractAircraftId); //获取合同飞机

            //创建基本构型历史
            BasicConfigHistory newBasicConfigHistory = BasicConfigHistoryFactory.CreateBasicConfigHistory(
                contractAircraft, basicConfigGroup, dto.StartDate, dto.EndDate);
            newBasicConfigHistory.ChangeCurrentIdentity(dto.Id);

            _basicConfigHistoryRepository.Add(newBasicConfigHistory);
        }

        /// <summary>
        ///     更新BasicConfigHistory。
        /// </summary>
        /// <param name="dto">BasicConfigHistoryDTO。</param>
        [Update(typeof (BasicConfigHistoryDTO))]
        public void ModifyBasicConfigHistory(BasicConfigHistoryDTO dto)
        {
            BasicConfigGroup basicConfigGroup = _basicConfigGroupRepository.Get(dto.BasicConfigGroupId); //获取基本构型组
            ContractAircraft contractAircraft = _contractAircraftRepository.Get(dto.ContractAircraftId); //获取合同飞机

            //获取需要更新的对象
            BasicConfigHistory updateBasicConfigHistory = _basicConfigHistoryRepository.Get(dto.Id);
            if (updateBasicConfigHistory != null)
            {
                updateBasicConfigHistory.SetBasicConfigGroup(basicConfigGroup);
                updateBasicConfigHistory.SetContractAircraft(contractAircraft);
                updateBasicConfigHistory.SetEndDate(dto.EndDate);
                updateBasicConfigHistory.SetStartDate(dto.StartDate);
                _basicConfigHistoryRepository.Modify(updateBasicConfigHistory);
            }
        }

        /// <summary>
        ///     删除BasicConfigHistory。
        /// </summary>
        /// <param name="dto">BasicConfigHistoryDTO。</param>
        [Delete(typeof (BasicConfigHistoryDTO))]
        public void DeleteBasicConfigHistory(BasicConfigHistoryDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            BasicConfigHistory delBasicConfigHistory = _basicConfigHistoryRepository.Get(dto.Id);
            //获取需要删除的对象。

            if (delBasicConfigHistory != null)
            {
                _basicConfigHistoryRepository.Remove(delBasicConfigHistory); //删除基本构型历史。
            }
        }

        #endregion
    }
}