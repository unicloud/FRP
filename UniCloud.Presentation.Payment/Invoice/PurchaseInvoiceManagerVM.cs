#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/12 9:41:53
// 文件名：PurchaseInvoiceManagerVM
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
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
    [Export(typeof(PurchaseInvoiceManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PurchaseInvoiceManagerVM : EditViewModelBase
    {
        #region 声明、初始化
        private readonly IRegionManager _regionManager;
        private PaymentData _paymentData;

        [ImportingConstructor]
        public PurchaseInvoiceManagerVM(IRegionManager regionManager)
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
            PurchaseInvoices = Service.CreateCollection<PurchaseInvoiceDTO>(_paymentData.PurchaseInvoices);
            Service.RegisterCollectionView(PurchaseInvoices); //注册查询集合。
            PurchaseInvoices.PropertyChanged += OnViewPropertyChanged;

            Currencies = Service.CreateCollection<CurrencyDTO>(_paymentData.Currencies);
            Service.RegisterCollectionView(Currencies); //注册查询集合。
            Currencies.PropertyChanged += OnViewPropertyChanged;

            AircraftPurchaseOrders = Service.CreateCollection<AircraftPurchaseOrderDTO>(_paymentData.AircraftPurchaseOrders);
            Service.RegisterCollectionView(AircraftPurchaseOrders); //注册查询集合。
            AircraftPurchaseOrders.PropertyChanged += OnViewPropertyChanged;

            EnginePurchaseOrders = Service.CreateCollection<EnginePurchaseOrderDTO>(_paymentData.EnginePurchaseOrders);
            Service.RegisterCollectionView(EnginePurchaseOrders); //注册查询集合。
            EnginePurchaseOrders.PropertyChanged += OnViewPropertyChanged;

            BFEPurchaseOrders = Service.CreateCollection<BFEPurchaseOrderDTO>(_paymentData.BFEPurchaseOrders);
            Service.RegisterCollectionView(BFEPurchaseOrders); //注册查询集合。
            BFEPurchaseOrders.PropertyChanged += OnViewPropertyChanged;

            AcPaymentSchedules = Service.CreateCollection<AcPaymentScheduleDTO>(_paymentData.AcPaymentSchedules);
            Service.RegisterCollectionView(AcPaymentSchedules); //注册查询集合。
            AcPaymentSchedules.PropertyChanged += OnViewPropertyChanged;

            EnginePaymentSchedules = Service.CreateCollection<EnginePaymentScheduleDTO>(_paymentData.EnginePaymentSchedules);
            Service.RegisterCollectionView(EnginePaymentSchedules); //注册查询集合。
            EnginePaymentSchedules.PropertyChanged += OnViewPropertyChanged;

            ContractAircrafts = Service.CreateCollection<ContractAircraftDTO>(_paymentData.ContractAircrafts);
            Service.RegisterCollectionView(ContractAircrafts); //注册查询集合。
            ContractAircrafts.PropertyChanged += OnViewPropertyChanged;

            ContractEngines = Service.CreateCollection<ContractEngineDTO>(_paymentData.ContractEngines);
            Service.RegisterCollectionView(ContractEngines); //注册查询集合。
            ContractEngines.PropertyChanged += OnViewPropertyChanged;
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
            CellEditEndCommand=new DelegateCommand<object>(OnCellEditEnd);
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
            PurchaseInvoices.AutoLoad = true;
            Currencies.AutoLoad = true;
            AircraftPurchaseOrders.AutoLoad = true;
            EnginePurchaseOrders.AutoLoad = true;
            BFEPurchaseOrders.AutoLoad = true;
            AcPaymentSchedules.AutoLoad = true;
            EnginePaymentSchedules.AutoLoad = true;
            ContractAircrafts.AutoLoad = true;
            ContractEngines.AutoLoad = true;
        }

        #region 业务

        #region 采购发票集合
        /// <summary>
        ///     采购发票集合
        /// </summary>
        public QueryableDataServiceCollectionView<PurchaseInvoiceDTO> PurchaseInvoices { get; set; }

        #endregion

        #region 选择的采购发票

        private PurchaseInvoiceDTO _selPurchaseInvoice;

        /// <summary>
        ///     选择的采购发票
        /// </summary>
        public PurchaseInvoiceDTO SelPurchaseInvoice
        {
            get { return _selPurchaseInvoice; }
            set
            {
                if (_selPurchaseInvoice != value)
                {
                    _selPurchaseInvoice = value;
                    _invoiceLines.Clear();
                    foreach (var invoiceLine in value.InvoiceLines)
                    {
                        InvoiceLines.Add(invoiceLine);
                    }
                    RaisePropertyChanged(() => SelPurchaseInvoice);
                }
            }
        }

        #endregion

        #region 采购发票行

        private ObservableCollection<InvoiceLineDTO> _invoiceLines = new ObservableCollection<InvoiceLineDTO>();

        /// <summary>
        ///     采购发票行
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

        #region 选择的采购发票行

        private InvoiceLineDTO _selInvoiceLine;

        /// <summary>
        ///     选择的采购发票行
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

        #region 订单集合

        /// <summary>
        ///     飞机采购订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftPurchaseOrderDTO> AircraftPurchaseOrders { get; set; }

        /// <summary>
        ///     飞机租赁订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftLeaseOrderDTO> AircraftLeaseOrders { get; set; }

        /// <summary>
        ///     发动机采购订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePurchaseOrderDTO> EnginePurchaseOrders { get; set; }

        /// <summary>
        ///     发动机租赁订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<EngineLeaseOrderDTO> EngineLeaseOrders { get; set; }

        /// <summary>
        ///     BFE订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<BFEPurchaseOrderDTO> BFEPurchaseOrders { get; set; }

        #endregion

        #region 选择的订单

        private AircraftPurchaseOrderDTO _selAircraftPurchaseOrder;

        /// <summary>
        ///     选择的飞机采购订单
        /// </summary>
        public AircraftPurchaseOrderDTO SelAircraftPurchaseOrder
        {
            get { return _selAircraftPurchaseOrder; }
            set
            {
                if (_selAircraftPurchaseOrder != value)
                {
                    _selAircraftPurchaseOrder = value;
                    _aircraftPurchaseOrderLines.Clear();
                    foreach (var orderLine in value.AircraftPurchaseOrderLines)
                    {
                        AircraftPurchaseOrderLines.Add(orderLine);
                    }
                    RaisePropertyChanged(() => SelAircraftPurchaseOrder);
                }
            }
        }

        private EnginePurchaseOrderDTO _selEnginePurchaseOrder;

        /// <summary>
        ///     选择的发动机采购订单
        /// </summary>
        public EnginePurchaseOrderDTO SelEnginePurchaseOrder
        {
            get { return _selEnginePurchaseOrder; }
            set
            {
                if (_selEnginePurchaseOrder != value)
                {
                    _selEnginePurchaseOrder = value;
                    _enginePurchaseOrderLines.Clear();
                    foreach (var orderLine in value.EnginePurchaseOrderLines)
                    {
                        EnginePurchaseOrderLines.Add(orderLine);
                    }
                    RaisePropertyChanged(() => SelEnginePurchaseOrder);
                }
            }
        }

        private BFEPurchaseOrderDTO _selBFEPurchaseOrder;

        /// <summary>
        ///     选择的BFE订单
        /// </summary>
        public BFEPurchaseOrderDTO SelBFEPurchaseOrder
        {
            get { return _selBFEPurchaseOrder; }
            set
            {
                if (_selBFEPurchaseOrder != value)
                {
                    _selBFEPurchaseOrder = value;
                    _bfePurchaseOrderLines.Clear();
                    foreach (var orderLine in value.BFEPurchaseOrderLines)
                    {
                        BFEPurchaseOrderLines.Add(orderLine);
                    }
                    RaisePropertyChanged(() => SelBFEPurchaseOrder);
                }
            }
        }
        #endregion

        #region 订单行

        private ObservableCollection<AircraftPurchaseOrderLineDTO> _aircraftPurchaseOrderLines = new ObservableCollection<AircraftPurchaseOrderLineDTO>();

        /// <summary>
        ///     飞机采购订单行
        /// </summary>
        public ObservableCollection<AircraftPurchaseOrderLineDTO> AircraftPurchaseOrderLines
        {
            get { return _aircraftPurchaseOrderLines; }
            private set
            {
                if (_aircraftPurchaseOrderLines != value)
                {
                    _aircraftPurchaseOrderLines = value;
                    RaisePropertyChanged(() => AircraftPurchaseOrderLines);
                }
            }
        }


        private ObservableCollection<EnginePurchaseOrderLineDTO> _enginePurchaseOrderLines = new ObservableCollection<EnginePurchaseOrderLineDTO>();

        /// <summary>
        ///     发动机采购订单行
        /// </summary>
        public ObservableCollection<EnginePurchaseOrderLineDTO> EnginePurchaseOrderLines
        {
            get { return _enginePurchaseOrderLines; }
            private set
            {
                if (_enginePurchaseOrderLines != value)
                {
                    _enginePurchaseOrderLines = value;
                    RaisePropertyChanged(() => EnginePurchaseOrderLines);
                }
            }
        }


        private ObservableCollection<BFEPurchaseOrderLineDTO> _bfePurchaseOrderLines = new ObservableCollection<BFEPurchaseOrderLineDTO>();

        /// <summary>
        ///     BFE订单行
        /// </summary>
        public ObservableCollection<BFEPurchaseOrderLineDTO> BFEPurchaseOrderLines
        {
            get { return _bfePurchaseOrderLines; }
            private set
            {
                if (_bfePurchaseOrderLines != value)
                {
                    _bfePurchaseOrderLines = value;
                    RaisePropertyChanged(() => BFEPurchaseOrderLines);
                }
            }
        }

        #endregion

        #region 选择的订单行

        private AircraftPurchaseOrderLineDTO _selAircraftPurchaseOrderLine;

        /// <summary>
        ///     选择的采购订单行
        /// </summary>
        public AircraftPurchaseOrderLineDTO SelAircraftPurchaseOrderLine
        {
            get { return _selAircraftPurchaseOrderLine; }
            set
            {
                if (_selAircraftPurchaseOrderLine != value)
                {
                    _selAircraftPurchaseOrderLine = value;
                    RaisePropertyChanged(() => SelAircraftPurchaseOrderLine);
                }
            }
        }

        private EnginePurchaseOrderLineDTO _selEnginePurchaseOrderLine;

        /// <summary>
        ///     选择的发动机采购订单行
        /// </summary>
        public EnginePurchaseOrderLineDTO SelEnginePurchaseOrderLine
        {
            get { return _selEnginePurchaseOrderLine; }
            set
            {
                if (_selEnginePurchaseOrderLine != value)
                {
                    _selEnginePurchaseOrderLine = value;
                    RaisePropertyChanged(() => SelEnginePurchaseOrderLine);
                }
            }
        }
        #endregion

        #region 付款计划集合
        /// <summary>
        ///     飞机付款计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<AcPaymentScheduleDTO> AcPaymentSchedules { get; set; }


        /// <summary>
        ///     发动机付款计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePaymentScheduleDTO> EnginePaymentSchedules { get; set; }

        /// <summary>
        ///     付款计划集合
        /// </summary>
        //public QueryableDataServiceCollectionView<PaymentScheduleDTO> AcPaymentSchedules { get; set; }
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

        #region 新建采购发票

        /// <summary>
        ///     新建采购发票
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            PurchaseOrderChildView.ShowDialog();
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除采购发票

        /// <summary>
        ///     删除采购发票
        /// </summary>
        public DelegateCommand<object> DeleteCommand { get; private set; }

        private void OnDelete(object obj)
        {
            PurchaseInvoices.Remove(SelPurchaseInvoice);
            var currentPurchaseInvoice = PurchaseInvoices.FirstOrDefault();
            if (currentPurchaseInvoice == null)
            {
                //删除完，若没有记录了，则也要删除界面明细
                InvoiceLines.Clear();
            }
        }

        private bool CanDelete(object obj)
        {
            bool canRemove;
            if (SelPurchaseInvoice != null)
                canRemove = true;
            else if (PurchaseInvoices != null)
                canRemove = true;
            else canRemove = false;
            return canRemove;
        }
        #endregion

        #region 新增采购发票行
        /// <summary>
        ///     新增采购发票行
        /// </summary>
        public DelegateCommand<object> AddCommand { get; private set; }

        private void OnAdd(object obj)
        {
            var invoiceLine = new InvoiceLineDTO
            {
                InvoiceLineId = RandomHelper.Next(),
                InvoiceId = SelPurchaseInvoice.PurchaseInvoiceId
            };
            SelPurchaseInvoice.InvoiceLines.Add(invoiceLine);
            InvoiceLines.Add(invoiceLine);
        }

        private bool CanAdd(object obj)
        {
            return true;
        }
        #endregion

        #region 删除采购发票行

        /// <summary>
        ///     删除采购发票行
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            SelPurchaseInvoice.InvoiceLines.Remove(SelInvoiceLine);
            InvoiceLines.Remove(SelInvoiceLine);
        }

        private bool CanRemove(object obj)
        {
            bool canRemove;
            if (SelPurchaseInvoice != null && SelInvoiceLine != null)
                canRemove = true;
            else canRemove = false;
            return canRemove;
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
                    decimal totalCount = SelPurchaseInvoice.InvoiceLines.Sum(invoiceLine => invoiceLine.Amount);
                    SelPurchaseInvoice.InvoiceValue = totalCount;
                }      
            }
        }


        #endregion
        #endregion

        #region 子窗体相关操作
        [Import]
        public PurchaseOrderChildView PurchaseOrderChildView; //初始化子窗体

        #region 命令

        #region 取消命令

        public DelegateCommand<object> CancelCommand { get; private set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCancelExecute(object sender)
        {
            PurchaseOrderChildView.Close();
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
            var invoice = new PurchaseInvoiceDTO
            {
                PurchaseInvoiceId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
                InvoiceDate = DateTime.Now,
            };
            string selectedPane = this.PurchaseOrderChildView.PaneGroups.SelectedPane.Title.ToString();
            if (selectedPane == "飞机采购订单")
            {
                if (SelAircraftPurchaseOrderLine != null)
                {
                    invoice.OrderId = SelAircraftPurchaseOrder.Id;
                    invoice.SupplierName = SelAircraftPurchaseOrder.Name;
                    var paymentSchedule =
                        AcPaymentSchedules.FirstOrDefault(
                            p => p.ContractAcId == SelAircraftPurchaseOrderLine.ContractAircraftId);
                    if (paymentSchedule != null) invoice.PaymentScheduleLineId = paymentSchedule.AcPaymentScheduleId;
                    PurchaseInvoices.AddNew(invoice);
                    PurchaseOrderChildView.Close();
                }
                else
                {
                    MessageAlert("未选中飞机采购订单行");
                }
            }
            else if (selectedPane == "发动机采购订单")
            {
                if (SelEnginePurchaseOrderLine != null)
                {
                    invoice.OrderId = SelEnginePurchaseOrder.Id;
                    invoice.SupplierName = SelEnginePurchaseOrder.Name;
                    var paymentSchedule =
                        EnginePaymentSchedules.FirstOrDefault(
                            p => p.ContractEngineId == SelEnginePurchaseOrderLine.ContractEngineId);
                    if (paymentSchedule != null) invoice.PaymentScheduleLineId = paymentSchedule.EnginePaymentScheduleId;
                    PurchaseInvoices.AddNew(invoice);
                    PurchaseOrderChildView.Close();
                }
                else
                {
                    MessageBox.Show("未选中发动机采购订单行！");
                }
            }
            else if (selectedPane == "BFE采购订单")
            {
                if (SelBFEPurchaseOrder != null)
                {
                    invoice.OrderId = SelEnginePurchaseOrder.Id;
                    invoice.SupplierName = SelBFEPurchaseOrder.Name;
                    //var paymentSchedule =
                    //    AcPaymentSchedules.FirstOrDefault(
                    //        p => p.ContractAcId == SelAircraftPurchaseOrderLine.ContractAircraftId);
                    //if (paymentSchedule != null) invoice.PaymentScheduleLineId = paymentSchedule.AcPaymentScheduleId;
                    PurchaseInvoices.AddNew(invoice);
                    PurchaseOrderChildView.Close();
                }
                else
                {
                    MessageBox.Show("未选中BFE采购订单！");
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
