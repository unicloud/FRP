#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/11 17:47:51
// 文件名：PurchaseContractEngineAppService
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
    /// <summary>
    ///     实现采购合同发动机服务接口。
    ///     用于处理采购合同发动机相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class PurchaseContractEngineAppService : ContextBoundObject, IPurchaseContractEngineAppService
    {
        private readonly IPurchaseContractEngineQuery _purchaseContractEngineQuery;
        private readonly IContractEngineRepository _contractEngineRepository;

        public PurchaseContractEngineAppService(IPurchaseContractEngineQuery purchaseContractEngineQuery,
            IContractEngineRepository contractEngineRepository)
        {
            _purchaseContractEngineQuery = purchaseContractEngineQuery;
            _contractEngineRepository = contractEngineRepository;
        }

        #region PurchaseContractEngineDTO

        /// <summary>
        ///     获取所有采购合同发动机
        /// </summary>
        /// <returns></returns>
        public IQueryable<PurchaseContractEngineDTO> GetPurchaseContractEngines()
        {
            var queryBuilder =
                new QueryBuilder<PurchaseContractEngine>();
            return _purchaseContractEngineQuery.PurchaseContractEngineDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增采购合同发动机。
        /// </summary>
        /// <param name="purchaseContractEngine">采购合同发动机DTO。</param>
        [Insert(typeof(PurchaseContractEngineDTO))]
        public void InsertPurchaseContractEngine(PurchaseContractEngineDTO purchaseContractEngine)
        {
            var newPurchaseContractEngine = ContractEngineFactory.CreatePurchaseContractEngine("", "");
            _contractEngineRepository.Add(newPurchaseContractEngine);
        }

        /// <summary>
        ///     更新采购合同发动机。
        /// </summary>
        /// <param name="purchaseContractEngine">采购合同发动机DTO。</param>
        [Update(typeof(PurchaseContractEngineDTO))]
        public void ModifyPurchaseContractEngine(PurchaseContractEngineDTO purchaseContractEngine)
        {

            var updatePurchaseContractEngine = _contractEngineRepository.GetFiltered(t => t.Id == purchaseContractEngine.PurchaseContractEngineId).FirstOrDefault();
            //获取需要更新的对象。
            if (updatePurchaseContractEngine != null)
            {
            }
            _contractEngineRepository.Modify(updatePurchaseContractEngine);
        }

        /// <summary>
        ///     删除采购合同发动机。
        /// </summary>
        /// <param name="purchaseContractEngine">采购合同发动机DTO。</param>
        [Delete(typeof(PurchaseContractEngineDTO))]
        public void DeletePurchaseContractEngine(PurchaseContractEngineDTO purchaseContractEngine)
        {
            var newPurchaseContractEngine = _contractEngineRepository.GetFiltered(t => t.Id == purchaseContractEngine.PurchaseContractEngineId).FirstOrDefault();
            //获取需要删除的对象。
            _contractEngineRepository.Remove(newPurchaseContractEngine); //删除采购合同发动机。
        }

        #endregion
    }
}
