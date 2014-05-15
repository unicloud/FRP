#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/17 21:07:04
// 文件名：InvoiceVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/17 21:07:04
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
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.MaintainInvoice
{
    public class InvoiceVm : EditViewModelBase
    {
        #region 声明、初始化

        [Import]
        public MaintainPaymentScheduleView PrepayPayscheduleChildView; //初始化子窗体

        [ImportingConstructor]
        public InvoiceVm(IPaymentService service)
            : base(service)
        {
            PaymentData context = service.Context;
            Suppliers = new QueryableDataServiceCollectionView<SupplierDTO>(context, context.Suppliers);
            Currencies = new QueryableDataServiceCollectionView<CurrencyDTO>(context, context.Currencies);
            PaymentSchedules = new QueryableDataServiceCollectionView<PaymentScheduleDTO>(context, context.PaymentSchedules);

            AddInvoiceCommand = new DelegateCommand<object>(OnAddInvoice, CanAddInvoice);
            RemoveInvoiceCommand = new DelegateCommand<object>(OnRemoveInvoice, CanRemoveInvoice);
            AddInvoiceLineCommand = new DelegateCommand<object>(OnAddInvoiceLine, CanAddInvoiceLine);
            EditInvoiceCommand = new DelegateCommand<object>(OnEditInvoice, CanEditInvoice);
            RemoveInvoiceLineCommand = new DelegateCommand<object>(OnRemoveInvoiceLine, CanRemoveInvoiceLine);
            SubmitInvoiceCommand = new DelegateCommand<object>(OnSubmitInvoice, CanSubmitInvoice);
            ReviewInvoiceCommand = new DelegateCommand<object>(OnReviewInvoice, CanReviewInvoice);
        }

        #endregion

        #region 公共属性

        /// <summary>
        ///     供应商
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }

        /// <summary>
        ///     币种
        /// </summary>
        public QueryableDataServiceCollectionView<CurrencyDTO> Currencies { get; set; }

        /// <summary>
        ///     维修发票维修项
        /// </summary>
        public Dictionary<int, MaintainItem> MaintainItems
        {
            get { return Enum.GetValues(typeof(MaintainItem)).Cast<object>().ToDictionary(value => (int)value, value => (MaintainItem)value); }
        }

        /// <summary>
        ///   项名称
        /// </summary>
        public Dictionary<int, ItemNameType> ItemNameTypes
        {
            get { return Enum.GetValues(typeof(ItemNameType)).Cast<object>().ToDictionary(value => (int)value, value => (ItemNameType)value); }
        }
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

        #region 操作

        #region 创建新发票

        /// <summary>
        ///     创建新发票
        /// </summary>
        public DelegateCommand<object> AddInvoiceCommand { get; set; }

        protected virtual void OnAddInvoice(object obj)
        {
        }

        protected virtual bool CanAddInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 修改发票

        /// <summary>
        ///     修改发票
        /// </summary>
        public DelegateCommand<object> EditInvoiceCommand { get; set; }

        protected virtual void OnEditInvoice(object obj)
        {
        }

        protected virtual bool CanEditInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 删除发票

        /// <summary>
        ///     删除发票
        /// </summary>
        public DelegateCommand<object> RemoveInvoiceCommand { get; set; }

        protected virtual void OnRemoveInvoice(object obj)
        {
        }

        protected virtual bool CanRemoveInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 增加维修发票行

        /// <summary>
        ///     增加维修发票行
        /// </summary>
        public DelegateCommand<object> AddInvoiceLineCommand { get; set; }

        protected virtual void OnAddInvoiceLine(object obj)
        {
        }

        protected virtual bool CanAddInvoiceLine(object obj)
        {
            return true;
        }

        #endregion

        #region 移除维修发票行

        /// <summary>
        ///     移除维修发票行
        /// </summary>
        public DelegateCommand<object> RemoveInvoiceLineCommand { get; private set; }

        protected virtual void OnRemoveInvoiceLine(object obj)
        {
        }

        protected virtual bool CanRemoveInvoiceLine(object obj)
        {
            return true;
        }

        #endregion

        #region 提交发票

        /// <summary>
        ///     提交发票
        /// </summary>
        public DelegateCommand<object> SubmitInvoiceCommand { get; set; }

        protected virtual void OnSubmitInvoice(object obj)
        {
        }

        protected virtual bool CanSubmitInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 审核发票

        /// <summary>
        ///     审核发票
        /// </summary>
        public DelegateCommand<object> ReviewInvoiceCommand { get; set; }

        protected virtual void OnReviewInvoice(object obj)
        {
        }

        protected virtual bool CanReviewInvoice(object obj)
        {
            return true;
        }

        #endregion

        public override void LoadData()
        {
        }

        #endregion
    }
}