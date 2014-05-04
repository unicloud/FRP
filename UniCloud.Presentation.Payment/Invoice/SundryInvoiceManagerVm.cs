#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/4 14:28:26
// 文件名：SundryInvoiceManagerVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/4 14:28:26
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
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

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(SundryInvoiceManagerVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SundryInvoiceManagerVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PaymentData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPaymentService _service;

        [ImportingConstructor]
        public SundryInvoiceManagerVm(IRegionManager regionManager, IPaymentService service)
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
            SundryInvoices = _service.CreateCollection(_context.SundryInvoices, o => o.InvoiceLines);
            _service.RegisterCollectionView(SundryInvoices); //注册查询集合。

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
            SundryInvoices.AutoLoad = true;
            Currencies = _service.GetCurrency(() => RaisePropertyChanged(() => Currencies));
            Suppliers = _service.GetSupplier(() => RaisePropertyChanged(() => Suppliers));
        }

        #region 业务

        #region 杂项发票集合

        /// <summary>
        ///     杂项发票集合
        /// </summary>
        public QueryableDataServiceCollectionView<SundryInvoiceDTO> SundryInvoices { get; set; }

        #endregion

        #region 选择的杂项发票

        private SundryInvoiceDTO _selectSundryInvoice;

        /// <summary>
        ///     选择的杂项发票
        /// </summary>
        public SundryInvoiceDTO SelectSundryInvoice
        {
            get { return _selectSundryInvoice; }
            set
            {
                if (value != null && _selectSundryInvoice != value)
                {
                    _selectSundryInvoice = value;
                    _invoiceLines.Clear();
                    foreach (var invoiceLine in value.InvoiceLines)
                    {
                        InvoiceLines.Add(invoiceLine);
                    }
                    RaisePropertyChanged(() => SelectSundryInvoice);
                }
            }
        }

        #endregion

        #region 杂项发票行

        private ObservableCollection<InvoiceLineDTO> _invoiceLines = new ObservableCollection<InvoiceLineDTO>();

        /// <summary>
        ///     杂项发票行
        /// </summary>
        public ObservableCollection<InvoiceLineDTO> InvoiceLines
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

        #region 选择的杂项发票行

        private InvoiceLineDTO _selInvoiceLine;

        /// <summary>
        ///     选择的杂项发票行
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

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 新建杂项发票

        /// <summary>
        ///     新建杂项发票
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            SelectSundryInvoice = new SundryInvoiceDTO
            {
                SundryInvoiceId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
                InvoiceDate = DateTime.Now,
                CurrencyId = Currencies.FirstOrDefault().Id,
            };
            var supplier = Suppliers.FirstOrDefault();
            if (supplier != null)
            {
                SelectSundryInvoice.SupplierId = supplier.SupplierId;
                SelectSundryInvoice.SupplierName = supplier.Name;
            }
            SundryInvoices.AddNew(SelectSundryInvoice);
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除杂项发票

        /// <summary>
        ///     删除杂项发票
        /// </summary>
        public DelegateCommand<object> DeleteCommand { get; private set; }

        private void OnDelete(object obj)
        {
            if (SelectSundryInvoice == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            SundryInvoices.Remove(SelectSundryInvoice);
            var currentCreditNote = SundryInvoices.FirstOrDefault();
            if (currentCreditNote == null)
            {
                //删除完，若没有记录了，则也要删除界面明细
                InvoiceLines.Clear();
            }
        }

        private bool CanDelete(object obj)
        {
            return true;
        }

        #endregion

        #region 新增杂项发票行

        /// <summary>
        ///     新增杂项发票行
        /// </summary>
        public DelegateCommand<object> AddCommand { get; private set; }

        private void OnAdd(object obj)
        {
            var invoiceLine = new InvoiceLineDTO
            {
                InvoiceLineId = RandomHelper.Next(),
                InvoiceId = SelectSundryInvoice.SundryInvoiceId,
            };
            SelectSundryInvoice.InvoiceLines.Add(invoiceLine);
            InvoiceLines.Add(invoiceLine);
        }

        private bool CanAdd(object obj)
        {
            return true;
        }

        #endregion

        #region 删除杂项发票行

        /// <summary>
        ///     删除杂项发票行
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            SelectSundryInvoice.InvoiceLines.Remove(SelInvoiceLine);
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
            SelectSundryInvoice.Reviewer = "HQB";
            SelectSundryInvoice.ReviewDate = DateTime.Now;
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
                    decimal totalCount = SelectSundryInvoice.InvoiceLines.Sum(invoiceLine => invoiceLine.Amount);
                    SelectSundryInvoice.InvoiceValue = totalCount;
                }
            }
        }

        #endregion

        #endregion
    }
}
