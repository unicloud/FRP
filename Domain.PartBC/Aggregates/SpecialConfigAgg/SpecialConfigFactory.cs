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
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg
{
    /// <summary>
    ///     SpecialConfig工厂。
    /// </summary>
    public static class SpecialConfigFactory
    {
        /// <summary>
        ///     创建SpecialConfig。
        /// </summary>
        /// <returns>SpecialConfig</returns>
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
        ///     创建特定选型
        /// </summary>
        /// <param name="position">位置信息</param>
        /// <param name="description">描述</param>
        /// <param name="item">附件项</param>
        /// <param name="parentAcConfig">父项构型</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="contractAircraft">合同飞机</param>
        /// <returns></returns>
        public static SpecialConfig CreateSpecialConfig(int position, string description, Item item,
            AcConfig parentAcConfig, DateTime startDate, DateTime? endDate, ContractAircraft contractAircraft)
        {
            var specialConfig = new SpecialConfig();
            specialConfig.GenerateNewIdentity();
            specialConfig.CreateDate = DateTime.Now;
            specialConfig.SetPosition((Position)position);
            specialConfig.SetDescription(description);
            specialConfig.SetItem(item);
            specialConfig.SetParentItem(parentAcConfig);
            specialConfig.SetContractAircraf(contractAircraft);
            specialConfig.SetStartDate(startDate);
            specialConfig.SetEndDate(endDate);
            return specialConfig;
        }
    }
}