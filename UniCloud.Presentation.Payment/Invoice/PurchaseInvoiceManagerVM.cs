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

            Currencies = new QueryableDataServiceCollectionView<CurrencyDTO>(_paymentData, _paymentData.Currencies);

            Suppliers = new QueryableDataServiceCollectionView<SupplierDTO>(_paymentData, _paymentData.Suppliers);

            PaymentSchedules = new QueryableDataServiceCollectionView<PaymentScheduleDTO>(_paymentData,
                _paymentData.PaymentSchedules);

            AcPaymentSchedules = Service.CreateCollection<AcPaymentScheduleDTO>(_paymentData.AcPaymentSchedules);
            Service.RegisterCollectionView(AcPaymentSchedules); //注册查询集合。
            AcPaymentSchedules.PropertyChanged += OnViewPropertyChanged;

            EnginePaymentSchedules =
                Service.CreateCollection<EnginePaymentScheduleDTO>(_paymentData.EnginePaymentSchedules);
            Service.RegisterCollectionView(EnginePaymentSchedules); //注册查询集合。
            EnginePaymentSchedules.PropertyChanged += OnViewPropertyChanged;

            StandardPaymentSchedules =
                Service.CreateCollection<StandardPaymentScheduleDTO>(_paymentData.StandardPaymentSchedules);
            Service.RegisterCollectionView(StandardPaymentSchedules); //注册查询集合。
            StandardPaymentSchedules.PropertyChanged += OnViewPropertyChanged;

            AircraftPurchaseOrders = new QueryableDataServiceCollectionView<AircraftPurchaseOrderDTO>(_paymentData,
                _paymentData.AircraftPurchaseOrders);

            EnginePurchaseOrders = new QueryableDataServiceCollectionView<EnginePurchaseOrderDTO>(_paymentData,
                _paymentData.EnginePurchaseOrders);

            BFEPurchaseOrders = new QueryableDataServiceCollectionView<BFEPurchaseOrderDTO>(_paymentData,
                _paymentData.BFEPurchaseOrders);

            var fd = new FilterDescriptor("ImportType", FilterOperator.Contains, "购买");
            
            ContractAircrafts = Service.CreateCollection<ContractAircraftDTO>(_paymentData.ContractAircrafts);
            ContractAircrafts.FilterDescriptors.Add(fd);
            Service.RegisterCollectionView(ContractAircrafts); //注册查询集合。
            ContractAircrafts.PropertyChanged += OnViewPropertyChanged;


            ContractEngines = Service.CreateCollection<ContractEngineDTO>(_paymentData.ContractEngines);
            ContractEngines.FilterDescriptors.Add(fd);
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
            CellEditEndCommand = new DelegateCommand<object>(OnCellEditEnd);
            SubmitCommand = new DelegateCommand<object>(OnSubmit, CanSubmit);
            CheckCommand = new DelegateCommand<object>(OnCheck, CanCheck);
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
            PurchaseInvoices.Load(true);
            PaymentSchedules.Load(true);
            AcPaymentSchedules.Load(true);
            EnginePaymentSchedules.Load(true);
            StandardPaymentSchedules.Load(true);
            AircraftPurchaseOrders.Load(true);
            EnginePurchaseOrders.Load(true);
            BFEPurchaseOrders.Load(true);
            ContractAircrafts.Load(true);
            ContractEngines.Load(true);
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
                    SelInvoiceLine = InvoiceLines.FirstOrDefault();
                    _relatedPaymentSchedule.Clear();
                    RelatedPaymentSchedule.Add(
                        PaymentSchedules.FirstOrDefault(p =>
                        {
                            var paymentScheduleLine =
                                p.PaymentScheduleLines.FirstOrDefault(
                                    l => l.PaymentScheduleLineId == value.PaymentScheduleLineId);
                            return paymentScheduleLine != null &&
                                   paymentScheduleLine.PaymentScheduleLineId == value.PaymentScheduleLineId;
                        }));
                    SelPaymentSchedule = RelatedPaymentSchedule.FirstOrDefault();
                    if(SelPaymentSchedule!=null)
                    RelatedPaymentScheduleLine =
                        SelPaymentSchedule.PaymentScheduleLines.FirstOrDefault(
                            l => l.InvoiceId == value.PurchaseInvoiceId);
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

        #region 关联的付款计划及付款计划行

        private ObservableCollection<PaymentScheduleDTO> _relatedPaymentSchedule =
            new ObservableCollection<PaymentScheduleDTO>();

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
                    RaisePropertyChanged(() => RelatedPaymentSchedule);
                }
            }
        }
        private PaymentScheduleDTO _selPaymentSchedule;

        /// <summary>
        ///     关联的付款计划
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

        private PaymentScheduleLineDTO _relatedPaymentScheduleLine;

        /// <summary>
        ///     选择的付款计划行
        /// </summary>
        public PaymentScheduleLineDTO RelatedPaymentScheduleLine
        {
            get { return _relatedPaymentScheduleLine; }
            set
            {
                if (_relatedPaymentScheduleLine != value)
                {
                    _relatedPaymentScheduleLine = value;
                    RaisePropertyChanged(() => RelatedPaymentScheduleLine);
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
        ///     飞机采购订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftPurchaseOrderDTO> AircraftPurchaseOrders { get; set; }

        /// <summary>
        ///     发动机采购订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePurchaseOrderDTO> EnginePurchaseOrders { get; set; }

        /// <summary>
        ///     BFE订单集合
        /// </summary>
        public QueryableDataServiceCollectionView<BFEPurchaseOrderDTO> BFEPurchaseOrders { get; set; }

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
            PurchasePayscheduleChildView.ShowDialog();
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
                RelatedPaymentSchedule.Clear();
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
            SelPurchaseInvoice.Reviewer = "HQB";
            SelPurchaseInvoice.ReviewDate = DateTime.Now;
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
                    decimal totalCount = SelPurchaseInvoice.InvoiceLines.Sum(invoiceLine => invoiceLine.Amount);
                    SelPurchaseInvoice.InvoiceValue = totalCount;
                }
            }
        }


        #endregion

        #endregion

        #region 子窗体相关操作

        [Import]
        public PurchasePayscheduleChildView PurchasePayscheduleChildView; //初始化子窗体

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

        /// <summary>
        ///     标准付款计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<StandardPaymentScheduleDTO> StandardPaymentSchedules { get; set; }

        #endregion

        #region 合同飞机集合

        /// <summary>
        ///     合同飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<ContractAircraftDTO> ContractAircrafts { get; set; }

        private ContractAircraftDTO _selContractAircraft;

        /// <summary>
        ///     选择的合同飞机
        /// </summary>
        public ContractAircraftDTO SelContractAircraft
        {
            get { return _selContractAircraft; }
            set
            {
                if (_selContractAircraft != value)
                {
                    _selContractAircraft = value;
                    _curAcPaymentSchedule.Clear();
                    CurAcPaymentSchedule.Add(
                        AcPaymentSchedules.FirstOrDefault(p => p.ContractAcId == value.ContractAircrafId));
                    SelAcPaymentSchedule = CurAcPaymentSchedule.FirstOrDefault();
                    RaisePropertyChanged(() => SelContractAircraft);
                }
            }
        }

        #endregion

        #region 合同发动机集合

        /// <summary>
        ///     合同发动机集合
        /// </summary>
        public QueryableDataServiceCollectionView<ContractEngineDTO> ContractEngines { get; set; }

        private ContractEngineDTO _selContractEngine;

        /// <summary>
        ///     选择的合同发动机
        /// </summary>
        public ContractEngineDTO SelContractEngine
        {
            get { return _selContractEngine; }
            set
            {
                if (_selContractEngine != value)
                {
                    _selContractEngine = value;
                    _curEnginePaymentSchedule.Clear();
                    CurEnginePaymentSchedule.Add(
                        EnginePaymentSchedules.FirstOrDefault(p => p.ContractEngineId == value.ContractEngineId));
                    SelEnginePaymentSchedule = CurEnginePaymentSchedule.FirstOrDefault();

                    RaisePropertyChanged(() => SelContractEngine);
                }
            }
        }

        #endregion

        #region 与选择合同飞机关联的飞机付款计划

        private ObservableCollection<AcPaymentScheduleDTO> _curAcPaymentSchedule =
            new ObservableCollection<AcPaymentScheduleDTO>();

        /// <summary>
        /// 与选择合同飞机关联的飞机付款计划
        /// </summary>
        public ObservableCollection<AcPaymentScheduleDTO> CurAcPaymentSchedule
        {
            get { return _curAcPaymentSchedule; }
            set
            {
                if (_curAcPaymentSchedule != value)
                {
                    _curAcPaymentSchedule = value;
                    RaisePropertyChanged(() => CurAcPaymentSchedule);
                }
            }
        }

        #endregion

        #region 与选择合同发动机关联的发动机付款计划

        private ObservableCollection<EnginePaymentScheduleDTO> _curEnginePaymentSchedule =
            new ObservableCollection<EnginePaymentScheduleDTO>();

        /// <summary>
        /// 与选择合同发动机关联的发动机付款计划
        /// </summary>
        public ObservableCollection<EnginePaymentScheduleDTO> CurEnginePaymentSchedule
        {
            get { return _curEnginePaymentSchedule; }
            set
            {
                if (_curEnginePaymentSchedule != value)
                {
                    _curEnginePaymentSchedule = value;
                    RaisePropertyChanged(() => CurEnginePaymentSchedule);
                }
            }
        }

        #endregion

        #region 选择的付款计划及付款计划行

        private AcPaymentScheduleDTO _selAcPaymentSchedule;

        /// <summary>
        ///     选择的飞机付款计划
        /// </summary>
        public AcPaymentScheduleDTO SelAcPaymentSchedule
        {
            get { return _selAcPaymentSchedule; }
            set
            {
                if (_selAcPaymentSchedule != value)
                {
                    _selAcPaymentSchedule = value;
                    RaisePropertyChanged(() => SelAcPaymentSchedule);
                }
            }
        }

        private EnginePaymentScheduleDTO _selEnginePaymentSchedule;

        /// <summary>
        ///     选择的发动机付款计划
        /// </summary>
        public EnginePaymentScheduleDTO SelEnginePaymentSchedule
        {
            get { return _selEnginePaymentSchedule; }
            set
            {
                if (_selEnginePaymentSchedule != value)
                {
                    _selEnginePaymentSchedule = value;
                    RaisePropertyChanged(() => SelEnginePaymentSchedule);
                }
            }
        }

        private StandardPaymentScheduleDTO _selStandardPaymentSchedule;

        /// <summary>
        ///     选择的BFE付款计划
        /// </summary>
        public StandardPaymentScheduleDTO SelStandardPaymentSchedule
        {
            get { return _selStandardPaymentSchedule; }
            set
            {
                if (_selStandardPaymentSchedule != value)
                {
                    _selStandardPaymentSchedule = value;
                    RaisePropertyChanged(() => SelStandardPaymentSchedule);
                }
            }
        }

        private PaymentScheduleLineDTO _selPaymentScheduleLine;

        /// <summary>
        ///     选择的付款计划行
        /// </summary>
        public PaymentScheduleLineDTO SelPaymentScheduleLine
        {
            get { return _selPaymentScheduleLine; }
            set
            {
                if (_selPaymentScheduleLine != value)
                {
                    _selPaymentScheduleLine = value;
                    RaisePropertyChanged(() => SelPaymentScheduleLine);
                }
            }
        }

        #endregion

        #region 命令

        #region 取消命令

        public DelegateCommand<object> CancelCommand { get; private set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCancelExecute(object sender)
        {
            PurchasePayscheduleChildView.Close();
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
            string selectedPane = this.PurchasePayscheduleChildView.PaneGroups.SelectedPane.Title.ToString();
            if (selectedPane == "采购的飞机对应的付款计划")
            {
                if (SelContractAircraft == null)
                {
                    MessageAlert("还未选择合同飞机！");
                }
                else if (SelAcPaymentSchedule != null)
                {
                    if (SelPaymentScheduleLine == null)
                    {
                        MessageAlert("请选择一条付款计划行!");
                    }
                    else
                    {
                        var orderline = new AircraftPurchaseOrderLineDTO();
                        var order = AircraftPurchaseOrders.FirstOrDefault(p =>
                        {
                            orderline =
                                p.AircraftPurchaseOrderLines.FirstOrDefault(
                                    l => l.ContractAircraftId == SelContractAircraft.ContractAircrafId);
                            return orderline != null &&
                                   orderline.OrderId == p.Id;
                        });
                        if (order != null)
                        {
                            invoice.OrderId = order.Id; //发票关联到订单，发票行关联到订单行
                        }
                        else
                        {
                            MessageAlert("与订单关联时出错，请检查数据！");
                        }
                        invoice.InvoiceValue = SelPaymentScheduleLine.Amount;
                        invoice.SupplierName = SelAcPaymentSchedule.SupplierName;
                        invoice.SupplierId = SelAcPaymentSchedule.SupplierId;
                        invoice.PaymentScheduleLineId = SelPaymentScheduleLine.PaymentScheduleLineId;
                        var invoiceLine = new InvoiceLineDTO
                        {
                            OrderLineId = orderline.Id,
                            Amount = SelPaymentScheduleLine.Amount,
                        };
                        invoice.InvoiceLines.Add(invoiceLine);
                        PurchaseInvoices.AddNew(invoice);
                        PurchasePayscheduleChildView.Close();

                    }
                }
            }
            else if (selectedPane == "采购的发动机对应的付款计划")
            {
                if (SelContractEngine == null)
                {
                    MessageAlert("还未选择合同发动机！");
                }
                else if (SelEnginePaymentSchedule != null)
                {
                    if (SelPaymentScheduleLine == null)
                    {
                        MessageAlert("请选择一条付款计划行!");
                    }
                    else
                    {
                        var orderline = new EnginePurchaseOrderLineDTO();
                        var order = EnginePurchaseOrders.FirstOrDefault(p =>
                        {
                            orderline =
                                p.EnginePurchaseOrderLines.FirstOrDefault(
                                    l => l.ContractEngineId == SelContractEngine.ContractEngineId);
                            return orderline != null &&
                                   orderline.OrderId == p.Id;
                        });
                        if (order != null)
                        {
                            invoice.OrderId = order.Id; //发票关联到订单，发票行关联到订单行
                        }
                        else
                        {
                            MessageAlert("与订单关联时出错，请检查数据！");
                        }
                        invoice.InvoiceValue = SelPaymentScheduleLine.Amount;
                        invoice.SupplierName = SelEnginePaymentSchedule.SupplierName;
                        invoice.SupplierId = SelEnginePaymentSchedule.SupplierId;
                        invoice.PaymentScheduleLineId = SelPaymentScheduleLine.PaymentScheduleLineId;
                        var invoiceLine = new InvoiceLineDTO
                        {
                            OrderLineId = orderline.Id,
                            Amount = SelPaymentScheduleLine.Amount,
                        };
                        invoice.InvoiceLines.Add(invoiceLine);
                        PurchaseInvoices.AddNew(invoice);
                        PurchasePayscheduleChildView.Close();
                    }
                }
            }
            else if (selectedPane == "采购航材的付款计划")
            {
                if (SelStandardPaymentSchedule != null)
                {
                    if (SelPaymentScheduleLine == null)
                    {
                        MessageAlert("请选择一条付款计划行！");
                    }
                    else
                    {
                        invoice.SupplierId = SelStandardPaymentSchedule.SupplierId;
                        invoice.SupplierName = SelStandardPaymentSchedule.SupplierName;
                        invoice.OrderId = SelStandardPaymentSchedule.OrderId;
                        invoice.PaymentScheduleLineId = SelPaymentScheduleLine.PaymentScheduleLineId;
                        invoice.InvoiceValue = SelPaymentScheduleLine.Amount;
                        var invoiceLine = new InvoiceLineDTO
                        {
                            Amount = SelPaymentScheduleLine.Amount,
                        };
                        invoice.InvoiceLines.Add(invoiceLine);
                        PurchaseInvoices.AddNew(invoice);
                        PurchasePayscheduleChildView.Close();
                    }
                }
                else
                {
                    MessageBox.Show("未选中航材付款计划！");
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
