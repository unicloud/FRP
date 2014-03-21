#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/11 15:23:19
// 文件名：ManageFunctionsInRoleVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/11 15:23:19
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export(typeof(ManageFunctionsInRoleVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageFunctionsInRoleVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly BaseManagementData _context;
        private readonly IRegionManager _regionManager;
        private readonly IBaseManagementService _service;

        [ImportingConstructor]
        public ManageFunctionsInRoleVm(IRegionManager regionManager, IBaseManagementService service)
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
            //创建并注册CollectionView
            FunctionItems = _service.CreateCollection(_context.FunctionItems);
            FunctionItems.LoadedData += (o, e) =>
                                        {
                                            FunctionItems.ToList().ForEach(GenerateFunctionItemStructure);
                                            Applications = FunctionItems.Where(p => p.ParentItemId == null).ToList();
                                        };
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
            //// 将CollectionView的AutoLoad属性设为True
            if (!FunctionItems.AutoLoad)
            {
                FunctionItems.AutoLoad = true;
            }
        }

        #region 功能菜单
        public QueryableDataServiceCollectionView<FunctionItemDTO> FunctionItems { get; set; }

        private ObservableCollection<FunctionItemDTO> _functionItemStructures = new ObservableCollection<FunctionItemDTO>();
        public ObservableCollection<FunctionItemDTO> FunctionItemStructures
        {
            get { return _functionItemStructures; }
            set
            {
                _functionItemStructures = value;
                RaisePropertyChanged(() => FunctionItemStructures);
            }
        }
        #endregion

        #region 应用集合
        /// <summary>
        /// 应用集合
        /// </summary>
        private List<FunctionItemDTO> _applications;
        public List<FunctionItemDTO> Applications
        {
            get { return _applications; }
            set
            {
                _applications = value;
                RaisePropertyChanged(() => Applications);
            }
        }

        /// <summary>
        /// 选中的应用
        /// </summary>
        private FunctionItemDTO _application;
        public FunctionItemDTO Application
        {
            get { return _application; }
            set
            {
                _application = value;
                if (_application != null)
                {
                    FunctionItemStructures.Clear();
                    FunctionItemStructures.Add(_application);
                }
                RaisePropertyChanged(() => Application);
            }
        }
        #endregion

        #endregion

        #endregion

        #region 操作

        #region 重组成有层次结构的菜单

        private void GenerateFunctionItemStructure(FunctionItemDTO functionItem)
        {
            var temp = FunctionItems.Where(p => p.ParentItemId == functionItem.Id).ToList().OrderBy(p => p.Sort);
            functionItem.SubFunctionItems.Load(temp);
            foreach (var subItem in functionItem.SubFunctionItems)
            {
                GenerateFunctionItemStructure(subItem);
            }
        }
        #endregion


        #endregion
    }
}
