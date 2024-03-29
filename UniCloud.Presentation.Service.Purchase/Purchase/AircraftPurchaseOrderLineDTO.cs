﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/17，11:23
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
    public partial class AircraftPurchaseOrderLineDTO
    {
        #region 属性

        /// <summary>
        ///     合同飞机状态
        /// </summary>
        public ContractAircraftStatus ContractAircraftStatus
        {
            get { return (ContractAircraftStatus) Status; }
        }

        /// <summary>
        ///     是否匹配
        /// </summary>
        public bool IsMatched
        {
            get { return PlanAircraftID != null; }
        }

        #endregion

        #region 操作

        partial void OnPlanAircraftIDChanged()
        {
            OnPropertyChanged("IsMatched");
        }

        #endregion
    }
}