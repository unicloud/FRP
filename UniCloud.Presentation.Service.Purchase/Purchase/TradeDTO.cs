#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/22，21:02
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
    public partial class TradeDTO
    {
        /// <summary>
        ///     交易状态
        /// </summary>
        public TradeStatus TradeStatus
        {
            get { return (TradeStatus) Status; }
        }

        partial void OnNameChanging(string value)
        {
            if (value.Length < 3)
            {
                throw new Exception("长度不足");
            }
        }
    }
}