#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.FleetPlan.PerformFleetPlan
{
    [Export(typeof(QueryPerformPlanVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QueryPerformPlanVM : ViewModelBase
    {
        private readonly FleetPlanData _context;
        private readonly IFleetPlanService _service;
        private FilterDescriptor _planHistoryDescriptor;

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
            InitialPlanHistory();//加载计划明细
            InitialCommand();
        }

        #region 加载计划

        private IEnumerable<PlanHistoryDTO> _planHistories;
        private PlanDTO _selectedPlan;

        private PlanHistoryDTO _selectedPlanHistory;

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
                    GetPerformPlanHistory();
                    RaisePropertyChanged(() => SelectedPlan);
                }
            }
        }

        public PlanHistoryDTO SelectedPlanHistory
        {
            get { return _selectedPlanHistory; }
            set
            {
                if (_selectedPlanHistory != value)
                {
                    _selectedPlanHistory = value;
                    RaisePropertyChanged(() => SelectedPlanHistory);
                }
            }
        }

        /// <summary>
        ///     获取所有计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> PlansView { get; set; }

        /// <summary>
        /// 获取所选计划的计划明细集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanHistoryDTO> SelectedPlanHistories { get; set; }


        public IEnumerable<PlanHistoryDTO> PlanHistories
        {
            get { return _planHistories; }
            set
            {
                _planHistories = value;
                RaisePropertyChanged(() => PlanHistories);
            }
        }

        /// <summary>
        ///     初始化计划信息。
        /// </summary>
        private void InitialPlan()
        {
            PlansView = _service.CreateCollection(_context.Plans);
            _service.RegisterCollectionView(PlansView);
            PlansView.PageSize = 20;
            PlansView.FilterDescriptors.Add(new FilterDescriptor("IsValid", FilterOperator.IsEqualTo, true));
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
                    if (SelectedPlan != null)
                    {
                        _planHistoryDescriptor.Value = SelectedPlan.Id;
                        if (!SelectedPlanHistories.AutoLoad)
                        {
                            SelectedPlanHistories.AutoLoad = true;
                        }
                        else
                        {
                            SelectedPlanHistories.Load(true);
                        }
                    }
                }
            };
        }

        /// <summary>
        /// 初始化所选计划的计划明细
        /// </summary>
        private void InitialPlanHistory()
        {
            SelectedPlanHistories = _service.CreateCollection(_context.PlanHistories);
            _planHistoryDescriptor = new FilterDescriptor("PlanId", FilterOperator.IsEqualTo, Guid.Empty);
            SelectedPlanHistories.FilterDescriptors.Add(_planHistoryDescriptor);
            _service.RegisterCollectionView(SelectedPlanHistories);
        }

        /// <summary>
        ///     获取执行计划的历史
        /// </summary>
        private void GetPerformPlanHistory()
        {
            if (SelectedPlan != null)
            {
                Func<PlanHistoryDTO, bool> annualPlanHistoryExpress = p =>
                    p.PerformAnnualId == SelectedPlan.AnnualId; //当年度计划历史表达式
                //不是当年度的计划，但执行时间在选择计划的年度表达式
                Func<PlanHistoryDTO, bool> performedPlanHistoryExpress = p =>
                    p.PerformAnnualId != SelectedPlan.AnnualId
                    && p.RelatedGuid != null
                    &&
                    ((p.RelatedStartDate != null
                      &&
                      p.RelatedStartDate.Value.Year ==
                      SelectedPlan.Year)
                     ||
                     (p.RelatedEndDate != null &&
                      p.RelatedEndDate.Value.Year ==
                      SelectedPlan.Year));
                var annualPlanHistory = SelectedPlanHistories.Where(annualPlanHistoryExpress);
                var performedPlanHistory = SelectedPlanHistories.Where(performedPlanHistoryExpress);
                PlanHistories = annualPlanHistory.Union(performedPlanHistory);
            }
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
            if (currentPlan == null)
            {
                PerformPlanHeader = "当前计划完成情况（单位：%）";
                return;
            }
            if (SelectedPlanHistories.Count == 0)
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
                decimal performedCount = SelectedPlanHistories.Count(exprOperationPlanImportAndExport);
                //计划执行
                decimal planPerformCount = SelectedPlanHistories.Count(p => p.Year == currentPlan.Year);
                //统计执行百分比
                Performance = planPerformCount == 0 ? 100 : Math.Round(performedCount * 100 / planPerformCount, 2);
            }
            PerformPlanHeader = currentPlan.Year + "年度计划完成情况（执行率：" + Convert.ToString(Performance) + "%）";
        }

        #endregion

        #region 查询申请、运营历史等相关信息

        /// <summary>
        ///     创建查询路径
        /// </summary>
        /// <param name="planHistoryId"></param>
        /// <param name="approvalHistoryId"></param>
        /// <param name="planType"></param>
        /// <param name="relatedGuid"></param>
        /// <returns></returns>
        private Uri CreatePerformPathQuery(string planHistoryId, string approvalHistoryId, int planType,
            string relatedGuid)
        {
            return new Uri(string.Format("PerformPlanQuery?planHistoryId='{0}'&approvalHistoryId='{1}'" +
                                         "&relatedGuid='{2}'&" +
                                         "&planType={3}", planHistoryId, approvalHistoryId, relatedGuid, planType),
                UriKind.Relative);
        }

        #region 查看申请、运营历史

        /// <summary>
        ///     提交审核
        /// </summary>
        public DelegateCommand<object> ViewPerformPlanCommand { get; private set; }

        private void OnViewPerformPlan(object obj)
        {
            var operationChild = ServiceLocator.Current.GetInstance<OperationChild>();
            var planHistory = obj as PlanHistoryDTO;
            if (planHistory != null)
            {
                //查看路径
                var path = CreatePerformPathQuery(planHistory.Id.ToString(), planHistory.ApprovalHistoryId.ToString(),
                    planHistory.PlanType, planHistory.RelatedGuid.ToString());
                var operationChildVm = operationChild.DataContext as OperationChildVM;
                if (operationChildVm != null)
                    operationChildVm.OnLoadPerformPlan(path);
                operationChild.ShowDialog();
            }
        }

        private bool CanViewPerformPlan(object obj)
        {
            return true;
        }

        private void InitialCommand()
        {
            ViewPerformPlanCommand = new DelegateCommand<object>(OnViewPerformPlan, CanViewPerformPlan);
        }

        #endregion

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

        protected override void SetIsBusy()
        {
            IsBusy = PlansView.IsBusy;
        }

        #endregion
    }
}