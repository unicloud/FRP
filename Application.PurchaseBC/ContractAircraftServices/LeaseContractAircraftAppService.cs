#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/3 11:20:55
// 文件名：LeaseContractAircraftAppService
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
    ///     实现租赁合同飞机服务接口。
    ///     用于处理租赁合同飞机相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class LeaseContractAircraftAppService : ILeaseContractAircraftAppService
    {
        private readonly ILeaseContractAircraftQuery _leaseContractAircraftQuery;
        private readonly IContractAircraftRepository _contractAircraftRepository;

        public LeaseContractAircraftAppService(ILeaseContractAircraftQuery leaseContractAircraftQuery,
            IContractAircraftRepository contractAircraftRepository)
        {
            _leaseContractAircraftQuery = leaseContractAircraftQuery;
            _contractAircraftRepository = contractAircraftRepository;
        }

        #region LeaseContractAircraftDTO

        /// <summary>
        ///     获取所有租赁合同飞机
        /// </summary>
        /// <returns></returns>
        public IQueryable<LeaseContractAircraftDTO> GetLeaseContractAircrafts()
        {
            var queryBuilder =
                new QueryBuilder<LeaseContractAircraft>();
            return _leaseContractAircraftQuery.LeaseContractAircraftDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增租赁合同飞机。
        /// </summary>
        /// <param name="leaseContractAircraft">租赁合同飞机DTO。</param>
        [Insert(typeof(LeaseContractAircraftDTO))]
        public void InsertLeaseContractAircraft(LeaseContractAircraftDTO leaseContractAircraft)
        {
            var newLeaseContractAircraft = ContractAircraftFactory.CreateLeaseContractAircraft("", "");
            _contractAircraftRepository.Add(newLeaseContractAircraft);
        }

        /// <summary>
        ///     更新租赁合同飞机。
        /// </summary>
        /// <param name="leaseContractAircraft">租赁合同飞机DTO。</param>
        [Update(typeof(LeaseContractAircraftDTO))]
        public void ModifyLeaseContractAircraft(LeaseContractAircraftDTO leaseContractAircraft)
        {

            var updateLeaseContractAircraft = _contractAircraftRepository.GetFiltered(t => t.Id == leaseContractAircraft.LeaseContractAircraftId).FirstOrDefault();
            //获取需要更新的对象。
            if (updateLeaseContractAircraft != null)
            {
            }
            _contractAircraftRepository.Modify(updateLeaseContractAircraft);
        }

        /// <summary>
        ///     删除租赁合同飞机。
        /// </summary>
        /// <param name="leaseContractAircraft">租赁合同飞机DTO。</param>
        [Delete(typeof(LeaseContractAircraftDTO))]
        public void DeleteLeaseContractAircraft(LeaseContractAircraftDTO leaseContractAircraft)
        {
            var newLeaseContractAircraft = _contractAircraftRepository.GetFiltered(t => t.Id == leaseContractAircraft.LeaseContractAircraftId).FirstOrDefault();
            //获取需要删除的对象。
            _contractAircraftRepository.Remove(newLeaseContractAircraft); //删除租赁合同飞机。
        }

        #endregion
    }
}
