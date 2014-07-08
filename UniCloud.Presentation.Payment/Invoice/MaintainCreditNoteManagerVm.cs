#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/4 9:56:41
// 文件名：MaintainCreditNoteManagerVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/4 9:56:41
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
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof (MaintainCreditNoteManagerVm))]
    public class MaintainCreditNoteManagerVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PaymentData _context;
        private readonly IPaymentService _service;

        [ImportingConstructor]
        public MaintainCreditNoteManagerVm(IPaymentService service)
            : base(service)
        {
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
            CreditNotes = _service.CreateCollection(_context.MaintainCreditNotes, o => o.InvoiceLines);
            CreditNotes.LoadedData += (o, e) =>
            {
                if (SelCreditNote == null)
                    SelCreditNote = CreditNotes.FirstOrDefault();
            };
            _service.RegisterCollectionView(CreditNotes); //注册查询集合。
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
        ///     项名称
        /// </summary>
        public Dictionary<int, ItemNameType> ItemNameTypes
        {
            get
            {
                return Enum.GetValues(typeof (ItemNameType))
                    .Cast<object>()
                    .ToDictionary(value => (int) value, value => (ItemNameType) value);
            }
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
            CreditNotes.AutoLoad = true;
            Currencies = _service.GetCurrency(() => RaisePropertyChanged(() => Currencies));
            Suppliers = _service.GetSupplier(() => RaisePropertyChanged(() => Suppliers));
        }

        #region 业务

        #region 贷项单集合

        /// <summary>
        ///     贷项单集合
        /// </summary>
        public QueryableDataServiceCollectionView<MaintainCreditNoteDTO> CreditNotes { get; set; }

        #endregion

        #region 选择的贷项单

        private MaintainCreditNoteDTO _selCreditNote;

        /// <summary>
        ///     选择的贷项单
        /// </summary>
        public MaintainCreditNoteDTO SelCreditNote
        {
            get { return _selCreditNote; }
            set
            {
                _selCreditNote = value;
                _invoiceLines.Clear();
                if (value != null)
                {
                    SelInvoiceLine = value.InvoiceLines.FirstOrDefault();
                    foreach (var invoiceLine in value.InvoiceLines)
                    {
                        InvoiceLines.Add(invoiceLine);
                    }
                }
                RaisePropertyChanged(() => SelCreditNote);
            }
        }

        #endregion

        #region 贷项单行

        private ObservableCollection<InvoiceLineDTO> _invoiceLines = new ObservableCollection<InvoiceLineDTO>();

        /// <summary>
        ///     贷项单行
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

        #region 选择的贷项单行

        private InvoiceLineDTO _selInvoiceLine;

        /// <summary>
        ///     选择的贷项单行
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

        #region 新建贷项单

        /// <summary>
        ///     新建贷项单
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            SelCreditNote = new MaintainCreditNoteDTO
            {
                CreditNoteId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
                InvoiceDate = DateTime.Now,
                CurrencyId = Currencies.FirstOrDefault().Id,
            };
            var supplier = Suppliers.FirstOrDefault();
            if (supplier != null)
            {
                SelCreditNote.SupplierId = supplier.SupplierId;
                SelCreditNote.SupplierName = supplier.Name;
            }
            CreditNotes.AddNew(SelCreditNote);
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除贷项单

        /// <summary>
        ///     删除贷项单
        /// </summary>
        public DelegateCommand<object> DeleteCommand { get; private set; }

        private void OnDelete(object obj)
        {
            if (SelCreditNote == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                CreditNotes.Remove(SelCreditNote);
                SelCreditNote = CreditNotes.FirstOrDefault();
                if (SelCreditNote == null)
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

        #region 新增贷项单行

        /// <summary>
        ///     新增贷项单行
        /// </summary>
        public DelegateCommand<object> AddCommand { get; private set; }

        private void OnAdd(object obj)
        {
            if (SelCreditNote == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            SelInvoiceLine = new InvoiceLineDTO
            {
                InvoiceLineId = RandomHelper.Next(),
                InvoiceId = SelCreditNote.CreditNoteId,
            };
            SelCreditNote.InvoiceLines.Add(SelInvoiceLine);
            InvoiceLines.Add(SelInvoiceLine);
        }

        private bool CanAdd(object obj)
        {
            return true;
        }

        #endregion

        #region 删除贷项单行

        /// <summary>
        ///     删除贷项单行
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
                SelCreditNote.InvoiceLines.Remove(SelInvoiceLine);
                InvoiceLines.Remove(SelInvoiceLine);
                SelInvoiceLine = SelCreditNote.InvoiceLines.FirstOrDefault();
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
            SelCreditNote.Reviewer = "HQB";
            SelCreditNote.ReviewDate = DateTime.Now;
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
                if (string.Equals(cell.Column.UniqueName, "TotalLine"))
                {
                    var totalCount = SelCreditNote.InvoiceLines.Sum(invoiceLine => invoiceLine.Amount);
                    SelCreditNote.InvoiceValue = totalCount;
                }
            }
        }

        #endregion

        #endregion
    }
}