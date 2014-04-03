#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：BasicConfigGroupFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg
{
    /// <summary>
    ///     BasicConfigGroup工厂。
    /// </summary>
    public static class BasicConfigGroupFactory
    {
        /// <summary>
        ///     创建BasicConfigGroup。
        /// </summary>
        /// <returns>BasicConfigGroup</returns>
        public static BasicConfigGroup CreateBasicConfigGroup()
        {
            var basicConfigGroup = new BasicConfigGroup();
            basicConfigGroup.GenerateNewIdentity();
            return basicConfigGroup;
        }

        /// <summary>
        ///     创建基本构型组
        /// </summary>
        /// <param name="aircraftType">机型</param>
        /// <param name="description">描述</param>
        /// <param name="groupNo">基本构型组号</param>
        /// <returns></returns>
        public static BasicConfigGroup CreateBasicConfigGroup(AircraftType aircraftType, string description,
            string groupNo)
        {
            var basicConfigGroup = new BasicConfigGroup();
            basicConfigGroup.GenerateNewIdentity();
            basicConfigGroup.SetAircraftType(aircraftType);
            basicConfigGroup.SetDescription(description);
            basicConfigGroup.SetGroupNo(groupNo);
            return basicConfigGroup;
        }
    }
}