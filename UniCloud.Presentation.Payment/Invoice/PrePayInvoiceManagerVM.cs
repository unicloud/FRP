#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/12 9:44:02
// 文件名：PrePayInvoiceManagerVM
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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof (PrePayInvoiceManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PrePayInvoiceManagerVM : EditViewModelBase
    {
        #region 声明、初始化
        private readonly IRegionManager _regionManager;
        private PaymentData _paymentData;

        [ImportingConstructor]
        public PrePayInvoiceManagerVM(IRegionManager regionManager)
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
            PrepaymentInvoices = Service.CreateCollection<PrepaymentInvoiceDTO>(_paymentData.PrepaymentInvoices);
            Service.RegisterCollectionView(PrepaymentInvoices); //注册查询集合。
            PrepaymentInvoices.PropertyChanged += OnViewPropertyChanged;
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
            PrepaymentInvoices.AutoLoad = true;
        }

        #region 业务

        #region 预付款发票集合
        /// <summary>
        ///     预付款发票集合
        /// </summary>
        public QueryableDataServiceCollectionView<PrepaymentInvoiceDTO> PrepaymentInvoices { get; set; }

        #endregion

        #region 选择的预付款发票

        private PrepaymentInvoiceDTO _selPrepaymentInvoice;

        /// <summary>
        ///     选择的预付款发票
        /// </summary>
        public PrepaymentInvoiceDTO SelPrepaymentInvoice
        {
            get { return _selPrepaymentInvoice; }
            set
            {
                if (_selPrepaymentInvoice != value)
                {
                    _selPrepaymentInvoice = value;
                    _prepaymentInvoiceLines.Clear();
                    foreach (var invoiceLine in value.InvoiceLines)
                    {
                        PrepaymentInvoiceLines.Add(invoiceLine);
                    }
                    RaisePropertyChanged(() => SelPrepaymentInvoice);
                }
            }
        }

        #endregion

        #region 预付款发票行

        private ObservableCollection<PrepaymentInvoiceLineDTO> _prepaymentInvoiceLines=new ObservableCollection<PrepaymentInvoiceLineDTO>();

        /// <summary>
        ///     预付款发票行
        /// </summary>
        public ObservableCollection<PrepaymentInvoiceLineDTO> PrepaymentInvoiceLines
        {
            get { return _prepaymentInvoiceLines; }
            private set
            {
                if (_prepaymentInvoiceLines != value)
                {
                    _prepaymentInvoiceLines = value;
                    RaisePropertyChanged(() => PrepaymentInvoiceLines);
                }
            }
        }

        #endregion

        #region 选择的预付款发票行

        private PrepaymentInvoiceLineDTO _selPrepaymentInvoiceLine;

        /// <summary>
        ///     选择的预付款发票行
        /// </summary>
        public PrepaymentInvoiceLineDTO SelPrepaymentInvoiceLine
        {
            get { return _selPrepaymentInvoiceLine; }
            set
            {
                if (_selPrepaymentInvoiceLine != value)
                {
                    _selPrepaymentInvoiceLine = value;
                    RaisePropertyChanged(() => SelPrepaymentInvoiceLine);
                }
            }
        }

        #endregion
        #endregion

        #endregion

        #endregion

        #region 重载操作

        #region 新建预付款发票

        /// <summary>
        ///     新建预付款发票
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var invoice = new PrepaymentInvoiceDTO
            {
                PrepaymentInvoiceId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
            };
            PrepaymentInvoices.AddNew(invoice);
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除预付款发票

        /// <summary>
        ///     删除预付款发票
        /// </summary>
        public DelegateCommand<object> DeleteCommand { get; private set; }

        private void OnDelete(object obj)
        {
            PrepaymentInvoices.Remove(SelPrepaymentInvoice);
            var currentPrepaymentInvoice = PrepaymentInvoices.FirstOrDefault();
            if (currentPrepaymentInvoice == null)
            {
                //删除完，若没有记录了，则也要删除界面明细
                PrepaymentInvoiceLines.Clear();
            }
        }

        private bool CanDelete(object obj)
        {
            bool canRemove;
            if (SelPrepaymentInvoice != null)
                canRemove = true;
            else if (PrepaymentInvoices != null)
                canRemove = true;
            else canRemove = false;
            return canRemove;
        }
        #endregion

        #region 新增预付款发票行
        /// <summary>
        ///     新增预付款发票行
        /// </summary>
        public DelegateCommand<object> AddCommand { get; private set; }

        private void OnAdd(object obj)
        {
            var invoiceLine = new PrepaymentInvoiceLineDTO
            {
                PrepaymentInvoiceLineId = RandomHelper.Next(),
                InvoiceId = SelPrepaymentInvoice.PrepaymentInvoiceId
            };
            SelPrepaymentInvoice.InvoiceLines.Add(invoiceLine);
            PrepaymentInvoiceLines.Add(invoiceLine);
        }

        private bool CanAdd(object obj)
        {
            return true;
        }
        #endregion

        #region 删除预付款发票行

        /// <summary>
        ///     删除预付款发票行
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            SelPrepaymentInvoice.InvoiceLines.Remove(SelPrepaymentInvoiceLine);
            PrepaymentInvoiceLines.Remove(SelPrepaymentInvoiceLine);
        }

        private bool CanRemove(object obj)
        {
            bool canRemove;
            if (SelPrepaymentInvoice != null && SelPrepaymentInvoiceLine != null)
                canRemove = true;
            else canRemove = false;
            return canRemove;
        }
        #endregion
        #endregion
    }
}
