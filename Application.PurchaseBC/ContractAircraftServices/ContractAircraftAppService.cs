#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/4 10:38:51
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
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.ContractAircraftQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.PlanAircraftAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.ContractAircraftServices
{
    [LogAOP]
    public class ContractAircraftAppService : ContextBoundObject, IContractAircraftAppService
    {
        private readonly IContractAircraftQuery _contractAircraftQuery;
        private readonly IContractAircraftRepository _contractAircraftRepository;
        private readonly IPlanAircraftRepository _planAircraftRepository;
        public ContractAircraftAppService(IContractAircraftQuery contractAircraftQuery,
            IContractAircraftRepository contractAircraftRepository,
            IPlanAircraftRepository planAircraftRepository)
        {
            _contractAircraftQuery = contractAircraftQuery;
            _contractAircraftRepository = contractAircraftRepository;
            _planAircraftRepository = planAircraftRepository;
        }

        #region ContractAircraftDTO

        /// <summary>
        ///     获取所有合同飞机
        /// </summary>
        /// <returns></returns>
        public IQueryable<ContractAircraftDTO> GetContractAircrafts()
        {
            var query =
                new QueryBuilder<ContractAircraft>();
            return _contractAircraftQuery.ContractAircraftDTOQuery(query);
        }

        /// <summary>
        ///     更新合同飞机。
        /// </summary>
        /// <param name="contractAircraft">租赁合同飞机DTO。</param>
        [Update(typeof(ContractAircraftDTO))]
        public void ModifyContractAircraft(ContractAircraftDTO contractAircraft)
        {
            var planAircraft = _planAircraftRepository.GetFiltered(p => p.Id == contractAircraft.PlanAircraftID).FirstOrDefault();
            var updateContractAircraft = _contractAircraftRepository
                .GetFiltered(t => t.ContractNumber == contractAircraft.ContractNumber && t.RankNumber == contractAircraft.RankNumber).FirstOrDefault();
            //获取需要更新的对象。
            if (updateContractAircraft != null)
            {
                if (planAircraft != null)
                    updateContractAircraft.SetPlanAircraft(planAircraft);
                else 
                    updateContractAircraft.RemovePlanAircraft();
            }
            _contractAircraftRepository.Modify(updateContractAircraft);
        }

        #endregion

    }
}
