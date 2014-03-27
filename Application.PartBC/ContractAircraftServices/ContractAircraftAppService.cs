#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ContractAircraftAppService
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
using UniCloud.Application.PartBC.Query.ContractAircraftQueries;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
#endregion

namespace UniCloud.Application.PartBC.ContractAircraftServices
{
    /// <summary>
    /// 实现ContractAircraft的服务接口。
    ///  用于处理ContractAircraft相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class ContractAircraftAppService : ContextBoundObject, IContractAircraftAppService
    {
        private readonly IContractAircraftQuery _contractAircraftQuery;
        private readonly IContractAircraftRepository _contractAircraftRepository;
        public ContractAircraftAppService(IContractAircraftQuery contractAircraftQuery,
            IContractAircraftRepository contractAircraftRepository)
        {
            _contractAircraftQuery = contractAircraftQuery;
            _contractAircraftRepository = contractAircraftRepository;
        }

        #region ContractAircraftDTO

        /// <summary>
        /// 获取所有ContractAircraft。
        /// </summary>
        public IQueryable<ContractAircraftDTO> GetContractAircrafts()
        {
            var queryBuilder =
               new QueryBuilder<ContractAircraft>();
            return _contractAircraftQuery.ContractAircraftDTOQuery(queryBuilder);
        }


        /// <summary>
        ///  更新ContractAircraft。
        /// </summary>
        /// <param name="dto">ContractAircraftDTO。</param>
        [Update(typeof(ContractAircraftDTO))]
        public void ModifyContractAircraft(ContractAircraftDTO dto)
        {
            var updateContractAircraft = _contractAircraftRepository.Get(dto.Id); //获取需要更新的对象。

            //更新。
            updateContractAircraft.SetBasicConfigGroup(dto.BasicConfigGroupId);
            _contractAircraftRepository.Modify(updateContractAircraft);
        }
        #endregion

    }
}
