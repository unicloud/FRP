#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/3 11:21:19
// 文件名：PurchaseContractAircraftAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.ContractAircraftQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.ContractAircraftServices
{
    /// <summary>
    ///     实现采购合同飞机服务接口。
    ///     用于处理采购合同飞机相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class PurchaseContractAircraftAppService : IPurchaseContractAircraftAppService
    {
        private readonly IPurchaseContractAircraftQuery _purchaseContractAircraftQuery;
        private readonly IContractAircraftRepository _contractAircraftRepository;

        public PurchaseContractAircraftAppService(IPurchaseContractAircraftQuery purchaseContractAircraftQuery,
            IContractAircraftRepository contractAircraftRepository)
        {
            _purchaseContractAircraftQuery = purchaseContractAircraftQuery;
            _contractAircraftRepository = contractAircraftRepository;
        }

        #region PurchaseContractAircraftDTO

        /// <summary>
        ///     获取所有采购合同飞机
        /// </summary>
        /// <returns></returns>
        public IQueryable<PurchaseContractAircraftDTO> GetPurchaseContractAircrafts()
        {
            var queryBuilder =
                new QueryBuilder<PurchaseContractAircraft>();
            return _purchaseContractAircraftQuery.PurchaseContractAircraftDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增采购合同飞机。
        /// </summary>
        /// <param name="purchaseContractAircraft">采购合同飞机DTO。</param>
        [Insert(typeof(PurchaseContractAircraftDTO))]
        public void InsertPurchaseContractAircraft(PurchaseContractAircraftDTO purchaseContractAircraft)
        {
            var newPurchaseContractAircraft = ContractAircraftFactory.CreatePurchaseContractAircraft("", "");
            _contractAircraftRepository.Add(newPurchaseContractAircraft);
        }

        /// <summary>
        ///     更新采购合同飞机。
        /// </summary>
        /// <param name="purchaseContractAircraft">采购合同飞机DTO。</param>
        [Update(typeof(PurchaseContractAircraftDTO))]
        public void ModifyPurchaseContractAircraft(PurchaseContractAircraftDTO purchaseContractAircraft)
        {

            var updatePurchaseContractAircraft = _contractAircraftRepository.GetFiltered(t => t.Id == purchaseContractAircraft.PurchaseContractAircraftId).FirstOrDefault();
            //获取需要更新的对象。
            if (updatePurchaseContractAircraft != null)
            {
            }
            _contractAircraftRepository.Modify(updatePurchaseContractAircraft);
        }

        /// <summary>
        ///     删除采购合同飞机。
        /// </summary>
        /// <param name="purchaseContractAircraft">采购合同飞机DTO。</param>
        [Delete(typeof(PurchaseContractAircraftDTO))]
        public void DeletePurchaseContractAircraft(PurchaseContractAircraftDTO purchaseContractAircraft)
        {
            var newPurchaseContractAircraft = _contractAircraftRepository.GetFiltered(t => t.Id == purchaseContractAircraft.PurchaseContractAircraftId).FirstOrDefault();
            //获取需要删除的对象。
            _contractAircraftRepository.Remove(newPurchaseContractAircraft); //删除采购合同飞机。
        }

        #endregion
    }
}
