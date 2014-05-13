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
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.BaseManagement.MaintainBaseSettings
{
    [Export(typeof(ConfigMailAddressVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ConfigMailAddressVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;

        [ImportingConstructor]
        public ConfigMailAddressVm(IRegionManager regionManager, IFleetPlanService service)
            : base(service)
        {
            _regionManager = regionManager;
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
            MailAddresses.LoadedData += (o, e) =>
                                        {
                                            MailAddress = MailAddresses.FirstOrDefault();
                                            if (MailAddress == null)
                                            {
                                                MailAddress = new MailAddressDTO {SendPort = 25};
                                                MailAddresses.AddNewItem(MailAddress);
                                            }
                                        };
            _service.RegisterCollectionView(MailAddresses);

        }

        #endregion

        #region 数据

        #region 公共属性
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
        /// <summary>
        ///     邮箱账号集合
        /// </summary>
        public QueryableDataServiceCollectionView<MailAddressDTO> MailAddresses { get; set; }

        private MailAddressDTO _mailAddress;
        /// <summary>
        ///     选中的邮箱账号
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

        #endregion

        #endregion

        #endregion

        #region 操作
        #region 重载操作

        #endregion

        #endregion

    }
}
