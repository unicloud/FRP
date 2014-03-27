#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/11 17:46:56
// 文件名：LeaseContractEngineAppService
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
    ///     实现租赁合同发动机服务接口。
    ///     用于处理租赁合同发动机相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class LeaseContractEngineAppService : ContextBoundObject, ILeaseContractEngineAppService
    {
        private readonly ILeaseContractEngineQuery _leaseContractEngineQuery;
        private readonly IContractEngineRepository _contractEngineRepository;

        public LeaseContractEngineAppService(ILeaseContractEngineQuery leaseContractEngineQuery,
            IContractEngineRepository contractEngineRepository)
        {
            _leaseContractEngineQuery = leaseContractEngineQuery;
            _contractEngineRepository = contractEngineRepository;
        }

        #region LeaseContractEngineDTO

        /// <summary>
        ///     获取所有租赁合同发动机
        /// </summary>
        /// <returns></returns>
        public IQueryable<LeaseContractEngineDTO> GetLeaseContractEngines()
        {
            var queryBuilder =
                new QueryBuilder<LeaseContractEngine>();
            return _leaseContractEngineQuery.LeaseContractEngineDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增租赁合同发动机。
        /// </summary>
        /// <param name="leaseContractEngine">租赁合同发动机DTO。</param>
        [Insert(typeof(LeaseContractEngineDTO))]
        public void InsertLeaseContractEngine(LeaseContractEngineDTO leaseContractEngine)
        {
            var newLeaseContractEngine = ContractEngineFactory.CreateLeaseContractEngine("", "");
            _contractEngineRepository.Add(newLeaseContractEngine);
        }

        /// <summary>
        ///     更新租赁合同发动机。
        /// </summary>
        /// <param name="leaseContractEngine">租赁合同发动机DTO。</param>
        [Update(typeof(LeaseContractEngineDTO))]
        public void ModifyLeaseContractEngine(LeaseContractEngineDTO leaseContractEngine)
        {

            var updateLeaseContractEngine = _contractEngineRepository.GetFiltered(t => t.Id == leaseContractEngine.LeaseContractEngineId).FirstOrDefault();
            //获取需要更新的对象。
            if (updateLeaseContractEngine != null)
            {
            }
            _contractEngineRepository.Modify(updateLeaseContractEngine);
        }

        /// <summary>
        ///     删除租赁合同发动机。
        /// </summary>
        /// <param name="leaseContractEngine">租赁合同发动机DTO。</param>
        [Delete(typeof(LeaseContractEngineDTO))]
        public void DeleteLeaseContractEngine(LeaseContractEngineDTO leaseContractEngine)
        {
            var newLeaseContractEngine = _contractEngineRepository.GetFiltered(t => t.Id == leaseContractEngine.LeaseContractEngineId).FirstOrDefault();
            //获取需要删除的对象。
            _contractEngineRepository.Remove(newLeaseContractEngine); //删除租赁合同发动机。
        }

        #endregion
    }
}
