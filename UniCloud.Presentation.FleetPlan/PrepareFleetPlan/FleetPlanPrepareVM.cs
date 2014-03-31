#region 版本信息

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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export(typeof(FleetPlanPrepareVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FleetPlanPrepareVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;
        private AnnualDTO _curAnnual = new AnnualDTO();
        private bool _loadedAnnuals;
        private bool _loadedPlans;

        [ImportingConstructor]
        public FleetPlanPrepareVM(IRegionManager regionManager, IFleetPlanService service)
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
            Annuals = _service.CreateCollection(_context.Annuals);
            var sort = new SortDescriptor { Member = "Year", SortDirection = ListSortDirection.Descending };
            Annuals.SortDescriptors.Add(sort);
            var group = new GroupDescriptor { Member = "ProgrammingName", SortDirection = ListSortDirection.Descending };
            Annuals.GroupDescriptors.Add(group);
            Annuals.LoadedData += (sender, e) =>
            {
                _curAnnual = e.Entities.Cast<AnnualDTO>().FirstOrDefault(p => p.IsOpen);
                _loadedAnnuals = true;
                SetSelAnnual();
                RefreshCommandState();
            };
            _service.RegisterCollectionView(Annuals);//注册查询集合

            AllPlans = _service.CreateCollection(_context.Plans);
            AllPlans.LoadedData += (sender, e) =>
            {
                _loadedPlans = true;
                SetSelAnnual();
            };
            _service.RegisterCollectionView(AllPlans);

            AllPlanHistories = _service.CreateCollection(_context.PlanHistories);
            AllPlanHistories.LoadedData += (sender, e) => RaisePropertyChanged(() => SelPlan);
            _service.RegisterCollectionView(AllPlanHistories);

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
            _service.GetActionCategories(() => { });
            _service.GetAircraftCategories(() => { });
            _service.GetAircraftTypes(() => { });

            _loadedAnnuals = false;
            _loadedPlans = false;
            if (!AllPlans.AutoLoad)
                AllPlans.AutoLoad = true;
            else
                AllPlans.Load(true);

            if (!AllPlanHistories.AutoLoad)
                AllPlanHistories.AutoLoad = true;
            else
                AllPlanHistories.Load(true);

            if (!Annuals.AutoLoad)
                Annuals.AutoLoad = true;
            else
                Annuals.Load(true);
        }

        public void SetSelAnnual()
        {
            if (_loadedAnnuals && _loadedPlans)
            {
                SelAnnual = _curAnnual;
            }
        }

        #region 业务

        #region 所有计划年度集合

        /// <summary>
        ///     计划年度集合
        /// </summary>
        public QueryableDataServiceCollectionView<AnnualDTO> Annuals { get; set; }

        #endregion

        #region 所有运力增减计划集合

        /// <summary>
        ///     所有运力增减计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> AllPlans { get; set; }

        #endregion

        #region 选择的年度

        private AnnualDTO _selAnnual = new AnnualDTO();

        /// <summary>
        ///     选择的年度
        /// </summary>
        public AnnualDTO SelAnnual
        {
            get { return _selAnnual; }
            private set
            {
                if (_selAnnual != value)
                {
                    _selAnnual = value;
                    ViewPlans.Clear();
                    foreach (var plan in AllPlans.ToList())
                    {
                        if (plan.Year == value.Year)
                            ViewPlans.Add(plan);
                    }
                    ViewPlans.OrderBy(p => p.VersionNumber);
                    SelPlan = ViewPlans.FirstOrDefault();
                    RaisePropertyChanged(() => SelAnnual);
                }
            }
        }

        #endregion

        #region 选择年度内的计划集合

        private ObservableCollection<PlanDTO> _viewPlans = new ObservableCollection<PlanDTO>();

        /// <summary>
        ///     选择年度内的计划集合
        /// </summary>
        public ObservableCollection<PlanDTO> ViewPlans
        {
            get { return _viewPlans; }
            private set
            {
                if (_viewPlans != value)
                {
                    _viewPlans = value;
                    RaisePropertyChanged(() => ViewPlans);
                }
            }
        }

        #endregion

        #region 计划明细集合

        /// <summary>
        ///     所有计划明细集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanHistoryDTO> AllPlanHistories { get; set; }

        private ObservableCollection<PlanHistoryDTO> _viewPlanHistories = new ObservableCollection<PlanHistoryDTO>();

        /// <summary>
        ///     选择计划的计划明细集合
        /// </summary>
        public ObservableCollection<PlanHistoryDTO> ViewPlanHistories
        {
            get { return _viewPlanHistories; }
            private set
            {
                if (_viewPlanHistories != value)
                {
                    _viewPlanHistories = value;
                    RaisePropertyChanged(() => ViewPlanHistories);
                }
            }
        }
        #endregion

        #region 选择的计划

        private PlanDTO _selPlan;

        /// <summary>
        ///     选择的计划
        /// </summary>
        public PlanDTO SelPlan
        {
            get { return _selPlan; }
            private set
            {
                if (_selPlan != value)
                {
                    _selPlan = value;
                    ViewPlanHistories.Clear();
                    if (value != null)
                    {
                        ViewPlanHistories.Clear();
                        foreach (var ph in AllPlanHistories.SourceCollection.Cast<PlanHistoryDTO>())
                        {
                            if (ph.PlanId == value.Id)
                                ViewPlanHistories.Add(ph);
                        }
                        RaisePropertyChanged(() => ViewPlanHistories);
                    }

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

        #region 重写保存前操作

        protected override bool OnSaveExecuting(object sender)
        {
            var ph = AllPlanHistories.ToList();
            ph.ForEach(p =>
            {
                p.ActionCategories.Clear();
                p.AircraftTypes.Clear();
                p.AircraftCategories.Clear();
            });
            return true;
        }

        #endregion

        #region 打开新的计划年度

        /// <summary>
        ///     打开新的计划年度
        /// </summary>
        public DelegateCommand<object> UnlockCommand { get; private set; }

        private void OnUnLock(object obj)
        {
            var newAnnual = Annuals.SourceCollection.Cast<AnnualDTO>().FirstOrDefault(a => a.Year == _curAnnual.Year + 1);
            if (newAnnual == null || _curAnnual == null)
            {
                MessageAlert("年度不能为空！");
                return;
            }
            //获取当前计划作为创建新版本计划的上一版本计划
            PlanDTO lastPlan = AllPlans.Where(p => p.Year == _curAnnual.Year).OrderBy(p => p.VersionNumber).LastOrDefault();

            //设置当前打开年度
            newAnnual.IsOpen = true;
            _curAnnual.IsOpen = false;

            //刷新_service中的静态属性CurrentAnnual
            _curAnnual = Annuals.SourceCollection.Cast<AnnualDTO>().SingleOrDefault(p => p.IsOpen);

            //创建新年度的第一版本计划
            AllPlans.AddNew(_service.CreateNewYearPlan(lastPlan, AllPlanHistories, newAnnual));

            SelAnnual = _curAnnual;

            RefreshCommandState();
        }

        private bool CanUnLock(object obj)
        {
            //当前月份在允许打开新计划年度的范围内且当前年度在当前打开年度，按钮可用。
            if (_curAnnual != null)
            {
                if (DateTime.Now.Month > 12 - 3)
                    return _curAnnual.Year == DateTime.Now.Year;
                else return _curAnnual.Year == DateTime.Now.Year - 1;
            }
            return false;
        }

        #endregion

        #endregion
    }
}