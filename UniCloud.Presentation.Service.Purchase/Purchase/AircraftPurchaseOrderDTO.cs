﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/12，15:52
// 方案：FRP
// 项目：Service.Purchase
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using UniCloud.Presentation.Service.Purchase.Purchase.Enums;

#endregion

namespace UniCloud.Presentation.Service.Purchase.Purchase
{
    public partial class AircraftPurchaseOrderDTO
    {
        #region 属性

        /// <summary>
        ///     订单状态
        /// </summary>
        public OrderStatus OrderStatus
        {
            get { return (OrderStatus) Status; }
        }

        #endregion

        partial void OnNameChanging(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("合同名称不能为空！");
            }
        }

        partial void OnStatusChanged()
        {
            OnPropertyChanged("OrderStatus");
        }
    }
}