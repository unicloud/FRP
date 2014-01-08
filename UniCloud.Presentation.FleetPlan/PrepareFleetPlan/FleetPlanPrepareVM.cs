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
    [Export(typeof(FleetPlanPrepareVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FleetPlanPrepareVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;
        private AnnualDTO _curAnnual;
        private AirlinesDTO _curAirlines;
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
            ViewAnnuals = _service.CreateCollection(_context.Annuals.Expand(p => p.Plans), o => o.Plans);
            ViewAnnuals.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelAnnual == null)
                {
                    SelAnnual = e.Entities.Cast<AnnualDTO>().FirstOrDefault(p => p.IsOpen);
                }
                _curAnnual = e.Entities.Cast<AnnualDTO>().FirstOrDefault(p => p.IsOpen);
                RefreshCommandState();
            };
            _service.RegisterCollectionView(ViewAnnuals);//注册查询集合

            //TODO：初始化当前航空公司
            _curAirlines = new AirlinesDTO
            {
                Id = Guid.Parse("1978ADFC-A2FD-40CC-9A26-6DEDB55C335F"),
                CnName = "四川航空股份有限公司",
                CnShortName = "川航",
            };
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
            ViewAnnuals.AutoLoad = true;
        }

        #region 业务

        #region 计划年度集合

        /// <summary>
        ///     计划年度集合
        /// </summary>
        public QueryableDataServiceCollectionView<AnnualDTO> ViewAnnuals { get; set; }

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
                    ViewPlans.Clear();
                    foreach (var plan in value.Plans)
                    {
                        ViewPlans.Add(plan);
                    }
                    SelPlan = ViewPlans.FirstOrDefault();

                    RaisePropertyChanged(() => SelAnnual);
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
                    PlanHistories.Clear();
                    foreach (var ph in value.PlanHistories)
                    {
                        PlanHistories.Add(ph);
                    }
                    RaisePropertyChanged(() => SelPlan);
                }
            }
        }

        #endregion

        #region 计划明细集合
        private ObservableCollection<PlanHistoryDTO> _planHistories = new ObservableCollection<PlanHistoryDTO>();

        /// <summary>
        ///     计划明细集合
        /// </summary>
        public ObservableCollection<PlanHistoryDTO> PlanHistories
        {
            get { return _planHistories; }
            private set
            {
                if (_planHistories != value)
                {
                    _planHistories = value;
                    RaisePropertyChanged(() => PlanHistories);
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
            var newAnnual = ViewAnnuals.FirstOrDefault(a => a.Year == _curAnnual.Year + 1);
            if (newAnnual == null || _curAnnual == null)
            {
                MessageAlert("年度不能为空！");
                return;
            }
            //获取当前计划作为创建新版本计划的上一版本计划
            PlanDTO lastPlan = _curAnnual.Plans.OrderBy(p => p.VersionNumber).LastOrDefault();

            //设置当前打开年度
            newAnnual.IsOpen = true;
            _curAnnual.IsOpen = false;

            //刷新_service中的静态属性CurrentAnnual
            _curAnnual = ViewAnnuals.SingleOrDefault(p=>p.IsOpen);

            //创建新年度的第一版本计划
            newAnnual.Plans.Add(_service.CreateNewYearPlan(lastPlan, newAnnual.Id, newAnnual.Year, _curAirlines));

            SelAnnual = _curAnnual;

            RefreshCommandState();
        }

        private bool CanUnLock(object obj)
        {
            // 当前月份在允许打开新计划年度的范围内且当前年度在当前打开年度，按钮可用。
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