﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，11:01
// 方案：FRP
// 项目：Domain.Common
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.Common.Enums
{
    /// <summary>
    ///     任务依赖类型
    /// </summary>
    public enum DependencyType
    {
        完成开始 = 0,
        开始开始 = 1,
        完成完成 = 2,
        开始完成 = 3
    }
}