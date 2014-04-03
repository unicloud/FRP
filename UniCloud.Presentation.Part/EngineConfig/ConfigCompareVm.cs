#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/23 18:06:46
// 文件名：ConfigCompareVm
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.EngineConfig
{
    [Export(typeof(ConfigCompareVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ConfigCompareVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;
        private PartData _context;

        [ImportingConstructor]
        public ConfigCompareVm(IRegionManager regionManager, IPartService service)
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
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
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
            LoadConfigGroups();
        }

        #region 业务

        #region 构型组集合

        private List<ConfigGroupDTO> _configGroups;

        /// <summary>
        /// 构型组集合
        /// </summary>
        public List<ConfigGroupDTO> ConfigGroups
        {
            get { return this._configGroups; }
            private set
            {
                if (this._configGroups != value)
                {
                    _configGroups = value;
                    this.RaisePropertyChanged(() => this.ConfigGroups);
                }
            }
        }

        #endregion
        #endregion

        #endregion

        #endregion

        #region 操作

        public void LoadConfigGroups()
        {
            var path = CreateConfigGroupsQueryUri();
            IsBusy = true;
            _context.BeginExecute<ConfigGroupDTO>(path,
               result => Deployment.Current.Dispatcher.BeginInvoke(() =>
               {
                   var context = result.AsyncState as PartData;
                   try
                   {
                       if (context != null)
                       {
                           ConfigGroups = context.EndExecute<ConfigGroupDTO>(result).ToList();
                       }
                   }
                   catch (DataServiceQueryException ex)
                   {
                       QueryOperationResponse response = ex.Response;
                       MessageAlert(response.Error.Message);
                   }
                   IsBusy = false;
               }), _context);
        }

        private Uri CreateConfigGroupsQueryUri()
        {
            return new Uri(string.Format("GetConfigGroups"),
                UriKind.Relative);
        }
        #endregion
    }
}