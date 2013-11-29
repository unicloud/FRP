#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，10:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.ContractEngineAgg
{
    /// <summary>
    ///     合同发动机工厂
    /// </summary>
    public static class ContractEngineFactory
    {
        /// <summary>
        ///     创建租赁合同发动机
        /// </summary>
        /// <param name="contractName">合同名称</param>
        /// <param name="rankNumber">RANK号</param>
        /// <returns>租赁合同发动机</returns>
        public static LeaseContractEngine CreateLeaseContractEngine(string contractName, string rankNumber)
        {
            var leaseContractEngine = new LeaseContractEngine
            {
                ContractName = contractName,
                RankNumber = rankNumber
            };

            leaseContractEngine.GenerateNewIdentity();

            return leaseContractEngine;
        }

        /// <summary>
        ///     创建购买合同发动机
        /// </summary>
        /// <param name="contractName">合同名称</param>
        /// <param name="rankNumber">RANK号</param>
        /// <returns>购买合同发动机</returns>
        public static PurchaseContractEngine CreatePurchaseContractEngine(string contractName, string rankNumber)
        {
            var purchaseContractEngine = new PurchaseContractEngine
            {
                ContractName = contractName,
                RankNumber = rankNumber
            };

            purchaseContractEngine.GenerateNewIdentity();

            return purchaseContractEngine;
        }
    }
}