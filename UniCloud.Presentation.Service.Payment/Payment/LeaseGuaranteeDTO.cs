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

namespace UniCloud.Presentation.Service.Payment.Payment
{
    public partial class LeaseGuaranteeDTO
    {
        partial void OnAmountChanging(decimal value)
        {
            if (value==0)
            {
                throw new Exception("付款金额不能为空");
            }
        }
     
        partial void OnOrderIdChanging(int value)
        {
            if (value==0)
            {
                throw new Exception("租赁订单名称不能为空");
            }
        }

       
        partial void OnCurrencyIdChanging(int value)
        {
            if (value==0)
            {
                throw new Exception("币种不能为空");
            }
        }
    }
}
