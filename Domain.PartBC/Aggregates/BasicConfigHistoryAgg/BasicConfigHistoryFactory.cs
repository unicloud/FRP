#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 14:02:40
// 文件名：BasicConfigHistoryFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.BasicConfigHistoryAgg
{
    /// <summary>
    ///     BasicConfigHistory工厂。
    /// </summary>
    public static class BasicConfigHistoryFactory
    {
        /// <summary>
        ///     创建BasicConfigHistory。
        /// </summary>
        /// <returns>BasicConfigHistory</returns>
        public static BasicConfigHistory CreateBasicConfigHistory()
        {
            var basicConfigHistory = new BasicConfigHistory();
            basicConfigHistory.GenerateNewIdentity();
            return basicConfigHistory;
        }

        /// <summary>
        ///     创建基本构型历史
        /// </summary>
        /// <param name="contractAircraft">合同飞机</param>
        /// <param name="basicConfigGroup">基本构型组</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static BasicConfigHistory CreateBasicConfigHistory(ContractAircraft contractAircraft,
            BasicConfigGroup basicConfigGroup, DateTime startDate, DateTime? endDate)
        {
            var basicConfigHistory = new BasicConfigHistory();
            basicConfigHistory.GenerateNewIdentity();
            basicConfigHistory.SetContractAircraft(contractAircraft);
            basicConfigHistory.SetBasicConfigGroup(basicConfigGroup);
            basicConfigHistory.SetStartDate(startDate);
            basicConfigHistory.SetEndDate(endDate);
            return basicConfigHistory;
        }
    }
}