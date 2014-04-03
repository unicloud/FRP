#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：BasicConfigFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.BasicConfigAgg
{
    /// <summary>
    /// BasicConfig工厂。
    /// </summary>
    public static class BasicConfigFactory
    {
        /// <summary>
        /// 创建BasicConfig。
        /// </summary>
        ///  <returns>BasicConfig</returns>
        public static BasicConfig CreateBasicConfig()
        {
            var basicConfig = new BasicConfig
            {
            };
            basicConfig.GenerateNewIdentity();
            return basicConfig;

        }

        /// <summary>
        /// 创建基本构型
        /// </summary>
        /// <param name="description">描述</param>
        /// <param name="itemNo">项号</param>
        /// <param name="parentId">父项Id</param>
        /// <param name="parentItemNo">父项项号</param>
        /// <param name="ts">Ts</param>
        /// <param name="bcGroupId">基本构型组</param>
        /// <returns></returns>
        public static BasicConfig CreateBasicConfig(string description, string itemNo, int? parentId, string parentItemNo,
            TechnicalSolution ts,int bcGroupId)
        {
            var basicConfig = new BasicConfig
            {
            };
            basicConfig.GenerateNewIdentity();
            basicConfig.SetDescription(description);
            basicConfig.SetItemNo(itemNo);
            basicConfig.SetParentAcConfigId(parentId);
            basicConfig.SetParentItemNo(parentItemNo);
            basicConfig.SetTechnicalSolution(ts);
            basicConfig.BasicConfigGroupId = bcGroupId;
            return basicConfig;
        }
    }
}
