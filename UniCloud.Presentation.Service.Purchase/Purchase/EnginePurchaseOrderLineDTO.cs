#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/07，12:02
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
using UniCloud.Presentation.Service.Purchase.Purchase.Enums;

#endregion

namespace UniCloud.Presentation.Service.Purchase.Purchase
{
    public partial class EnginePurchaseOrderLineDTO
    {
        #region 属性

        /// <summary>
        ///     合同发动机状态
        /// </summary>
        public ContractEngineStatus ContractEngineStatus
        {
            get { return (ContractEngineStatus) Status; }
        }

        #endregion

        #region 操作

        partial void OnSerialNumberChanging(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("序列号不能为空！");
            }
        }

        #endregion
    }
}