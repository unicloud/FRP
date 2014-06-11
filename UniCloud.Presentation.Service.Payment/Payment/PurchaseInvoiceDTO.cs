#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/17 18:32:40
// 文件名：PurchaseInvoiceDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

#endregion

namespace UniCloud.Presentation.Service.Payment.Payment
{
    public partial class PurchaseInvoiceDTO
    {
        partial void OnOperatorNameChanging(string value)
        {
            if (value.Trim().Length==0)
            {
                throw new Exception("经办人不能为空");
            }
        }

        //partial void OnPaidAmountChanging(decimal value)
        //{
        //    if (value == 0)
        //    {
        //        throw new Exception("已付金额不能为空！");
        //    }
        //}

    }
}
