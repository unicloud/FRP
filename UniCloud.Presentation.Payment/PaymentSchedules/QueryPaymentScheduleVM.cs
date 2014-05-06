#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/18，17:12
// 文件名：QueryPaymentScheduleVM.cs
// 程序集：UniCloud.Presentation.Payment
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.PaymentSchedules
{
    [Export(typeof(QueryPaymentScheduleVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QueryPaymentScheduleVM : EditViewModelBase
    {
        private readonly PaymentData _context;
        private readonly IPaymentService _service;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public QueryPaymentScheduleVM(IPaymentService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialContractAircraft(); // 初始化合同飞机信息。
            InitialAcPaymentSchedule(); //初始化飞机付款计划
            InitialStandardOrder(); //初始化标准订单
            InitialStandardPaymentSchedule(); //初始化标准付款计划
            InitialContractEngine(); //初始化合同发动机
            InitialEnginePaymentSchedule(); //初始化发动机付款计划
            InitialMaintainPaymentSchedule();
            CurrencysView = _service.CreateCollection(_context.Currencies);
            Suppliers = _service.CreateCollection(_context.Suppliers);
        }

        public QueryableDataServiceCollectionView<CurrencyDTO> CurrencysView { get; set; }
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }

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
                    if (value != null)
                    {
                        LoadAcPaymentScheduleByContractAcId();
                    }
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
            ContractAircraftsView = _service.CreateCollection(_context.ContractAircrafts);
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

        private FilterDescriptor _paymnetFilterOperator; //付款计划查询

        private AcPaymentScheduleDTO _selectedAcPaymentSchedule;

        /// <summary>
        ///     选择飞机付款计划
        /// </summary>
        public AcPaymentScheduleDTO SelectedAcPaymentSchedule
        {
            get { return _selectedAcPaymentSchedule; }
            set
            {
                _selectedAcPaymentSchedule = value;
                RaisePropertyChanged(() => SelectedAcPaymentSchedule);
            }
        }

        /// <summary>
        ///     获取所有付款计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<AcPaymentScheduleDTO> AcPaymentSchedulesView { get; set; }

        /// <summary>
        ///     初始化付款计划信息。
        /// </summary>
        private void InitialAcPaymentSchedule()
        {
            AcPaymentSchedulesView = _service.CreateCollection(_context.AcPaymentSchedules);
            _paymnetFilterOperator = new FilterDescriptor("ContractAcId", FilterOperator.IsEqualTo, 0);
            AcPaymentSchedulesView.FilterDescriptors.Add(_paymnetFilterOperator);
            AcPaymentSchedulesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedAcPaymentSchedule = e.Entities.Cast<AcPaymentScheduleDTO>().FirstOrDefault();
                RefreshCommandState(); //刷新按钮状态
            };
        }

        /// <summary>
        ///     根据合同飞机Id查询付款计划
        /// </summary>
        private void LoadAcPaymentScheduleByContractAcId()
        {
            _paymnetFilterOperator.Value = SelectedContractAircraft.ContractAircrafId;
            if (!AcPaymentSchedulesView.AutoLoad)
            {
                AcPaymentSchedulesView.AutoLoad = true;
            }
            else
            {
                AcPaymentSchedulesView.Load(true);
            }
        }

        #endregion

        #region 加载合同发动机

        private ContractEngineDTO _selectedContractEngine;

        /// <summary>
        ///     选择合同发动机。
        /// </summary>
        public ContractEngineDTO SelectedContractEngine
        {
            get { return _selectedContractEngine; }
            set
            {
                if (_selectedContractEngine != value)
                {
                    _selectedContractEngine = value;
                    if (value != null)
                    {
                        LoadEnginePaymentScheduleByContractEngineId();
                    }
                    RaisePropertyChanged(() => SelectedContractEngine);
                }
            }
        }

        /// <summary>
        ///     获取所有合同发动机信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ContractEngineDTO> ContractEnginesView { get; set; }

        /// <summary>
        ///     初始化合同发动机信息。
        /// </summary>
        private void InitialContractEngine()
        {
            ContractEnginesView = _service.CreateCollection(_context.ContractEngines);
            ContractEnginesView.PageSize = 20;
            ContractEnginesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedContractEngine == null)
                {
                    SelectedContractEngine = e.Entities.Cast<ContractEngineDTO>().FirstOrDefault();
                }
            };
        }

        #endregion

        #region 加载合同发动机下的付款计划

        private FilterDescriptor _enginePaymnetFilterOperator; //付款计划查询

        private EnginePaymentScheduleDTO _selectedEnginePaymentSchedule;

        /// <summary>
        ///     选择发动机付款计划
        /// </summary>
        public EnginePaymentScheduleDTO SelectedEnginePaymentSchedule
        {
            get { return _selectedEnginePaymentSchedule; }
            set
            {
                _selectedEnginePaymentSchedule = value;
                RaisePropertyChanged(() => SelectedEnginePaymentSchedule);
            }
        }

        /// <summary>
        ///     获取所有付款计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePaymentScheduleDTO> EnginePaymentSchedulesView { get; set; }

        /// <summary>
        ///     初始化付款计划信息。
        /// </summary>
        private void InitialEnginePaymentSchedule()
        {
            EnginePaymentSchedulesView = _service.CreateCollection(_context.EnginePaymentSchedules);
            _enginePaymnetFilterOperator = new FilterDescriptor("ContractEngineId", FilterOperator.IsEqualTo, 0);
            EnginePaymentSchedulesView.FilterDescriptors.Add(_enginePaymnetFilterOperator);
            EnginePaymentSchedulesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedEnginePaymentSchedule = e.Entities.Cast<EnginePaymentScheduleDTO>().FirstOrDefault();
                RefreshCommandState(); //刷新按钮状态
            };
        }

        /// <summary>
        ///     根据合同发动机Id查询付款计划
        /// </summary>
        private void LoadEnginePaymentScheduleByContractEngineId()
        {
            _enginePaymnetFilterOperator.Value = SelectedContractEngine.ContractEngineId;
            if (!EnginePaymentSchedulesView.AutoLoad)
            {
                EnginePaymentSchedulesView.AutoLoad = true;
            }
            else
            {
                EnginePaymentSchedulesView.Load(true);
            }
        }

        #endregion

        #region 加载标准订单

        private StandardOrderDTO _selectedStandardOrder;

        /// <summary>
        ///     选择标准订单。
        /// </summary>
        public StandardOrderDTO SelectedStandardOrder
        {
            get { return _selectedStandardOrder; }
            set
            {
                if (_selectedStandardOrder != value)
                {
                    _selectedStandardOrder = value;
                    if (value != null)
                    {
                        LoadStandardPaymentScheduleByOrderId();
                    }
                    RaisePropertyChanged(() => SelectedStandardOrder);
                }
            }
        }

        /// <summary>
        ///     获取所有标准订单信息。
        /// </summary>
        public QueryableDataServiceCollectionView<StandardOrderDTO> StandardOrdersView { get; set; }

        /// <summary>
        ///     初始化标准订单信息。
        /// </summary>
        private void InitialStandardOrder()
        {
            StandardOrdersView = _service.CreateCollection(_context.StandardOrders);
            StandardOrdersView.PageSize = 20;
            StandardOrdersView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedStandardOrder == null)
                {
                    SelectedStandardOrder = e.Entities.Cast<StandardOrderDTO>().FirstOrDefault();
                }
            };
        }

        #endregion

        #region 加载标准订单下的付款计划

        private StandardPaymentScheduleDTO _selectedStandardPaymentSchedule;
        private FilterDescriptor _standardpaymnetFilterOperator; //付款计划查询

        /// <summary>
        ///     选择标准付款计划
        /// </summary>
        public StandardPaymentScheduleDTO SelectedStandardPaymentSchedule
        {
            get { return _selectedStandardPaymentSchedule; }
            set
            {
                _selectedStandardPaymentSchedule = value;
                RaisePropertyChanged(() => SelectedStandardPaymentSchedule);
            }
        }

        /// <summary>
        ///     获取所有付款计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<StandardPaymentScheduleDTO> StandardPaymentSchedulesView { get; set; }

        /// <summary>
        ///     初始化付款计划信息。
        /// </summary>
        private void InitialStandardPaymentSchedule()
        {
            StandardPaymentSchedulesView = _service.CreateCollection(_context.StandardPaymentSchedules);
            _standardpaymnetFilterOperator = new FilterDescriptor("OrderId", FilterOperator.IsEqualTo, 0);
            StandardPaymentSchedulesView.FilterDescriptors.Add(_standardpaymnetFilterOperator);
            StandardPaymentSchedulesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedStandardPaymentSchedule = e.Entities.Cast<StandardPaymentScheduleDTO>().FirstOrDefault();
            };
        }

        /// <summary>
        ///     根据标准订单Id查询付款计划
        /// </summary>
        private void LoadStandardPaymentScheduleByOrderId()
        {
            _standardpaymnetFilterOperator.Value = SelectedStandardOrder.StandardOrderId;
            if (!StandardPaymentSchedulesView.AutoLoad)
            {
                StandardPaymentSchedulesView.AutoLoad = true;
            }
            else
            {
                StandardPaymentSchedulesView.Load(true);
            }
        }

        #endregion

        #region 加载维修付款计划

        private MaintainPaymentScheduleDTO _selectMaintainPaymentSchedule;

        /// <summary>
        ///     选择维修付款计划
        /// </summary>
        public MaintainPaymentScheduleDTO SelectMaintainPaymentSchedule
        {
            get { return _selectMaintainPaymentSchedule; }
            set
            {
                _selectMaintainPaymentSchedule = value;
                RaisePropertyChanged(() => SelectMaintainPaymentSchedule);
            }
        }

        /// <summary>
        ///     获取所有付款计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<MaintainPaymentScheduleDTO> MaintainPaymentSchedules { get; set; }

        /// <summary>
        ///     初始化付款计划信息。
        /// </summary>
        private void InitialMaintainPaymentSchedule()
        {
            MaintainPaymentSchedules = _service.CreateCollection(_context.MaintainPaymentSchedules);
            MaintainPaymentSchedules.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectMaintainPaymentSchedule = e.Entities.Cast<MaintainPaymentScheduleDTO>().FirstOrDefault();
            };
        }

        #endregion

        #region 重载基类服务

        public override void LoadData()
        {
            CurrencysView.AutoLoad = true;
            Suppliers.Load(true);
            //标准订单加载
            if (!StandardOrdersView.AutoLoad)
                StandardOrdersView.AutoLoad = true;
            //合同飞机加载
            if (!ContractAircraftsView.AutoLoad)
                ContractAircraftsView.AutoLoad = true;
            //合同发动机加载
            if (!ContractEnginesView.AutoLoad)
                ContractEnginesView.AutoLoad = true;
            if (!MaintainPaymentSchedules.AutoLoad)
                MaintainPaymentSchedules.AutoLoad = true;
        }

        #endregion
    }
}