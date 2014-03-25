#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/25 13:54:26
// 文件名：SpareEnginePlanLayVM
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
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export(typeof(SpareEnginePlanLayVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SpareEnginePlanLayVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;
        private AirlinesDTO _curAirlines = new AirlinesDTO();

        [ImportingConstructor]
        public SpareEnginePlanLayVM(IRegionManager regionManager, IFleetPlanService service)
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
            EnginePlans = _service.CreateCollection(_context.EnginePlans, o => o.EnginePlanHistories);
            _service.RegisterCollectionView(EnginePlans);//注册查询集合

            PlanEngines = _service.CreateCollection(_context.PlanEngines);
            _service.RegisterCollectionView(PlanEngines);//注册查询集合

            Annuals = new QueryableDataServiceCollectionView<AnnualDTO>(_context, _context.Annuals);
            var annualDescriptor = new FilterDescriptor("Year", FilterOperator.IsGreaterThanOrEqualTo, DateTime.Now.Year - 1);
            Annuals.FilterDescriptors.Add(annualDescriptor);
            Annuals.OrderBy(p => p.Year);

            EngineTypes = new QueryableDataServiceCollectionView<EngineTypeDTO>(_context, _context.EngineTypes);

            ActionCategories = new QueryableDataServiceCollectionView<ActionCategoryDTO>(_context, _context.ActionCategories);
            var actionDescriptor = new FilterDescriptor("ActionType", FilterOperator.IsGreaterThanOrEqualTo, "引进");
            ActionCategories.FilterDescriptors.Add(actionDescriptor);

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
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            CommitCommand = new DelegateCommand<object>(OnCommit, CanCommit);
            CheckCommand = new DelegateCommand<object>(OnCheck, CanCheck);
            AddEntityCommand = new DelegateCommand<object>(OnAddEntity, CanAddEntity);
            RemoveEntityCommand = new DelegateCommand<object>(OnRemoveEntity, CanRemoveEntity);
            CellEditEndCommand = new DelegateCommand<object>(OnCellEditEnd);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 计划年度集合

        /// <summary>
        ///     计划年度集合
        /// </summary>
        public QueryableDataServiceCollectionView<AnnualDTO> Annuals { get; set; }

        #endregion

        #region 发动机型号集合

        /// <summary>
        ///     发动机型号集合
        /// </summary>
        public QueryableDataServiceCollectionView<EngineTypeDTO> EngineTypes { get; set; }

        #endregion

        #region 活动类型集合

        /// <summary>
        ///     活动类型集合
        /// </summary>
        public QueryableDataServiceCollectionView<ActionCategoryDTO> ActionCategories { get; set; }

        #endregion

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
            if (!EnginePlans.AutoLoad)
                EnginePlans.AutoLoad = true;
            else
                EnginePlans.Load(true);

            if (!PlanEngines.AutoLoad)
                PlanEngines.AutoLoad = true;
            else
                PlanEngines.Load(true);

            Annuals.AutoLoad = true;
            EngineTypes.AutoLoad = true;
            ActionCategories.AutoLoad = true;
        }

        #region 业务

        #region 备发计划集合

        /// <summary>
        ///     备发计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePlanDTO> EnginePlans { get; set; }

        #endregion

        #region 计划发动机集合

        /// <summary>
        ///     计划发动机集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanEngineDTO> PlanEngines { get; set; }

        #endregion

        #region 选择的备发计划

        private EnginePlanDTO _selEnginePlan;

        /// <summary>
        ///     选择的备发计划
        /// </summary>
        public EnginePlanDTO SelEnginePlan
        {
            get { return _selEnginePlan; }
            private set
            {
                if (_selEnginePlan != value)
                {
                    _selEnginePlan = value;
                    RaisePropertyChanged(() => SelEnginePlan);
                    EnginePlanHistories.Clear();
                    if (_selEnginePlan != null)
                    {
                        foreach (var enginePh in _selEnginePlan.EnginePlanHistories)
                        {
                            EnginePlanHistories.Add(enginePh);
                        }
                    }
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 备发计划明细集合

        private ObservableCollection<EnginePlanHistoryDTO> _enginePlanHistories=new ObservableCollection<EnginePlanHistoryDTO>();

        /// <summary>
        ///     备发计划明细集合
        /// </summary>
        public ObservableCollection<EnginePlanHistoryDTO> EnginePlanHistories
        {
            get { return _enginePlanHistories; }
            private set
            {
                if (_enginePlanHistories != value)
                {
                    _enginePlanHistories = value;
                    RaisePropertyChanged(() => EnginePlanHistories);
                }
            }
        }

        #endregion

        #region 选择的备发计划明细

        private EnginePlanHistoryDTO _selEnginePlanHistory;

        /// <summary>
        ///     选择的备发计划明细
        /// </summary>
        public EnginePlanHistoryDTO SelEnginePlanHistory
        {
            get { return _selEnginePlanHistory; }
            private set
            {
                if (_selEnginePlanHistory != value)
                {
                    _selEnginePlanHistory = value;
                    RaisePropertyChanged(() => SelEnginePlanHistory);

                    // 刷新按钮状态
                    RefreshCommandState();
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
            AddAttachCommand.RaiseCanExecuteChanged();
            NewCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
            CommitCommand.RaiseCanExecuteChanged();
            CheckCommand.RaiseCanExecuteChanged();
            AddEntityCommand.RaiseCanExecuteChanged();
            RemoveEntityCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 创建备发计划

        /// <summary>
        ///     创建新备发计划
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var newEnginePlan = new EnginePlanDTO()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                VersionNumber = 1,
                Status = 0,
                AirlinesId = _curAirlines.Id,
            };
            //如果有上个版本的备发计划，将未执行完的计划明细复制到新的计划中
            var enginePlan = EnginePlans.OrderBy(p => p.CreateDate).LastOrDefault();
            if (enginePlan != null)
            {
                foreach (var ph in enginePlan.EnginePlanHistories)
                {
                    if (ph.Status < (int) EnginePlanDeliverStatus.运营)
                    {
                        var enginePh = new EnginePlanHistoryDTO
                        {
                            Id = Guid.NewGuid(),
                            ActionCategoryId = ph.ActionCategoryId,
                            PerformAnnualId = ph.PerformAnnualId,
                            PerformMonth = ph.PerformMonth,
                            PlanEngineId = ph.PlanEngineId,
                            EngineTypeId = ph.EngineTypeId,
                            MaxThrust = ph.MaxThrust,
                            ImportDate = ph.ImportDate,
                            IsFinished = ph.IsFinished,
                            Note = ph.Note,
                            Status = ph.Status,
                        };
                        newEnginePlan.EnginePlanHistories.Add(enginePh);
                    }
                }
            }
            EnginePlans.AddNew(newEnginePlan);
            RefreshCommandState();
        }

        private bool CanNew(object obj)
        {
            if (!EnginePlans.Any()) return true;
            return !(EnginePlans.ToList().Any(p => p.EnginePlanStatus < EnginePlanStatus.已审核));
        }

        #endregion

        #region 删除备发计划

        /// <summary>
        ///     删除备发计划
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            if (_selEnginePlan != null)
            {
                EnginePlans.Remove(_selEnginePlan);
            }
        }

        private bool CanRemove(object obj)
        {
            return _selEnginePlan != null && _selEnginePlan.Status < (int)EnginePlanStatus.已审核;
        }

        #endregion

        #region 提交审核

        /// <summary>
        ///     提交审核
        /// </summary>
        public DelegateCommand<object> CommitCommand { get; private set; }

        private void OnCommit(object obj)
        {
            _selEnginePlan.Status = (int)EnginePlanStatus.待审核;
            RefreshCommandState();
        }

        private bool CanCommit(object obj)
        {
            return _selEnginePlan != null && _selEnginePlan.Status == (int)EnginePlanStatus.草稿;
        }

        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> CheckCommand { get; private set; }

        private void OnCheck(object obj)
        {
            _selEnginePlan.Status = (int)EnginePlanStatus.已审核;
            _selEnginePlan.IsValid = true;
            RefreshCommandState();
        }

        private bool CanCheck(object obj)
        {
            return _selEnginePlan != null && _selEnginePlan.Status == (int)EnginePlanStatus.待审核;
        }

        #endregion

        #region 增加备发计划明细

        /// <summary>
        ///     增加备发计划明细
        /// </summary>
        public DelegateCommand<object> AddEntityCommand { get; private set; }

        private void OnAddEntity(object obj)
        {
            var enginePh = new EnginePlanHistoryDTO
            {
                Id = Guid.NewGuid(),
                Status = 0,
                PerformMonth = 1,
                MaxThrust = 1,
            };
            var planEngine = new PlanEngineDTO
            {
                Id = Guid.NewGuid(),
                AirlinesId = _curAirlines.Id,
            };
            enginePh.PlanEngineId = planEngine.Id;
            PlanEngines.AddNew(planEngine);
            EnginePlanHistories.Add(enginePh);
            _selEnginePlan.EnginePlanHistories.Add(enginePh);
        }

        private bool CanAddEntity(object obj)
        {
            return _selEnginePlan != null && _selEnginePlan.Status == (int)EnginePlanStatus.草稿;
        }

        #endregion

        #region 移除计划行

        /// <summary>
        ///     移除计划行
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; private set; }

        private void OnRemoveEntity(object obj)
        {
            if (_selEnginePlanHistory != null)
            {
                SelEnginePlan.EnginePlanHistories.Remove(_selEnginePlanHistory);
                EnginePlanHistories.Remove(_selEnginePlanHistory);
            }
        }

        private bool CanRemoveEntity(object obj)
        {
            return _selEnginePlan != null && _selEnginePlan.Status == 0 && _selEnginePlanHistory != null && _selEnginePlanHistory.Status < 2;
        }

        #endregion

        #region 添加附件

        protected override bool CanAddAttach(object obj)
        {
            return _selEnginePlan != null;
        }

        /// <summary>
        ///     子窗口关闭后执行的操作
        /// </summary>
        /// <param name="doc">添加的附件</param>
        /// <param name="sender">添加附件命令的参数</param>
        protected override void WindowClosed(DocumentDTO doc, object sender)
        {
            base.WindowClosed(doc, sender);
            if (sender is Guid)
            {
                SelEnginePlan.DocumentId = doc.DocumentId;
                SelEnginePlan.DocName = doc.Name;
            }
        }
        #endregion

        #region GridView单元格变更处理

        public DelegateCommand<object> CellEditEndCommand { set; get; }

        /// <summary>
        ///     GridView单元格变更处理
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnCellEditEnd(object sender)
        {
            var gridView = sender as RadGridView;
            if (gridView != null)
            {
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "EngineType"))
                {
                    var planhistory = gridView.CurrentCellInfo.Item as EnginePlanHistoryDTO;
                    if (planhistory != null)
                    {
                        var planEngine = PlanEngines.FirstOrDefault(p => p.Id == planhistory.PlanEngineId);
                        if (planEngine != null && planhistory.EngineTypeId != Guid.Empty)
                        {
                            planEngine.EngineTypeId = planhistory.EngineTypeId;
                        }
                    }
                }
            }
        }

        #endregion
        #endregion

        //TODO: 明细发动机型号变化，改变计划发动机中的EngineType；删除计划的逻辑，需要筛选相应的计划发动机删除；删除计划明细的时候，判断删除对应的计划发动机
    }
}