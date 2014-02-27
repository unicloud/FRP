#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SpecialConfigFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg
{
    /// <summary>
    /// SpecialConfig工厂。
    /// </summary>
    public static class SpecialConfigFactory
    {
        /// <summary>
        /// 创建SpecialConfig。
        /// </summary>
        ///  <returns>SpecialConfig</returns>
        public static SpecialConfig CreateSpecialConfig()
        {
            var specialConfig = new SpecialConfig
            {
                CreateDate = DateTime.Now,
            };
            specialConfig.GenerateNewIdentity();
            return specialConfig;
        }

        /// <summary>
        /// 创建特定选型
        /// </summary>
        /// <param name="contractAircraft">合同飞机</param>
        /// <param name="description">描述</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="itemNo">项号</param>
        /// <param name="parentId">父项Id</param>
        /// <param name="parentItemNo">父项项号</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="ts">技术解决方案</param>
        /// <returns></returns>
        public static SpecialConfig CreateSpecialConfig(ContractAircraft contractAircraft,string description,
            DateTime endDate, bool isValid, string itemNo, int? parentId, string parentItemNo,
            DateTime startDate,TechnicalSolution ts)
        {
            var specialConfig = new SpecialConfig
            {
                CreateDate = DateTime.Now,
            };
            specialConfig.GenerateNewIdentity();
            specialConfig.SetContractAircraf(contractAircraft);
            specialConfig.SetDescription(description);
            specialConfig.SetEndDate(endDate);
            specialConfig.SetIsValid(isValid);
            specialConfig.SetItemNo(itemNo);
            specialConfig.SetParentAcConfigId(parentId);
            specialConfig.SetParentItemNo(parentItemNo);
            specialConfig.SetStartDate(startDate);
            specialConfig.SetTechnicalSolution(ts);
            return specialConfig;
        }
    }
}
