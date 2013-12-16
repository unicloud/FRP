//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.ContractAircraftServices;
using UniCloud.Application.PaymentBC.ContractEngineServices;
using UniCloud.Application.PaymentBC.CurrencyServices;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.Payment
{
    /// <summary>
    ///     应付款模块数据类
    /// </summary>
    public class PaymentData : ExposeData.ExposeData
    {
        private readonly IContractAircraftAppService _contractAircraftAppService;
        private readonly IContractEngineAppService _contractEngineAppService;
        private readonly ICurrencyAppService _currencyAppService;

        public PaymentData()
            : base("UniCloud.Application.PaymentBC.DTO")
        {
            _currencyAppService = DefaultContainer.Resolve<ICurrencyAppService>();
            _contractAircraftAppService = DefaultContainer.Resolve<IContractAircraftAppService>();
            _contractEngineAppService = DefaultContainer.Resolve<IContractEngineAppService>();
        }

        public IQueryable<MaintainInvoiceDTO> MaintainInvoices
        {
            get { return null; }
        }

        #region 合同飞机

        public IQueryable<ContractAircraftDTO> ContractAircrafts
        {
            get { return _contractAircraftAppService.GetContractAircrafts(); }
        }

        #endregion

        #region 币种

        /// <summary>
        ///     币种集合
        /// </summary>
        public IQueryable<CurrencyDTO> Currencies
        {
            get { return _currencyAppService.GetCurrencies(); }
        }

        #endregion

        #region 合同发动机

        /// <summary>
        ///     合同发动机集合
        /// </summary>
        public IQueryable<ContractEngineDTO> ContractEngines
        {
            get { return _contractEngineAppService.GetContractEngines(); }
        }

        #endregion
    }
}