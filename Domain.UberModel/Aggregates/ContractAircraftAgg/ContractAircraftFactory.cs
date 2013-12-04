#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/21，12:11
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ContractAircraftAgg
{
    /// <summary>
    ///     合同飞机工厂
    /// </summary>
    public static class ContractAircraftFactory
    {
        /// <summary>
        ///     创建租赁合同飞机
        /// </summary>
        /// <param name="contractName">合同名称</param>
        /// <param name="rankNumber">RANK号</param>
        /// <returns>租赁合同飞机</returns>
        public static LeaseContractAircraft CreateLeaseContractAircraft(string contractName, string rankNumber)
        {
            var leaseContractAircraft = new LeaseContractAircraft
            {
                ContractName = contractName,
                RankNumber = rankNumber
            };

            return leaseContractAircraft;
        }

        /// <summary>
        ///     创建购买合同飞机
        /// </summary>
        /// <param name="contractName">合同名称</param>
        /// <param name="rankNumber">RANK号</param>
        /// <returns>购买合同飞机</returns>
        public static PurchaseContractAircraft CreatePurchaseContractAircraft(string contractName, string rankNumber)
        {
            var purchaseContractAircraft = new PurchaseContractAircraft
            {
                ContractName = contractName,
                RankNumber = rankNumber
            };

            return purchaseContractAircraft;
        }
    }
}