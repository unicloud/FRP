﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/02，21:31
// 方案：FRP
// 项目：Domain.ProjectBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

namespace UniCloud.Domain.ProjectBC.Aggregates.WorkGroupAgg
{
    /// <summary>
    ///     工作组工厂
    /// </summary>
    public static class WorkGroupFactory
    {
        /// <summary>
        ///     创建工作组
        /// </summary>
        /// <param name="name">工作组名称</param>
        /// <returns>工作组</returns>
        public static WorkGroup CreateWorkGroup(string name)
        {
            var workGroup = new WorkGroup
            {
                Name = name
            };
            workGroup.GenerateNewIdentity();

            return workGroup;
        }
    }
}