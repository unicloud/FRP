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
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.BasicConfigAgg
{
    /// <summary>
    ///     BasicConfig工厂。
    /// </summary>
    public static class BasicConfigFactory
    {
        /// <summary>
        ///     创建BasicConfig。
        /// </summary>
        /// <returns>BasicConfig</returns>
        public static BasicConfig CreateBasicConfig()
        {
            var basicConfig = new BasicConfig();
            basicConfig.GenerateNewIdentity();
            return basicConfig;
        }

        /// <summary>
        ///     创建基本构型
        /// </summary>
        /// <param name="position"></param>
        /// <param name="description"></param>
        /// <param name="item"></param>
        /// <param name="parentAcConfig"></param>
        /// <param name="bcGroupId"></param>
        /// <returns></returns>
        public static BasicConfig CreateBasicConfig(string position, string description, Item item,
            AcConfig parentAcConfig, int bcGroupId)
        {
            var basicConfig = new BasicConfig();
            basicConfig.GenerateNewIdentity();
            basicConfig.CreateDate = DateTime.Now;
            basicConfig.SetPosition(position);
            basicConfig.SetDescription(description);
            basicConfig.SetItem(item);
            basicConfig.SetParentItem(parentAcConfig);
            basicConfig.BasicConfigGroupId = bcGroupId;
            return basicConfig;
        }
    }
}