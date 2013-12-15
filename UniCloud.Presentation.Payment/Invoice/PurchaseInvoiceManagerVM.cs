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
    [Export(typeof (PurchaseInvoiceManagerVM))]
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
            _paymentData = new PaymentData(AgentHelper.PurchaseUri);
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
                    _purchaseInvoiceLines.Clear();
                    foreach (var invoiceLine in value.InvoiceLines)
                    {
                        PurchaseInvoiceLines.Add(invoiceLine);
                    }
                    RaisePropertyChanged(() => SelPurchaseInvoice);
                }
            }
        }

        #endregion

        #region 采购发票行

        private ObservableCollection<PurchaseInvoiceLineDTO> _purchaseInvoiceLines;

        /// <summary>
        ///     采购发票行
        /// </summary>
        public ObservableCollection<PurchaseInvoiceLineDTO> PurchaseInvoiceLines
        {
            get { return _purchaseInvoiceLines; }
            private set
            {
                if (_purchaseInvoiceLines != value)
                {
                    _purchaseInvoiceLines = value;
                    RaisePropertyChanged(() => PurchaseInvoiceLines);
                }
            }
        }

        #endregion

        #region 选择的采购发票行

        private PurchaseInvoiceLineDTO _selPurchaseInvoiceLine;

        /// <summary>
        ///     选择的采购发票行
        /// </summary>
        public PurchaseInvoiceLineDTO SelPurchaseInvoiceLine
        {
            get { return _selPurchaseInvoiceLine; }
            set
            {
                if (_selPurchaseInvoiceLine != value)
                {
                    _selPurchaseInvoiceLine = value;
                    RaisePropertyChanged(() => SelPurchaseInvoiceLine);
                }
            }
        }

        #endregion
        #endregion

        #endregion

        #endregion

        #region 重载操作

        #region 新建采购发票

        /// <summary>
        ///     新建采购发票
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var invoice = new PurchaseInvoiceDTO
            {
                PurchaseInvoiceId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
            };
            PurchaseInvoices.AddNew(invoice);
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
                PurchaseInvoiceLines.Clear();
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
            var invoiceLine = new PurchaseInvoiceLineDTO
            {
                PurchaseInvoiceLineId = RandomHelper.Next(),
                InvoiceId = SelPurchaseInvoice.PurchaseInvoiceId
            };
            SelPurchaseInvoice.InvoiceLines.Add(invoiceLine);
            PurchaseInvoiceLines.Add(invoiceLine);
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
            SelPurchaseInvoice.InvoiceLines.Remove(SelPurchaseInvoiceLine);
            PurchaseInvoiceLines.Remove(SelPurchaseInvoiceLine);
        }

        private bool CanRemove(object obj)
        {
            bool canRemove;
            if (SelPurchaseInvoice != null && SelPurchaseInvoiceLine != null)
                canRemove = true;
            else canRemove = false;
            return canRemove;
        }
        #endregion
        #endregion
    }
}
