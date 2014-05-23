#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 10:08:32
// 文件名：SpecialRefitInvoiceManagerVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 10:08:32
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.MaintainInvoice
{
    [Export(typeof(SpecialRefitInvoiceManagerVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SpecialRefitInvoiceManagerVm : EditViewModelBase
    {
        #region 声明、初始化
        [Import]
        public MaintainPaymentScheduleView PrepayPayscheduleChildView; //初始化子窗体
        private readonly PaymentData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPaymentService _service;

        [ImportingConstructor]
        public SpecialRefitInvoiceManagerVm(IRegionManager regionManager, IPaymentService service)
            : base(service)
        {
            _regionManager = regionManager;
            _service = service;
            _context = _service.Context;
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
            PaymentSchedules = new QueryableDataServiceCollectionView<PaymentScheduleDTO>(_context, _context.PaymentSchedules);
            SpecialRefitInvoices = _service.CreateCollection(_context.SpecialRefitInvoices, o => o.MaintainInvoiceLines);
            SpecialRefitInvoices.LoadedData += (o, e) =>
                                         {
                                             if (SelectRefitInvoice == null)
                                                 SelectRefitInvoice = SpecialRefitInvoices.FirstOrDefault();
                                         };
            _service.RegisterCollectionView(SpecialRefitInvoices); //注册查询集合。

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
            CellEditEndCommand = new DelegateCommand<object>(OnCellEditEnd);
            SubmitCommand = new DelegateCommand<object>(OnSubmit, CanSubmit);
            CheckCommand = new DelegateCommand<object>(OnCheck, CanCheck);
        }

        #endregion

        #region 数据

        #region 公共属性
        /// <summary>
        ///   项名称
        /// </summary>
        public Dictionary<int, ItemNameType> ItemNameTypes
        {
            get { return Enum.GetValues(typeof(ItemNameType)).Cast<object>().ToDictionary(value => (int)value, value => (ItemNameType)value); }
        }
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

        #region 是否已提交审核

        private bool _isSubmited;

        /// <summary>
        ///     是否已提交审核
        /// </summary>
        public bool IsSubmited
        {
            get { return _isSubmited; }
            private set
            {
                if (_isSubmited != value)
                {
                    _isSubmited = value;
                    RaisePropertyChanged(() => IsSubmited);
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
            SpecialRefitInvoices.AutoLoad = true;
            Currencies = _service.GetCurrency(() => RaisePropertyChanged(() => Currencies));
            Suppliers = _service.GetSupplier(() => RaisePropertyChanged(() => Suppliers));
            PaymentSchedules.Load(true);
        }

        #region 业务

        #region 特修改装发票集合

        /// <summary>
        ///     特修改装发票集合
        /// </summary>
        public QueryableDataServiceCollectionView<SpecialRefitInvoiceDTO> SpecialRefitInvoices { get; set; }

        #endregion

        #region 选择的特修改装发票

        private SpecialRefitInvoiceDTO _selectRefitInvoice;

        /// <summary>
        ///     选择的特修改装发票
        /// </summary>
        public SpecialRefitInvoiceDTO SelectRefitInvoice
        {
            get { return _selectRefitInvoice; }
            set
            {
                _selectRefitInvoice = value;
                _invoiceLines.Clear();
                RelatedPaymentSchedule.Clear();
                SelPaymentSchedule = null;
                if (value != null)
                {
                    SelInvoiceLine = value.MaintainInvoiceLines.FirstOrDefault();
                    foreach (var invoiceLine in value.MaintainInvoiceLines)
                    {
                        InvoiceLines.Add(invoiceLine);
                    }
                    var relate = PaymentSchedules.FirstOrDefault(p =>
                                                      {
                                                          var paymentScheduleLine =
                                                              p.PaymentScheduleLines.FirstOrDefault(
                                                                  l =>
                                                                      l.PaymentScheduleLineId ==
                                                                      value.PaymentScheduleLineId);
                                                          return paymentScheduleLine != null &&
                                                                 paymentScheduleLine.PaymentScheduleLineId ==
                                                                 value.PaymentScheduleLineId;
                                                      });
                    if (relate != null)
                        RelatedPaymentSchedule.Add(relate);
                    SelPaymentSchedule = RelatedPaymentSchedule.FirstOrDefault();
                    if (SelPaymentSchedule != null)
                        RelatedPaymentScheduleLine =
                            SelPaymentSchedule.PaymentScheduleLines.FirstOrDefault(
                                l => l.InvoiceId == value.SpecialRefitId);
                    RaisePropertyChanged(() => RelatedPaymentSchedule);
                }
                RaisePropertyChanged(() => SelectRefitInvoice);
            }
        }

        #endregion

        #region 特修改装发票行

        private ObservableCollection<MaintainInvoiceLineDTO> _invoiceLines = new ObservableCollection<MaintainInvoiceLineDTO>();

        /// <summary>
        ///     特修改装发票行
        /// </summary>
        public ObservableCollection<MaintainInvoiceLineDTO> InvoiceLines
        {
            get { return _invoiceLines; }
            set
            {
                if (_invoiceLines != value)
                {
                    _invoiceLines = value;
                    RaisePropertyChanged(() => InvoiceLines);
                }
            }
        }

        #endregion

        #region 选择的特修改装发票行

        private MaintainInvoiceLineDTO _selInvoiceLine;

        /// <summary>
        ///     选择的特修改装发票行
        /// </summary>
        public MaintainInvoiceLineDTO SelInvoiceLine
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
        /// <summary>
        ///     所有付款计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PaymentScheduleDTO> PaymentSchedules { get; set; }

        private ObservableCollection<PaymentScheduleDTO> _relatedPaymentSchedule = new ObservableCollection<PaymentScheduleDTO>();

        private PaymentScheduleLineDTO _relatedPaymentScheduleLine;

        private PaymentScheduleDTO _selPaymentSchedule;

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
        #endregion

        #endregion

        #endregion

        #region 操作

        #region 新建特修改装发票

        /// <summary>
        ///     新建特修改装发票
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            MessageConfirm("是否根据付款计划创建?", (s, arg) =>
                                          {
                                              if (arg.DialogResult != true)
                                              {
                                                  SelectRefitInvoice = new SpecialRefitInvoiceDTO
                                                                          {
                                                                              SpecialRefitId =
                                                                                  RandomHelper.Next(),
                                                                              CreateDate = DateTime.Now,
                                                                              InvoiceDate = DateTime.Now,
                                                                              InMaintainTime = DateTime.Now,
                                                                              OutMaintainTime = DateTime.Now,
                                                                          };
                                                  var currency = Currencies.FirstOrDefault();
                                                  if (currency != null)
                                                      SelectRefitInvoice.CurrencyId = currency.Id;
                                                  var supplier = Suppliers.FirstOrDefault();
                                                  if (supplier != null)
                                                  {
                                                      SelectRefitInvoice.SupplierId = supplier.SupplierId;
                                                      SelectRefitInvoice.SupplierName = supplier.Name;
                                                  }
                                                  SpecialRefitInvoices.AddNew(SelectRefitInvoice);
                                                  return;
                                              }
                                              PrepayPayscheduleChildView.ViewModel.InitData(
                                                  typeof(SpecialRefitInvoiceDTO), PrepayPayscheduleChildViewClosed);
                                              PrepayPayscheduleChildView.ShowDialog();
                                          });
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        private void PrepayPayscheduleChildViewClosed(object sender, WindowClosedEventArgs e)
        {
            if (PrepayPayscheduleChildView.Tag != null)
            {
                SelectRefitInvoice = PrepayPayscheduleChildView.Tag as SpecialRefitInvoiceDTO;
                SpecialRefitInvoices.AddNew(SelectRefitInvoice);
            }
        }
        #endregion

        #region 删除特修改装发票

        /// <summary>
        ///     删除特修改装发票
        /// </summary>
        public DelegateCommand<object> DeleteCommand { get; private set; }

        private void OnDelete(object obj)
        {
            if (SelectRefitInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                SpecialRefitInvoices.Remove(SelectRefitInvoice);
                                                SelectRefitInvoice = SpecialRefitInvoices.FirstOrDefault();
                                                if (SelectRefitInvoice == null)
                                                {
                                                    //删除完，若没有记录了，则也要删除界面明细
                                                    InvoiceLines.Clear();
                                                }
                                            });
        }

        private bool CanDelete(object obj)
        {
            return true;
        }

        #endregion

        #region 新增特修改装发票行

        /// <summary>
        ///     新增特修改装发票行
        /// </summary>
        public DelegateCommand<object> AddCommand { get; private set; }

        private void OnAdd(object obj)
        {
            if (SelectRefitInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            SelInvoiceLine = new MaintainInvoiceLineDTO
            {
                MaintainInvoiceLineId = RandomHelper.Next(),
                InvoiceId = SelectRefitInvoice.SpecialRefitId,
            };
            SelectRefitInvoice.MaintainInvoiceLines.Add(SelInvoiceLine);
            InvoiceLines.Add(SelInvoiceLine);
        }

        private bool CanAdd(object obj)
        {
            return true;
        }

        #endregion

        #region 删除特修改装发票行

        /// <summary>
        ///     删除特修改装发票行
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            if (SelInvoiceLine == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                SelectRefitInvoice.MaintainInvoiceLines.Remove(SelInvoiceLine);
                                                InvoiceLines.Remove(SelInvoiceLine);
                                                SelInvoiceLine = SelectRefitInvoice.MaintainInvoiceLines.FirstOrDefault();
                                            });
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
            SelectRefitInvoice.Reviewer = "HQB";
            SelectRefitInvoice.ReviewDate = DateTime.Now;
        }

        private bool CanCheck(object obj)
        {
            return true;
        }

        #endregion

        #region GridView单元格变更处理

        public DelegateCommand<object> CellEditEndCommand { set; get; }

        /// <summary>
        ///     GridView单元格变更处理
        /// </summary>
        /// <param name="sender"></param>
        public void OnCellEditEnd(object sender)
        {
            var gridView = sender as RadGridView;
            if (gridView != null)
            {
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "TotalLine", StringComparison.OrdinalIgnoreCase))
                {
                    decimal totalCount = SelectRefitInvoice.MaintainInvoiceLines.Sum(invoiceLine => invoiceLine.Amount);
                    SelectRefitInvoice.InvoiceValue = totalCount;
                }
            }
        }

        #endregion

        #endregion
    }
}