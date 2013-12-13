//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;

namespace UniCloud.DistributedServices.Payment
{
    /// <summary>
    /// 应付款模块数据类
    /// </summary>
    public class PaymentData : ExposeData.ExposeData
    {
        //private readonly IPaymentAppService _flightLogAppService = Container.Current.Resolve<IPaymentAppService>();

        public PaymentData()
            : base("UniCloud.Application.PaymentBC.DTO")
        {
        }

        public IQueryable<MaintainInvoiceDTO> MaintainInvoices
        {
            get { return null; }
        }
    }
}