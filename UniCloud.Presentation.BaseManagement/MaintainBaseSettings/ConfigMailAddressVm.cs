#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/13 10:49:37
// 文件名：ConfigMailAddressVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/13 10:49:37
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.BaseManagement.MaintainBaseSettings
{
    [Export(typeof (ConfigMailAddressVm))]
    public class ConfigMailAddressVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IFleetPlanService _service;
        private readonly CompositeFilterDescriptor cfd = new CompositeFilterDescriptor { LogicalOperator = FilterCompositionLogicalOperator.Or };
       
        [ImportingConstructor]
        public ConfigMailAddressVm(IFleetPlanService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
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
            MailAddresses = _service.CreateCollection(_context.MailAddresses);
            cfd.FilterDescriptors.Add(new FilterDescriptor("Id", FilterOperator.IsEqualTo, Guid.Parse("1978ADFC-A2FD-40CC-9A26-6DEDB55C335F")));
            cfd.FilterDescriptors.Add(new FilterDescriptor("Id", FilterOperator.IsEqualTo, Guid.Parse("31A9DE51-C207-4A73-919C-21521F17FEF9")));
            MailAddresses.LoadedData += (o, e) =>
            {
                MailAddress = MailAddresses.FirstOrDefault();

                if (MailAddresses.SourceCollection.Cast<MailAddressDTO>().Count() != 0)
                {
                    MailAddress = MailAddresses.SourceCollection.Cast<MailAddressDTO>().FirstOrDefault(p => p.Id == Guid.Parse("1978ADFC-A2FD-40CC-9A26-6DEDB55C335F"));
                    CAACMailAddress = MailAddresses.SourceCollection.Cast<MailAddressDTO>().FirstOrDefault(p => p.Id == Guid.Parse("31A9DE51-C207-4A73-919C-21521F17FEF9"));
                }
                IsReadOnly = true;
                RaisePropertyChanged(()=>IsReadOnly);
                RefreshCommandState();
            };
            _service.RegisterCollectionView(MailAddresses);

            EditCommand = new DelegateCommand<object>(OnEdit, CanEdit);
        }

        #endregion

        #region 数据

        #region 公共属性

        private bool _isReadOnly;

        /// <summary>
        /// 界面只读控制
        /// </summary>
        public bool IsReadOnly
        {
            get { return this._isReadOnly; }
            private set
            {
                if (this._isReadOnly != value)
                {
                    this._isReadOnly = value;
                    this.RaisePropertyChanged(() => this.IsReadOnly);
                }
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    if (!_confirmPassword.Equals(MailAddress.LoginPassword))
                    {
                        throw new Exception("密码不一致！");
                    }
                    RaisePropertyChanged(() => ConfirmPassword);
                }
            }
        }

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
            MailAddresses.Load(true);
        }

        #region 邮箱账号

        private MailAddressDTO _mailAddress;
        private MailAddressDTO _caacMailAddress;

        /// <summary>
        ///     邮箱账号集合
        /// </summary>
        public QueryableDataServiceCollectionView<MailAddressDTO> MailAddresses { get; set; }

        /// <summary>
        ///     航空公司发送邮箱账号
        /// </summary>
        public MailAddressDTO MailAddress
        {
            get { return _mailAddress; }
            set
            {
                if (_mailAddress != value)
                {
                    _mailAddress = value;
                    RaisePropertyChanged(() => MailAddress);
                }
            }
        }

        /// <summary>
        ///     民航局接收邮箱账号
        /// </summary>
        public MailAddressDTO CAACMailAddress
        {
            get { return _caacMailAddress; }
            set
            {
                if (_caacMailAddress != value)
                {
                    _caacMailAddress = value;
                    RaisePropertyChanged(() => CAACMailAddress);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 刷新按钮状态

        protected override void RefreshCommandState()
        {
            EditCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 重载操作

        #endregion

        #region 编辑邮箱账号

        /// <summary>
        ///     创建新申请
        /// </summary>
        public DelegateCommand<object> EditCommand { get; private set; }

        private void OnEdit(object obj)
        {
            IsReadOnly = false;
            RaisePropertyChanged(()=>IsReadOnly);
            if (MailAddress == null)
            {
                MailAddress = new MailAddressDTO()
                {
                    Id = Guid.Parse("1978ADFC-A2FD-40CC-9A26-6DEDB55C335F"),
                    SendPort = 25,
                    SendSSL = false,
                    StartTLS = true,
                };
                MailAddresses.AddNew(MailAddress);
            }
            if (CAACMailAddress == null)
            {
                CAACMailAddress = new MailAddressDTO()
                {
                    Id = Guid.Parse("31A9DE51-C207-4A73-919C-21521F17FEF9"),
                    ReceivePort = 110,
                    ReceiveSSL = false,
                    SendPort = 25,
                };
                MailAddresses.AddNew(CAACMailAddress);
            }
            RefreshCommandState();
        }

        private bool CanEdit(object obj)
        {
            return IsReadOnly;
        }

        #endregion

        #endregion
    }
}