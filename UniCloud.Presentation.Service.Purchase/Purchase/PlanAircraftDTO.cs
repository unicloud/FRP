﻿#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：19:59
// 方案：FRP
// 项目：Service.Purchase
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Presentation.Service.Purchase.Purchase.Enums;

#endregion

namespace UniCloud.Presentation.Service.Purchase.Purchase
{
    public partial class PlanAircraftDTO
    {
        #region 属性

        /// <summary>
        ///     管理状态
        /// </summary>
        public ManageStatus ManageStatus
        {
            get { return (ManageStatus) Status; }
        }

        /// <summary>
        ///     是否匹配
        /// </summary>
        public bool IsMatched
        {
            get { return ContractAircraftId != null; }
        }

        #endregion
    }
}