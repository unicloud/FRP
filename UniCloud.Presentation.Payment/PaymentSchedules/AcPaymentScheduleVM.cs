#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.PaymentSchedules
{
    [Export(typeof (AcPaymentScheduleVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AcPaymentScheduleVM : EditViewModelBase
    {
        private PaymentData _context;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public AcPaymentScheduleVM()
        {
            InitialContractAircraft(); // 初始化合同飞机信息。
        }

        #region 加载合同飞机

        private ContractAircraftDTO _selectedContractAircraft;

        /// <summary>
        ///     选择合同飞机。
        /// </summary>
        public ContractAircraftDTO SelectedContractAircraft
        {
            get { return _selectedContractAircraft; }
            set
            {
                if (_selectedContractAircraft != value)
                {
                    _selectedContractAircraft = value;
                    RaisePropertyChanged(() => SelectedContractAircraft);
                }
            }
        }


        /// <summary>
        ///     获取所有合同飞机信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ContractAircraftDTO> ContractAircraftsView { get; set; }

        /// <summary>
        ///     初始化合同飞机信息。
        /// </summary>
        private void InitialContractAircraft()
        {
            ContractAircraftsView = Service.CreateCollection(_context.ContractAircrafts);
            ContractAircraftsView.PageSize = 20;
            ContractAircraftsView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    if (SelectedContractAircraft == null)
                    {
                        SelectedContractAircraft = e.Entities.Cast<ContractAircraftDTO>().FirstOrDefault();
                    }
                };
        }

        #endregion

        #region 加载合同飞机下的付款计划

        private AcPaymentScheduleDTO _selectedAcPaymentSchedule;

        /// <summary>
        ///     选择付款计划
        /// </summary>
        public AcPaymentScheduleDTO SelectedAcPaymentSchedule
        {
            get { return _selectedAcPaymentSchedule; }
            set
            {
                if (_selectedAcPaymentSchedule != value)
                {
                    _selectedAcPaymentSchedule = value;
                    RaisePropertyChanged(() => SelectedAcPaymentSchedule);
                }
            }
        }


        /// <summary>
        ///     获取所有合同飞机信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ContractAircraftDTO> ContractAircraftsView { get; set; }

        /// <summary>
        ///     初始化合同飞机信息。
        /// </summary>
        private void InitialContractAircraft()
        {
            ContractAircraftsView = Service.CreateCollection(_context.ContractAircrafts);
            ContractAircraftsView.PageSize = 20;
            ContractAircraftsView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedContractAircraft == null)
                {
                    SelectedContractAircraft = e.Entities.Cast<ContractAircraftDTO>().FirstOrDefault();
                }
            };
        }


        #endregion

        #region 重载基类服务

        protected override IService CreateService()
        {
            _context = new PaymentData(AgentHelper.PaymentUri);
            return new PaymentService(_context);
        }

        public override void LoadData()
        {
            ContractAircraftsView.AutoLoad = true;
        }

        #endregion
    }
}