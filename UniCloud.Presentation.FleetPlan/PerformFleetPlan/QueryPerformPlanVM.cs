#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
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
            InitialCommand();
        }

        #region 加载计划

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
            if (currentPlan == null)
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
                Performance = planPerformCount == 0 ? 100 : Math.Round(performedCount*100/planPerformCount, 2);
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

        private PerformPlan _selPerformPlan;//选中的执行计划情况
        private void OnViewPerformPlan(object obj)
        {
            var planHistory = obj as PlanHistoryDTO;
            if (planHistory != null)
            {
                var path = CreatePerformPathQuery(planHistory.Id.ToString(), planHistory.ApprovalHistoryId.ToString(),
                    planHistory.PlanType, planHistory.RelatedGuid.ToString());

                _context.BeginExecute<PerformPlan>(path,
                    result => Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        var context = result.AsyncState as FleetPlanData;

                        try
                        {
                            if (context != null)
                            {
                                foreach (var performPlan in context.EndExecute<PerformPlan>(result))
                                {
                                    _selPerformPlan = performPlan;
                                    GetPerformPlan();
                                }
                            }
                        }
                        catch (DataServiceQueryException ex)
                        {
                            QueryOperationResponse response = ex.Response;

                            Console.WriteLine(response.Error.Message);
                        }

                    }), _context);
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

        /// <summary>
        /// 批文历史集合
        /// </summary>
        public IEnumerable<ApprovalHistoryDTO> ApprovalHistories
        {
            get
            {
                if (_selPerformPlan == null || _selPerformPlan.ApprovalHistory==null)
                {
                    return null;
                }
                var ls = new List<ApprovalHistoryDTO> {_selPerformPlan.ApprovalHistory};
                return ls;
            }
        }

        /// <summary>
        /// 运营权历史
        /// </summary>
        public IEnumerable<OperationHistoryDTO> OperationHistories
        {
            get
            {
                if (_selPerformPlan == null || _selPerformPlan.OperationHistory == null)
                {
                    return null;
                }
                var ls = new List<OperationHistoryDTO> { _selPerformPlan.OperationHistory };
                return ls;
            }
        }

        /// <summary>
        /// 商业数据
        /// </summary>
        public IEnumerable<AircraftBusinessDTO> AircraftBusiness
        {
            get
            {
                if (_selPerformPlan == null || _selPerformPlan.AircraftBusiness == null)
                {
                    return null;
                }
                var ls = new List<AircraftBusinessDTO> { _selPerformPlan.AircraftBusiness };
                return ls;
            }
        }

        private void GetPerformPlan()
        {
            RaisePropertyChanged(() => ApprovalHistories);
            RaisePropertyChanged(() => OperationHistories);
            RaisePropertyChanged(() => AircraftBusiness);


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

        #endregion
    }
}