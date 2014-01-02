﻿#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/25 13:50:27
// 文件名：FleetPlanPrepareVM
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
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export(typeof (FleetPlanPrepareVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FleetPlanPrepareVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;

        [ImportingConstructor]
        public FleetPlanPrepareVM(IRegionManager regionManager, IFleetPlanService service) : base(service)
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
            Annuals = _service.CreateCollection(_context.Annuals.Expand(p => p.Plans));
            _service.RegisterCollectionView(Annuals);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            UnlockCommand = new DelegateCommand<object>(OnUnLock, CanUnLock);
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
            Annuals.Load(true);
        }

        #region 业务

        #region 计划年度集合

        /// <summary>
        ///     计划年度集合
        /// </summary>
        public QueryableDataServiceCollectionView<AnnualDTO> Annuals { get; set; }

        #endregion

        #region 选择的年度

        private AnnualDTO _selAnnual;

        /// <summary>
        ///     选择的规划
        /// </summary>
        public AnnualDTO SelAnnual
        {
            get { return _selAnnual; }
            private set
            {
                if (_selAnnual != value)
                {
                    _selAnnual = value;
                    RaisePropertyChanged(() => SelAnnual);
                    _selPlan = value.Plans.FirstOrDefault();
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的计划

        private PlanDTO _selPlan;

        /// <summary>
        ///     选择的规划明细
        /// </summary>
        public PlanDTO SelPlan
        {
            get { return _selPlan; }
            private set
            {
                if (_selPlan != value)
                {
                    _selPlan = value;
                    RaisePropertyChanged(() => SelPlan);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 刷新按钮状态

        protected override void RefreshCommandState()
        {
            UnlockCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 打开新的计划年度

        /// <summary>
        ///     打开新的计划年度
        /// </summary>
        public DelegateCommand<object> UnlockCommand { get; private set; }

        private void OnUnLock(object obj)
        {
            var plan = new PlanDTO
            {
                Id = new Guid(),
                Title = "初始化计划",
                VersionNumber = 1,
                PlanHistories = new ObservableCollection<PlanHistoryDTO>
                {
                    new PlanHistoryDTO
                    {
                        Id = new Guid(),
                        PerformMonth = 11,
                    }
                }
            };
            _selAnnual.IsOpen = true;
            _selAnnual.Plans.Add(plan);
            RefreshCommandState();
        }

        private bool CanUnLock(object obj)
        {
            return true;
        }

        #endregion

        #endregion
    }
}