#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.FleetPlan.PerformFleetPlan
{
    [Export(typeof (QueryPerformPlanVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QueryPerformPlanVM : ViewModelBase
    {
        private readonly FleetPlanData _context;
        private readonly IFleetPlanService _service;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public QueryPerformPlanVM(IFleetPlanService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialPlan(); //加载计划
        }

        #region 加载计划

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
                    AnalysePlanPerforms(value);
                    RaisePropertyChanged(() => SelectedPlan);
                }
            }
        }

        /// <summary>
        ///     获取所有计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> PlansView { get; set; }

        /// <summary>
        ///     初始化计划信息。
        /// </summary>
        private void InitialPlan()
        {
            PlansView = _service.CreateCollection(_context.Plans);
            _service.RegisterCollectionView(PlansView);
            PlansView.PageSize = 20;
            PlansView.FilterDescriptors.Add(new FilterDescriptor("IsValid",FilterOperator.IsEqualTo,true));
            PlansView.LoadedData += (sender, e) =>
            {
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

        #region 属性

        private string _performPlanHeader = "当前计划完成情况（单位：%）";

        private decimal _performance;

        /// <summary>
        ///     计划完成情况标题
        /// </summary>
        public string PerformPlanHeader
        {
            get { return _performPlanHeader; }
            set
            {
                if (_performPlanHeader != value)
                {
                    _performPlanHeader = value;
                    RaisePropertyChanged(() => PerformPlanHeader);
                }
            }
        }

        /// <summary>
        ///     当前年度计划执行情况
        /// </summary>
        public decimal Performance
        {
            get { return _performance; }
            set
            {
                if (_performance != value)
                {
                    _performance = value;
                    RaisePropertyChanged(() => Performance);
                }
            }
        }

        #endregion

        #region 计算计划执行情况

        /// <summary>
        ///     分析计划完成情况
        /// </summary>
        /// <param name="currentPlan">当前计划</param>
        public void AnalysePlanPerforms(PlanDTO currentPlan)
        {
            if (currentPlan==null)
            {
                PerformPlanHeader = "当前计划完成情况（单位：%）";
                return;
            }
            if (currentPlan.PlanHistories.Count == 0)
            {
                Performance = 0;
            }
            else
            {
                //已经执行的计划，包括计划历史与运营历史
                Func<PlanHistoryDTO, bool> exprOperationPlanImportAndExport = p => p.RelatedGuid != null
                                                                                   &&
                                                                                   ((p.RelatedStartDate != null
                                                                                     &&
                                                                                     p.RelatedStartDate.Value.Year ==
                                                                                     currentPlan.Year)
                                                                                    ||
                                                                                    (p.RelatedEndDate != null &&
                                                                                     p.RelatedEndDate.Value.Year ==
                                                                                     currentPlan.Year));
               
                //某年实际执行条数
                decimal performedCount = currentPlan.PlanHistories.Count(exprOperationPlanImportAndExport);
                //计划执行
                decimal planPerformCount = currentPlan.PlanHistories.Count(p => p.Year == currentPlan.Year);
                //统计执行百分比
                Performance = planPerformCount == 0 ? 100 : Math.Round(performedCount * 100 / planPerformCount, 2);
            }
            PerformPlanHeader = currentPlan.Year + "年度计划完成情况（执行率：" + Convert.ToString(Performance) + "%）";
        }

        #endregion

        #region 重载基类服务

        public override void LoadData()
        {
            //加载计划
            if (!PlansView.AutoLoad)
            {
                PlansView.AutoLoad = true;
            }
            else
            {
                PlansView.Load(true);
            }
        }

        #endregion
    }
}