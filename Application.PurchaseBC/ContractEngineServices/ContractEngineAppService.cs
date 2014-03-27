#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/11 17:46:04
// 文件名：ContractEngineAppService
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
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.ContractEngineQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractEngineAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.ContractEngineServices
{
    [LogAOP]
    public class ContractEngineAppService : ContextBoundObject, IContractEngineAppService
    {
        private readonly IContractEngineQuery _contractEngineQuery;
        private readonly IContractEngineRepository _contractEngineRepository;
        public ContractEngineAppService(IContractEngineQuery contractEngineQuery,
            IContractEngineRepository contractEngineRepository)
        {
            _contractEngineQuery = contractEngineQuery;
            _contractEngineRepository = contractEngineRepository;
        }

        #region ContractEngineDTO

        /// <summary>
        ///     获取所有合同发动机
        /// </summary>
        /// <returns></returns>
        public IQueryable<ContractEngineDTO> GetContractEngines()
        {
            var query =
                new QueryBuilder<ContractEngine>();
            return _contractEngineQuery.ContractEngineDTOQuery(query);
        }

        /// <summary>
        ///     更新合同发动机。
        /// </summary>
        /// <param name="contractEngine">租赁合同发动机DTO。</param>
        [Update(typeof(ContractEngineDTO))]
        public void ModifyContractEngine(ContractEngineDTO contractEngine)
        {
            var updateContractEngine = _contractEngineRepository
                .GetFiltered(t => t.ContractNumber == contractEngine.ContractNumber && t.RankNumber == contractEngine.RankNumber).FirstOrDefault();
            _contractEngineRepository.Modify(updateContractEngine);
        }

        #endregion

    }
}
