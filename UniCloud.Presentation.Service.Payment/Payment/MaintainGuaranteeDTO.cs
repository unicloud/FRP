#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/27，13:12
// 文件名：MaintainGuaranteeDTO.cs
// 程序集：UniCloud.Presentation.Service.Payment
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Presentation.Service.Payment.Payment
{
    public partial class MaintainGuaranteeDTO
    {
        partial void OnAmountChanging(decimal value)
        {
            if (value == 0)
            {
                throw new Exception("付款金额不能为空");
            }
        }

        partial void OnMaintainContractIdChanging(int value)
        {
            if (value == 0)
            {
                throw new Exception("维修合同不能为空");
            }
        }

        partial void OnCurrencyIdChanging(int value)
        {
            if (value == 0)
            {
                throw new Exception("币种不能为空");
            }
        }
    }


}
