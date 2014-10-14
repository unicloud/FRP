#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/15，11:33
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ActionCategoryAgg
{
    /// <summary>
    ///     活动方式工厂
    /// </summary>
    public static class ActionCategoryFactory
    {
        /// <summary>
        ///     创建活动类型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="type">活动类型</param>
        /// <param name="name">活动名称</param>
        /// <param name="needRequest">是否需要申请</param>
        /// <returns></returns>
        public static ActionCategory CreateActionCategory(Guid id, string type, string name,bool needRequest)
        {
            var actionCategory = new ActionCategory
            {
                ActionType = type,
                ActionName = name,
                NeedRequest = needRequest,
            };
            actionCategory.ChangeCurrentIdentity(id);

            return actionCategory;
        }
    }
}