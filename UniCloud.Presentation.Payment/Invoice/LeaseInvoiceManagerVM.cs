#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/12 9:42:33
// 文件名：LeaseInvoiceManagerVM
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof (LeaseInvoiceManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LeaseInvoiceManagerVM : EditViewModelBase
    {
        #region 声明、初始化
        private readonly IRegionManager _regionManager;
        private PaymentData _paymentData;

        [ImportingConstructor]
        public LeaseInvoiceManagerVM(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitializeVM();
            InitializerCommand();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            LeaseInvoices = Service.CreateCollection<LeaseInvoiceDTO>(_paymentData.LeaseInvoices);
            Service.RegisterCollectionView(LeaseInvoices); //注册查询集合。
            LeaseInvoices.PropertyChanged += OnViewPropertyChanged;

            Currencies = new QueryableDataServiceCollectionView<CurrencyDTO>(_paymentData, _paymentData.Currencies);

            Suppliers = new QueryableDataServiceCollectionView<SupplierDTO>(_paymentData, _paymentData.Suppliers);

            Orders = new QueryableDataServiceCollectionView<OrderDTO>(_paymentData, _paymentData.Orders);

            AircraftLeaseOrders = new QueryableDataServiceCollectionView<AircraftLeaseOrderDTO>(_paymentData, _paymentData.AircraftLeaseOrders);

            EngineLeaseOrders = new QueryableDataServiceCollectionView<EngineLeaseOrderDTO>(_paymentData, _paymentData.EngineLeaseOrders);

            PaymentSchedules = new QueryableDataServiceCollectionView<PaymentScheduleDTO>(_paymentData, _paymentData.PaymentSchedules);

            AcPaymentSchedules = Service.CreateCollection<AcPaymentScheduleDTO>(_paymentData.AcPaymentSchedules);
            Service.RegisterCollectionView(AcPaymentSchedules); //注册查询集合。
            AcPaymentSchedules.PropertyChanged += OnViewPropertyChanged;

            EnginePaymentSchedules = Service.CreateCollection<EnginePaymentScheduleDTO>(_paymentData.EnginePaymentSchedules);
            Service.RegisterCollectionView(EnginePaymentSchedules); //注册查询集合。
            EnginePaymentSchedules.PropertyChanged += OnViewPropertyChanged;

            ContractAircrafts = new QueryableDataServiceCollectionView<ContractAircraftDTO>(_paymentData, _paymentData.ContractAircrafts);

            ContractEngines = new QueryableDataServiceCollectionView<ContractEngineDTO>(_paymentData, _paymentData.ContractEngines);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            DeleteCommand = new DelegateCommand<object>(OnDelete, CanDelete);
            AddCommand = new DelegateCommand<object>(OnAdd, CanAdd);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            CommitCommand = new DelegateCommand<object>(OnCommitExecute, CanCommitExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelExecute, CanCancelExecute);
            CellEditEndCommand = new DelegateCommand<object>(OnCellEditEnd);
            SubmitCommand = new DelegateCommand<object>(OnSubmit, CanSubmit);
            CheckCommand = new DelegateCommand<object>(OnCheck,CanCheck);
        }

        /// <summary>
        ///     创建服务实例
        /// </summary>
        protected override IService CreateService()
        {
            _paymentData = new PaymentData(AgentHelper.PaymentUri);
            return new PaymentService(_paymentData);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 是否已提交审核

        private bool _isSubmited;

        /// <summary>
        ///  是否已提交审核
        /// </summary>
        public bool IsSubmited
        {
            get { return this._isSubmited; }
            private set
            {
                if (this._isSubmited != value)
                {
                    _isSubmited = value;
                    this.RaisePropertyChanged(() => this.IsSubmited);
                }
            }
        }

        #endregion
        #endregion

        #region 加载数据

        /// <summary>
        ///     加载数据方法
        ///     <remarks>
        ///         导航到此页面时调用。
        ///         可在此处将CollectionView的AutoLoad属性设为True，以实现数据的自动加载。
        ///     </remarks>
        /// </summary>
        public override void LoadData()
        {
            Currencies.Load(true);
            Suppliers.Load(true);
            LeaseInvoices.Load(true);
            Orders.Load(true);
            AircraftLeaseOrders.Load(true);
            EngineLeaseOrders.Load(true);
            PaymentSchedules.Load(true);
            AcPaymentSchedules.Load(true);
            EnginePaymentSchedules.Load(true);
            ContractAircrafts.Load(true);
            ContractEngines.Load(true);
        }

        #region 业务

        #region 租赁发票集合
        /// <summary>
        ///     租赁发票集合
        /// </summary>
        public QueryableDataServiceCollectionView<LeaseInvoiceDTO> LeaseInvoices { get; set; }

        #endregion

        #region 选择的租赁发票

        private LeaseInvoiceDTO _selLeaseInvoice;

        /// <summary>
        ///     选择的租赁发票
        /// </summary>
        public LeaseInvoiceDTO SelLeaseInvoice
        {
            get { return _selLeaseInvoice; }
            set
            {
                if (_selLeaseInvoice != value)
                {
                    _selLeaseInvoice = value;
                    _invoiceLines.Clear();
                    foreach (var invoiceLine in value.InvoiceLines)
                    {
                        InvoiceLines.Add(invoiceLine);
                    }
                    SelInvoiceLine = InvoiceLines.FirstOrDefault();
                    _relatedOrder.Clear();
                    RelatedOrder.Add(Orders.FirstOrDefault(p => p.Id == value.OrderId));
                    SelOrder = RelatedOrder.FirstOrDefault();
                    if (SelOrder != null)
                        RelatedOrderLine = SelOrder.OrderLines.FirstOrDefault(p => p.Id == SelInvoiceLine.OrderLineId);
                    _relatedPaymentSchedule.Clear();
                    RelatedPaymentSchedule.Add(
                        PaymentSchedules.FirstOrDefault(p =>
                        {
                            var paymentScheduleLine = p.PaymentScheduleLines.FirstOrDefault(l => l.PaymentScheduleLineId == value.PaymentScheduleLineId);
                            return paymentScheduleLine != null && paymentScheduleLine.PaymentScheduleLineId == value.PaymentScheduleLineId;
                        }));
                    SelPaymentSchedule = RelatedPaymentSchedule.FirstOrDefault();
                    RaisePropertyChanged(() => SelLeaseInvoice);
                }
            }
        }

        #endregion

        #region 关联的租赁订单及订单行

        private ObservableCollection<OrderDTO> _relatedOrder = new ObservableCollection<OrderDTO>();

        /// <summary>
        ///     关联的租赁订单
        /// </summary>
        public ObservableCollection<OrderDTO> RelatedOrder
        {
            get { return _relatedOrder; }
            set
            {
                if (_relatedOrder != value)
                {
                    _relatedOrder = value;
                    RaisePropertyChanged(() => RelatedOrder);
                }
            }
        }

        #endregion

        #region 关联的付款计划

        private ObservableCollection<PaymentScheduleDTO> _relatedPaymentSchedule = new ObservableCollection<PaymentScheduleDTO>();

        /// <summary>
        ///     关联的付款计划
        /// </summary>
        public ObservableCollection<PaymentScheduleDTO> RelatedPaymentSchedule
        {
            get { return _relatedPaymentSchedule; }
            set
            {
                if (_relatedPaymentSchedule != value)
                {
                    _relatedPaymentSchedule = value;
                    RaisePropertyChanged(() => SelLeaseInvoice);
                }
            }
        }

        #endregion

        #region 租赁发票行

        private ObservableCollection<InvoiceLineDTO> _invoiceLines = new ObservableCollection<InvoiceLineDTO>();

        /// <summary>
        ///     租赁发票行
        /// </summary>
        public ObservableCollection<InvoiceLineDTO> InvoiceLines
        {
            get { return _invoiceLines; }
            private set
            {
                if (_invoiceLines != value)
                {
                    _invoiceLines = value;
                    RaisePropertyChanged(() => InvoiceLines);
                }
            }
        }

        #endregion

        #region 选择的租赁发票行

        private InvoiceLineDTO _selInvoiceLine;

        /// <summary>
        ///     选择的租赁发票行
        /// </summary>
        public InvoiceLineDTO SelInvoiceLine
        {
            get { return _selInvoiceLine; }
            set
            {
                if (_selInvoiceLine != value)
                {
                    _selInvoiceLine = value;
                    RaisePropertyChanged(() => SelInvoiceLine);
                }
            }
        }

        #endregion

        #region 币种集合
        /// <summary>
        ///     币种集合
        /// </summary>
        public QueryableDataServiceCollectionView<CurrencyDTO> Currencies { get; set; }

        #endregion

        #region 供应商集合
        /// <summary>
        ///     供应商集合
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }

        #endregion

        #region 订单集合
        /// <summary>
        ///     所有订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<OrderDTO> Orders { get; set; }

        /// <summary>
        ///     飞机租赁订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftLeaseOrderDTO> AircraftLeaseOrders { get; set; }

        /// <summary>
        ///     发动机租赁订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<EngineLeaseOrderDTO> EngineLeaseOrders { get; set; }

        #endregion

        #region 选择的订单
        private OrderDTO _selOrder;

        /// <summary>
        ///     选择的租赁订单
        /// </summary>
        public OrderDTO SelOrder
        {
            get { return _selOrder; }
            set
            {
                if (_selOrder != value)
                {
                    _selOrder = value;
                    RaisePropertyChanged(() => SelOrder);
                }
            }
        }

        private AircraftLeaseOrderDTO _selAircraftLeaseOrder;

        /// <summary>
        ///     选择的飞机租赁订单
        /// </summary>
        public AircraftLeaseOrderDTO SelAircraftLeaseOrder
        {
            get { return _selAircraftLeaseOrder; }
            set
            {
                if (_selAircraftLeaseOrder != value)
                {
                    _selAircraftLeaseOrder = value;
                    _aircraftLeaseOrderLines.Clear();
                    foreach (var orderLine in value.AircraftLeaseOrderLines)
                    {
                        AircraftLeaseOrderLines.Add(orderLine);
                    }
                    RaisePropertyChanged(() => SelAircraftLeaseOrder);
                }
            }
        }

        private EngineLeaseOrderDTO _selEngineLeaseOrder;

        /// <summary>
        ///     选择的发动机租赁订单
        /// </summary>
        public EngineLeaseOrderDTO SelEngineLeaseOrder
        {
            get { return _selEngineLeaseOrder; }
            set
            {
                if (_selEngineLeaseOrder != value)
                {
                    _selEngineLeaseOrder = value;
                    _engineLeaseOrderLines.Clear();
                    foreach (var orderLine in value.EngineLeaseOrderLines)
                    {
                        EngineLeaseOrderLines.Add(orderLine);
                    }
                    RaisePropertyChanged(() => SelEngineLeaseOrder);
                }
            }
        }

        #endregion

        #region 订单行

        private ObservableCollection<AircraftLeaseOrderLineDTO> _aircraftLeaseOrderLines = new ObservableCollection<AircraftLeaseOrderLineDTO>();

        /// <summary>
        ///     飞机租赁订单行
        /// </summary>
        public ObservableCollection<AircraftLeaseOrderLineDTO> AircraftLeaseOrderLines
        {
            get { return _aircraftLeaseOrderLines; }
            private set
            {
                if (_aircraftLeaseOrderLines != value)
                {
                    _aircraftLeaseOrderLines = value;
                    RaisePropertyChanged(() => AircraftLeaseOrderLines);
                }
            }
        }


        private ObservableCollection<EngineLeaseOrderLineDTO> _engineLeaseOrderLines = new ObservableCollection<EngineLeaseOrderLineDTO>();

        /// <summary>
        ///     发动机租赁订单行
        /// </summary>
        public ObservableCollection<EngineLeaseOrderLineDTO> EngineLeaseOrderLines
        {
            get { return _engineLeaseOrderLines; }
            private set
            {
                if (_engineLeaseOrderLines != value)
                {
                    _engineLeaseOrderLines = value;
                    RaisePropertyChanged(() => EngineLeaseOrderLines);
                }
            }
        }

        #endregion

        #region 选择的订单行
        private OrderLineDTO _relatedOrderLine;

        /// <summary>
        ///     选择的关联租赁订单行
        /// </summary>
        public OrderLineDTO RelatedOrderLine
        {
            get { return _relatedOrderLine; }
            set
            {
                if (_relatedOrderLine != value)
                {
                    _relatedOrderLine = value;
                    RaisePropertyChanged(() => RelatedOrderLine);
                }
            }
        }


        private AircraftLeaseOrderLineDTO _selAircraftLeaseOrderLine;

        /// <summary>
        ///     选择的租赁订单行
        /// </summary>
        public AircraftLeaseOrderLineDTO SelAircraftLeaseOrderLine
        {
            get { return _selAircraftLeaseOrderLine; }
            set
            {
                if (_selAircraftLeaseOrderLine != value)
                {
                    _selAircraftLeaseOrderLine = value;
                    RaisePropertyChanged(() => SelAircraftLeaseOrderLine);
                }
            }
        }

        private EngineLeaseOrderLineDTO _selEngineLeaseOrderLine;

        /// <summary>
        ///     选择的发动机租赁订单行
        /// </summary>
        public EngineLeaseOrderLineDTO SelEngineLeaseOrderLine
        {
            get { return _selEngineLeaseOrderLine; }
            set
            {
                if (_selEngineLeaseOrderLine != value)
                {
                    _selEngineLeaseOrderLine = value;
                    RaisePropertyChanged(() => SelEngineLeaseOrderLine);
                }
            }
        }
        #endregion

        #region 付款计划集合
        /// <summary>
        ///    所有付款计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PaymentScheduleDTO> PaymentSchedules { get; set; }

        /// <summary>
        ///     飞机付款计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<AcPaymentScheduleDTO> AcPaymentSchedules { get; set; }


        /// <summary>
        ///     发动机付款计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePaymentScheduleDTO> EnginePaymentSchedules { get; set; }

        #endregion

        #region 选择的付款计划
        private PaymentScheduleDTO _selPaymentSchedule;

        /// <summary>
        ///     选择的付款计划
        /// </summary>
        public PaymentScheduleDTO SelPaymentSchedule
        {
            get { return _selPaymentSchedule; }
            set
            {
                if (_selPaymentSchedule != value)
                {
                    _selPaymentSchedule = value;
                    RaisePropertyChanged(() => SelPaymentSchedule);
                }
            }
        }
        #endregion

        #region 合同飞机集合
        /// <summary>
        ///     合同飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<ContractAircraftDTO> ContractAircrafts { get; set; }

        #endregion

        #region 合同发动机集合
        /// <summary>
        ///     合同发动机集合
        /// </summary>
        public QueryableDataServiceCollectionView<ContractEngineDTO> ContractEngines { get; set; }

        #endregion

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 新建租赁发票

        /// <summary>
        ///     新建租赁发票
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            LeaseOrderChildView.ShowDialog();
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除租赁发票

        /// <summary>
        ///     删除租赁发票
        /// </summary>
        public DelegateCommand<object> DeleteCommand { get; private set; }

        private void OnDelete(object obj)
        {
            LeaseInvoices.Remove(SelLeaseInvoice);
            var currentLeaseInvoice = LeaseInvoices.FirstOrDefault();
            if (currentLeaseInvoice == null)
            {
                //删除完，若没有记录了，则也要删除界面明细
                InvoiceLines.Clear();
                RelatedPaymentSchedule.Clear();
                RelatedOrder.Clear();
            }
        }

        private bool CanDelete(object obj)
        {
            bool canRemove;
            if (SelLeaseInvoice != null)
                canRemove = true;
            else if (LeaseInvoices != null)
                canRemove = true;
            else canRemove = false;
            return canRemove;
        }
        #endregion

        #region 新增租赁发票行
        /// <summary>
        ///     新增租赁发票行
        /// </summary>
        public DelegateCommand<object> AddCommand { get; private set; }

        private void OnAdd(object obj)
        {
            var invoiceLine = new InvoiceLineDTO
            {
                InvoiceLineId = RandomHelper.Next(),
                InvoiceId = SelLeaseInvoice.LeaseInvoiceId
            };
            SelLeaseInvoice.InvoiceLines.Add(invoiceLine);
            InvoiceLines.Add(invoiceLine);
        }

        private bool CanAdd(object obj)
        {
            return true;
        }
        #endregion

        #region 删除租赁发票行

        /// <summary>
        ///     删除租赁发票行
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            SelLeaseInvoice.InvoiceLines.Remove(SelInvoiceLine);
            InvoiceLines.Remove(SelInvoiceLine);
        }

        private bool CanRemove(object obj)
        {
            return true;
        }
        #endregion

        #region 提交审核

        /// <summary>
        ///     提交审核
        /// </summary>
        public DelegateCommand<object> SubmitCommand { get; private set; }

        private void OnSubmit(object obj)
        {
            IsSubmited = true;
        }

        private bool CanSubmit(object obj)
        {   
            return true;
        }
        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> CheckCommand { get; private set; }

        private void OnCheck(object obj)
        {
            SelLeaseInvoice.Reviewer = "HQB";
            SelLeaseInvoice.ReviewDate = DateTime.Now;
        }

        private bool CanCheck(object obj)
        {
            return true;
        }
        #endregion

        #region GridView单元格变更处理
        public DelegateCommand<object> CellEditEndCommand { set; get; }

        /// <summary>
        /// GridView单元格变更处理
        /// </summary>
        /// <param name="sender"></param>
        public void OnCellEditEnd(object sender)
        {
            var gridView = sender as RadGridView;
            if (gridView != null)
            {
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "TotalLine"))
                {
                    decimal totalCount = SelLeaseInvoice.InvoiceLines.Sum(invoiceLine => invoiceLine.Amount);
                    SelLeaseInvoice.InvoiceValue = totalCount;
                }
            }
        }


        #endregion
        #endregion

        #region 子窗体相关操作
        [Import]
        public LeaseOrderChildView LeaseOrderChildView; //初始化子窗体

        #region 命令

        #region 取消命令

        public DelegateCommand<object> CancelCommand { get; private set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCancelExecute(object sender)
        {
            LeaseOrderChildView.Close();
        }

        /// <summary>
        ///     判断取消命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>取消命令是否可用。</returns>
        public bool CanCancelExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 确定命令

        public DelegateCommand<object> CommitCommand { get; private set; }

        /// <summary>
        ///     执行确定命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCommitExecute(object sender)
        {
            var invoice = new LeaseInvoiceDTO
            {
                LeaseInvoiceId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
                InvoiceDate = DateTime.Now,
            };
            string selectedPane = this.LeaseOrderChildView.PaneGroups.SelectedPane.Title.ToString();
            if (selectedPane == "飞机租赁订单")
            {
                if (SelAircraftLeaseOrder != null && SelAircraftLeaseOrderLine != null)
                {
                    invoice.OrderId = SelAircraftLeaseOrder.Id;
                    invoice.SupplierName = SelAircraftLeaseOrder.SupplierName;
                    invoice.SupplierId = SelAircraftLeaseOrder.SupplierId;
                    var paymentSchedule =
                        AcPaymentSchedules.FirstOrDefault(
                            p => p.ContractAcId == SelAircraftLeaseOrderLine.ContractAircraftId);
                    if (paymentSchedule != null && paymentSchedule.PaymentScheduleLines!=null)
                    {
                        invoice.PaymentScheduleLineId = paymentSchedule.PaymentScheduleLines.First().PaymentScheduleLineId;
                        var invoiceLine = new InvoiceLineDTO
                        {
                            OrderLineId = SelAircraftLeaseOrderLine.Id,
                        };
                        invoice.InvoiceLines.Add(invoiceLine);
                        LeaseInvoices.AddNew(invoice);
                        LeaseOrderChildView.Close();
                    }
                    else
                    {
                        MessageAlert("此订单行还没有创建付款计划！");
                    }
                }
                else
                {
                    MessageAlert("未选中飞机租赁订单行");
                }
            }
            else if (selectedPane == "发动机租赁订单")
            {
                if (SelEngineLeaseOrder != null && SelEngineLeaseOrderLine != null)
                {
                    invoice.OrderId = SelEngineLeaseOrder.Id;
                    invoice.SupplierName = SelAircraftLeaseOrder.SupplierName;
                    invoice.SupplierId = SelAircraftLeaseOrder.SupplierId;
                    var paymentSchedule =
                        EnginePaymentSchedules.FirstOrDefault(
                            p => p.ContractEngineId == SelEngineLeaseOrderLine.ContractEngineId);
                    if (paymentSchedule != null)
                    {
                        invoice.PaymentScheduleLineId = paymentSchedule.EnginePaymentScheduleId;

                        var invoiceLine = new InvoiceLineDTO
                        {
                            OrderLineId = SelEngineLeaseOrderLine.Id,
                        };
                        invoice.InvoiceLines.Add(invoiceLine);
                        LeaseInvoices.AddNew(invoice);
                        LeaseOrderChildView.Close();
                    }
                    else
                    {
                        MessageAlert("此订单行还没有创建付款计划！");
                    }
                }
                else
                {
                    MessageAlert("未选中发动机租赁订单行！");
                }
            }
        }


        /// <summary>
        ///     判断确定命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>确定命令是否可用。</returns>
        public bool CanCommitExecute(object sender)
        {
            return true;
        }

        #endregion

        #endregion

        #endregion
    }
}
