#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryPlans
{
    [Export(typeof (QueryPlanVM))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class QueryPlanVM : ViewModelBase
    {
        private readonly FleetPlanData _context;
        private readonly IFleetPlanService _service;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public QueryPlanVM(IFleetPlanService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialPlanHistory(); //加载计划
            InitialPlan(); //加载计划
            InitialComparePlan(); //加载比较计划
            InitialCommand(); //初始化命令
        }

        #region 加载计划明细

        /// <summary>
        ///     获取所有计划明细信息。
        /// </summary>
        public QueryableDataServiceCollectionView<PlanHistoryDTO> AllPlanHistories { get; set; }

        /// <summary>
        ///     初始化计划明细信息。
        /// </summary>
        private void InitialPlanHistory()
        {
            AllPlanHistories = new QueryableDataServiceCollectionView<PlanHistoryDTO>(_context, _context.PlanHistories);
            SetIsBusy();
            AllPlanHistories.LoadedData += (sender, e) =>
            {
                SetIsBusy();
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                GetInitialPlanHistory();
            };
        }

        #endregion

        #region 加载计划

        private IEnumerable<PlanHistoryDTO> _planHistories;
        private PlanDTO _selectedPlan;

        /// <summary>
        ///     选择计划。
        /// </summary>
        public PlanDTO SelectedPlan
        {
            get { return _selectedPlan; }
            set
            {
                if (_selectedPlan != value)
                {
                    _selectedPlan = value;
                    GetInitialPlanHistory();
                    CompareCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => SelectedPlan);
                }
            }
        }

        /// <summary>
        ///     获取所有计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> PlansView { get; set; }

        /// <summary>
        ///     原始计划明细
        /// </summary>
        public IEnumerable<PlanHistoryDTO> PlanHistories
        {
            get { return _planHistories; }
            set
            {
                if (_planHistories != value)
                {
                    _planHistories = value;
                    RaisePropertyChanged(() => PlanHistories);
                }
            }
        }

        /// <summary>
        ///     初始化计划信息。
        /// </summary>
        private void InitialPlan()
        {
            PlansView = new QueryableDataServiceCollectionView<PlanDTO>(_context, _context.Plans);
            PlansView.PageSize = 20;
            SetIsBusy();
            PlansView.LoadedData += (sender, e) =>
            {
                SetIsBusy();
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedPlan == null)
                {
                    SelectedPlan = e.Entities.Cast<PlanDTO>().FirstOrDefault();
                }
            };
        }

        #endregion

        #region 加载比较的计划

        private IEnumerable<PlanHistoryDTO> _comparePlanHistories;
        private PlanDTO _selectedComparePlan;

        /// <summary>
        ///     选择比较计划。
        /// </summary>
        public PlanDTO SelectedComparePlan
        {
            get { return _selectedComparePlan; }
            set
            {
                if (_selectedComparePlan != value)
                {
                    _selectedComparePlan = value;
                    GetInitialPlanHistory();
                    CompareCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => SelectedComparePlan);
                }
            }
        }

        /// <summary>
        ///     比较计划明细
        /// </summary>
        public IEnumerable<PlanHistoryDTO> ComparePlanHistories
        {
            get { return _comparePlanHistories; }
            set
            {
                if (_comparePlanHistories != value)
                {
                    _comparePlanHistories = value;
                    RaisePropertyChanged(() => ComparePlanHistories);
                }
            }
        }


        /// <summary>
        ///     获取所有比较计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> ComparePlansView { get; set; }

        /// <summary>
        ///     初始化比较计划信息。
        /// </summary>
        private void InitialComparePlan()
        {
            ComparePlansView = new QueryableDataServiceCollectionView<PlanDTO>(_context, _context.Plans);
            ComparePlansView.PageSize = 20;
            SetIsBusy();
            ComparePlansView.LoadedData += (sender, e) =>
            {
                SetIsBusy();
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedComparePlan == null)
                {
                    SelectedComparePlan = e.Entities.Cast<PlanDTO>().FirstOrDefault();
                }
            };
        }

        #endregion

        #region 获取计划历史

        public DelegateCommand<object> CompareCommand { get; set; }

        /// <summary>
        ///     执行比较命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCompare(object sender)
        {
            var planHistories = new List<PlanHistoryDTO>();
            var comparePlanHistories = new List<PlanHistoryDTO>();
            //遍历原始计划，用原始计划与对比的计划进行对比
            PlanHistories.OrderBy(p => p.Year).ThenBy(p => p.PerformMonth).ToList().ForEach(p =>
            {
                if (
                    ComparePlanHistories.Any(
                        c => c.PlanAircraftId == p.PlanAircraftId && c.ActionCategoryId == p.ActionCategoryId))
                {
                    var comparedPlanHistory =
                        ComparePlanHistories.First(
                            c => c.PlanAircraftId == p.PlanAircraftId && c.ActionCategoryId == p.ActionCategoryId);
                    var clonePlanHistory = p.Clone(); //克隆出一份新的计划历史，用于处理颜色的改变，而不会影响到原有的值
                    var cloneComparedPlanHistory = comparedPlanHistory.Clone(); //克隆出一份新的计划历史，用于处理颜色的改变，而不会影响到原有的值
                    //判断是否发生变更
                    ModifyPlanHistory(clonePlanHistory, cloneComparedPlanHistory);
                    planHistories.Add(clonePlanHistory);
                    comparePlanHistories.Add(cloneComparedPlanHistory);
                }
                else //如果原始计划有计划历史，而对比的计划没有相应的计划历史，则新增一条新的计划历史到计划中
                {
                    var newPlanHistory = new PlanHistoryDTO
                    {
                        PlanHistoryCompareStatus = PlanHistoryCompareStatus.Added
                    };
                    var clonePlanHistory = p.Clone();
                    clonePlanHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Added;
                    planHistories.Add(clonePlanHistory);
                    comparePlanHistories.Add(newPlanHistory);
                }
            });

            //遍历对比计划，用对比的计划与原始的计划进行对比
            ComparePlanHistories.OrderBy(p => p.Year).ThenBy(p => p.PerformMonth).ToList().ForEach(p =>
            {
                if (
                    PlanHistories.All(
                        c => c.PlanAircraftId != p.PlanAircraftId || c.ActionCategoryId != p.ActionCategoryId))
                {
                    var removedPlanHistory = new PlanHistoryDTO
                    {
                        PlanHistoryCompareStatus = PlanHistoryCompareStatus.Removed
                    };
                    var cloneComparedPlanHistory = p.Clone(); //克隆出一份新的计划历史，用于处理颜色的改变，而不会影响到原有的值
                    cloneComparedPlanHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Removed;
                    planHistories.Add(removedPlanHistory);
                    comparePlanHistories.Add(cloneComparedPlanHistory);
                }
            });
            ComparePlanHistories = comparePlanHistories;
            PlanHistories = planHistories;
        }

        /// <summary>
        ///     判断计划历史是否发生变更
        /// </summary>
        /// <param name="planHistory"></param>
        /// <param name="comparedPlanHistory"></param>
        private void ModifyPlanHistory(PlanHistoryDTO planHistory, PlanHistoryDTO comparedPlanHistory)
        {
            if (planHistory.SeatingCapacity != comparedPlanHistory.SeatingCapacity)
            {
                planHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Modified;
            }
            else if (planHistory.PerformMonth != comparedPlanHistory.PerformMonth)
            {
                planHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Modified;
            }
            else if (planHistory.IsValid != comparedPlanHistory.IsValid)
            {
                planHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Modified;
            }
            else if (planHistory.IsSubmit != comparedPlanHistory.IsSubmit)
            {
                planHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Modified;
            }
            else if (planHistory.Note != comparedPlanHistory.Note)
            {
                planHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Modified;
            }
            else if (planHistory.PlanAircraftId != comparedPlanHistory.PlanAircraftId)
            {
                planHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Modified;
            }
            else if (planHistory.PlanId != comparedPlanHistory.PlanId)
            {
                planHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Modified;
            }
            else if (planHistory.ActionCategoryId != comparedPlanHistory.ActionCategoryId)
            {
                planHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Modified;
            }
            else if (planHistory.TargetCategoryId != comparedPlanHistory.TargetCategoryId)
            {
                planHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Modified;
            }
            else if (planHistory.AircraftTypeId != comparedPlanHistory.AircraftTypeId)
            {
                planHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Modified;
            }
            else if (planHistory.AirlinesId != comparedPlanHistory.AirlinesId)
            {
                planHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Modified;
            }
            else if (planHistory.PerformAnnualId != comparedPlanHistory.PerformAnnualId)
            {
                planHistory.PlanHistoryCompareStatus = PlanHistoryCompareStatus.Modified;
            }
            comparedPlanHistory.PlanHistoryCompareStatus = planHistory.PlanHistoryCompareStatus;
        }

        /// <summary>
        ///     判断执行比较命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>执行比较命令是否可用。</returns>
        public bool CanCompare(object sender)
        {
            return SelectedPlan != null && SelectedComparePlan != null
                   && SelectedPlan.AnnualId == SelectedComparePlan.AnnualId;
        }

        /// <summary>
        ///     初始化命令
        /// </summary>
        private void InitialCommand()
        {
            CompareCommand = new DelegateCommand<object>(OnCompare, CanCompare);
        }

        /// <summary>
        ///     加载初始计划历史
        /// </summary>
        private void GetInitialPlanHistory()
        {
            if (SelectedComparePlan != null)
            {
                ComparePlanHistories = AllPlanHistories.Where(p => p.PlanId == SelectedComparePlan.Id);
            }
            if (SelectedPlan != null)
            {
                PlanHistories = AllPlanHistories.Where(p => p.PlanId == SelectedPlan.Id);
            }
        }

        #endregion

        #region 重载基类服务

        public override void LoadData()
        {
            //加载比较计划
            if (!ComparePlansView.AutoLoad)
            {
                ComparePlansView.AutoLoad = true;
            }
            else
            {
                ComparePlansView.Load(true);
            }

            //加载计划
            if (!PlansView.AutoLoad)
            {
                PlansView.AutoLoad = true;
            }
            else
            {
                PlansView.Load(true);
            }

            //加载计划计划明细
            if (!AllPlanHistories.AutoLoad)
            {
                AllPlanHistories.AutoLoad = true;
            }
            else
            {
                AllPlanHistories.Load(true);
            }
        }

        protected override void SetIsBusy()
        {
            if (PlansView == null || ComparePlansView == null || AllPlanHistories == null)
            {
                IsBusy = true;
                return;
            }
            IsBusy = PlansView.IsBusy || ComparePlansView.IsBusy || AllPlanHistories.IsBusy;
        }

        #endregion
    }
}