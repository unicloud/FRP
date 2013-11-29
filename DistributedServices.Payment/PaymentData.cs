//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.DistributedServices.Payment
{
    using Application.PaymentBC.Services;
    using InstanceProviders;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// 应付款模块数据类
    /// </summary>
    public class PaymentData
    {
        private readonly IPaymentAppService _flightLogAppService = Container.Current.Resolve<IPaymentAppService>();

    }
}