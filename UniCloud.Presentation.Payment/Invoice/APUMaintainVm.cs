#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/13 9:45:49
// 文件名：APUMaintainVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/13 9:45:49
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
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(APUMaintainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class APUMaintainVm: EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private PaymentData _paymentData;
        [Import]
        public DocumentViewer DocumentView;

        [ImportingConstructor]
        public APUMaintainVm(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            AddMaintainInvoiceCommand = new DelegateCommand<object>(OnAddMaintainInvoice, CanAddMaintainInvoice);
            RemoveMaintainInvoiceCommand = new DelegateCommand<object>(OnRemoveMaintainInvoice, CanRemoveMaintainInvoice);
            AddMaintainInvoiceLineCommand = new DelegateCommand<object>(OnAddMaintainInvoiceLine, CanAddMaintainInvoiceLine);
            RemoveMaintainInvoiceLineCommand = new DelegateCommand<object>(OnRemoveMaintainInvoiceLine, CanRemoveMaintainInvoiceLine);

            InitializeVm();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVm()
        {
            // 创建并注册CollectionView
            ApuMaintainInvoices = Service.CreateCollection(_paymentData.APUMaintainInvoices);
            Service.RegisterCollectionView(ApuMaintainInvoices);
            ApuMaintainInvoices.PropertyChanged += OnViewPropertyChanged;
            //ApuMaintainInvoices.PropertyChanged += (sender, e) =>
            //{
            //    if (e.PropertyName == "HasChanges")
            //    {
            //        CanSelectApuMaintain = !ApuMaintainInvoices.HasChanges;
            //    }
            //};
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
            // 将CollectionView的AutoLoad属性设为True
            ApuMaintainInvoices.AutoLoad = true;
        }


        #region APU维修发票
        /// <summary>
        /// APU维修发票集合
        /// </summary>
        public QueryableDataServiceCollectionView<APUMaintainInvoiceDTO> ApuMaintainInvoices { get; set; }

        private APUMaintainInvoiceDTO _apuMaintainInvoice;
        /// <summary>
        /// 选中的APU维修发票
        /// </summary>
        public APUMaintainInvoiceDTO ApuMaintainInvoice
        {
            get { return _apuMaintainInvoice; }
            set
            {
                if (_apuMaintainInvoice != value)
                {
                    _apuMaintainInvoice = value;
                    if (_apuMaintainInvoice != null)
                    {
                        if (value.Suppliers != null)
                        {
                            _supplier = value.Suppliers.FirstOrDefault(p => p.SupplierId == _apuMaintainInvoice.SupplierId);
                        }
                    }
                    RaisePropertyChanged(() => ApuMaintainInvoice);
                }
            }
        }

        private MaintainInvoiceLineDTO _apuMaintainInvoiceLine;
        /// <summary>
        /// 选中的APU维修发票
        /// </summary>
        public MaintainInvoiceLineDTO ApuMaintainInvoiceLine
        {
            get { return _apuMaintainInvoiceLine; }
            set
            {
                if (_apuMaintainInvoiceLine != value)
                {
                    _apuMaintainInvoiceLine = value;
                    RaisePropertyChanged(() => ApuMaintainInvoiceLine);
                }
            }
        }

        private bool _canSelectApuMaintain = true;
        //用户能否选择
        public bool CanSelectApuMaintain
        {
            get { return _canSelectApuMaintain; }
            set
            {
                if (_canSelectApuMaintain != value)
                {
                    _canSelectApuMaintain = value;
                    RaisePropertyChanged(() => CanSelectApuMaintain);
                }
            }
        }
        #endregion

        #region 签约对象
        private SupplierDTO _supplier;
        /// <summary>
        /// 选中的签约对象
        /// </summary>
        public SupplierDTO Supplier
        {
            get { return _supplier; }
            set
            {
                if (value != null && _supplier != value)
                {
                    _supplier = value;
                    ApuMaintainInvoice.SupplierName = _supplier.Name;
                    RaisePropertyChanged(() => Supplier);
                }
            }
        }
        #endregion

        #endregion

        #endregion

        #region 操作

        #region 重载操作


        #endregion

        #region 创建新维修发票

        /// <summary>
        ///  创建新维修发票
        /// </summary>
        public DelegateCommand<object> AddMaintainInvoiceCommand { get;  set; }

        private void OnAddMaintainInvoice(object obj)
        {
            var maintainInvoice = new APUMaintainInvoiceDTO
                                  {
                                      APUMaintainInvoiceId = RandomHelper.Next(),
                                      CreateDate = DateTime.Now,
                                      InvoiceDate = DateTime.Now
                                  };
            ApuMaintainInvoices.AddNew(maintainInvoice);
        }

        private bool CanAddMaintainInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 删除维修发票

        /// <summary>
        ///     删除维修发票
        /// </summary>
        public DelegateCommand<object> RemoveMaintainInvoiceCommand { get;  set; }

        private void OnRemoveMaintainInvoice(object obj)
        {
            if (_apuMaintainInvoice != null)
            {
                ApuMaintainInvoices.Remove(_apuMaintainInvoice);
            }
        }

        private bool CanRemoveMaintainInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 增加维修发票行

        /// <summary>
        ///     增加维修发票行
        /// </summary>
        public DelegateCommand<object> AddMaintainInvoiceLineCommand { get;  set; }

        private void OnAddMaintainInvoiceLine(object obj)
        {
            var maintainInvoiceLine = new MaintainInvoiceLineDTO
            {
                Amount = 1,
            };

            ApuMaintainInvoice.MaintainInvoiceLines.Add(maintainInvoiceLine);
        }

        private bool CanAddMaintainInvoiceLine(object obj)
        {
            return true;
        }

        #endregion

        #region 移除维修发票行

        /// <summary>
        ///     移除维修发票行
        /// </summary>
        public DelegateCommand<object> RemoveMaintainInvoiceLineCommand { get; private set; }

        private void OnRemoveMaintainInvoiceLine(object obj)
        {
            ApuMaintainInvoice.MaintainInvoiceLines.Remove(ApuMaintainInvoiceLine);
        }

        private bool CanRemoveMaintainInvoiceLine(object obj)
        {
            return true;
        }

        #endregion
        #endregion

        private Array values = Enum.GetValues(typeof(MaintainItem));
        public Array Values
        {
            get { return values; }
            set
            {
                if (value != null && values != value)
                {
                    values = value;
                    RaisePropertyChanged(() => Values);
                }
            }

        }
    }

}
