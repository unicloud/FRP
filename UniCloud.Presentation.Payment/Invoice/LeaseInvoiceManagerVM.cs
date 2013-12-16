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
            LeaseInvoices.AutoLoad = true;
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
                    _leaseInvoiceLines.Clear();
                    foreach (var invoiceLine in value.InvoiceLines)
                    {
                        LeaseInvoiceLines.Add(invoiceLine);
                    }
                    RaisePropertyChanged(() => SelLeaseInvoice);
                }
            }
        }

        #endregion

        #region 租赁发票行

        private ObservableCollection<LeaseInvoiceLineDTO> _leaseInvoiceLines=new ObservableCollection<LeaseInvoiceLineDTO>();

        /// <summary>
        ///     租赁发票行
        /// </summary>
        public ObservableCollection<LeaseInvoiceLineDTO> LeaseInvoiceLines
        {
            get { return _leaseInvoiceLines; }
            private set
            {
                if (_leaseInvoiceLines != value)
                {
                    _leaseInvoiceLines = value;
                    RaisePropertyChanged(() => LeaseInvoiceLines);
                }
            }
        }

        #endregion

        #region 选择的租赁发票行

        private LeaseInvoiceLineDTO _selLeaseInvoiceLine;

        /// <summary>
        ///     选择的租赁发票行
        /// </summary>
        public LeaseInvoiceLineDTO SelLeaseInvoiceLine
        {
            get { return _selLeaseInvoiceLine; }
            set
            {
                if (_selLeaseInvoiceLine != value)
                {
                    _selLeaseInvoiceLine = value;
                    RaisePropertyChanged(() => SelLeaseInvoiceLine);
                }
            }
        }

        #endregion
        #endregion

        #endregion

        #endregion

        #region 重载操作

        #region 新建租赁发票

        /// <summary>
        ///     新建租赁发票
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var invoice = new LeaseInvoiceDTO
            {
                LeaseInvoiceId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
            };
            LeaseInvoices.AddNew(invoice);
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
                LeaseInvoiceLines.Clear();
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
            var invoiceLine = new LeaseInvoiceLineDTO
            {
                LeaseInvoiceLineId = RandomHelper.Next(),
                InvoiceId = SelLeaseInvoice.LeaseInvoiceId
            };
            SelLeaseInvoice.InvoiceLines.Add(invoiceLine);
            LeaseInvoiceLines.Add(invoiceLine);
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
            SelLeaseInvoice.InvoiceLines.Remove(SelLeaseInvoiceLine);
            LeaseInvoiceLines.Remove(SelLeaseInvoiceLine);
        }

        private bool CanRemove(object obj)
        {
            bool canRemove;
            if (SelLeaseInvoice != null && SelLeaseInvoiceLine != null)
                canRemove = true;
            else canRemove = false;
            return canRemove;
        }
        #endregion
        #endregion
    }
}
