#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/25 13:51:46
// 文件名：FleetPlanLayVM
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
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export(typeof(FleetPlanLayVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FleetPlanLayVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;
        private AnnualDTO _curAnnual = new AnnualDTO();
        private FilterDescriptor _annualDescriptor;
        private bool _loadedPlans;
        private bool _loadedPlanAircrafts;
        private PlanAircraftDTO _planAircraft;
        private PlanHistoryDTO _operationPlan;
        private PlanHistoryDTO _changePlan;


        [ImportingConstructor]
        public FleetPlanLayVM(IRegionManager regionManager, IFleetPlanService service)
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
            _annualDescriptor = new FilterDescriptor("Year", FilterOperator.IsGreaterThanOrEqualTo, DateTime.Now.Year - 1);
            Annuals.FilterDescriptors.Add(_annualDescriptor);
            Annuals.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (Annuals.Count != 0)
                {
                    _curAnnual = Annuals.SingleOrDefault(p => p.IsOpen);
                    if (!Plans.AutoLoad)
                        Plans.AutoLoad = true;
                    else
                        Plans.Load(true);
                }
                RefreshCommandState();
            };

            Plans = _service.CreateCollection(_context.Plans);
            Plans.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                CurPlan = Plans.Where(p => p.Year == _curAnnual.Year).OrderBy(p => p.VersionNumber).LastOrDefault();
                _loadedPlans = true;
                LoadPlanAircrafts();
            };
            _service.RegisterCollectionView(Plans);//注册查询集合

            PlanAircrafts = _service.CreateCollection(_context.PlanAircrafts.Expand(p => p.PlanHistories));
            PlanAircrafts.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                _loadedPlanAircrafts = true;
                LoadPlanAircrafts();
            };
            _service.RegisterCollectionView(PlanAircrafts);//注册查询集合

            Aircrafts = new QueryableDataServiceCollectionView<AircraftDTO>(_context, _context.Aircrafts);

            ActionCategories = new QueryableDataServiceCollectionView<ActionCategoryDTO>(_context, _context.ActionCategories);

            AircraftCategories = new QueryableDataServiceCollectionView<AircraftCategoryDTO>(_context, _context.AircraftCategories);

            AircraftTypes = new QueryableDataServiceCollectionView<AircraftTypeDTO>(_context, _context.AircraftTypes);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            AddEntityCommand = new DelegateCommand<object>(OnAddEntity, CanAddEntity);
            RemoveEntityCommand = new DelegateCommand<object>(OnRemoveEntity, CanRemoveEntity);
            CommitCommand = new DelegateCommand<object>(OnCommit, CanCommit);
            CheckCommand = new DelegateCommand<object>(OnCheck, CanCheck);
            OkCommand = new DelegateCommand<object>(OnOk, CanOk);
            CancelCommand = new DelegateCommand<object>(OnCancel, CanCancel);
            CellEditEndCommand=new DelegateCommand<object>(OnCellEditEnd);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 计划年度集合

        /// <summary>
        ///     当前计划年度
        /// </summary>
        public QueryableDataServiceCollectionView<AnnualDTO> Annuals { get; set; }

        #endregion

        #region 所有运力增减计划集合

        /// <summary>
        /// 所有运力增减计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> Plans { get; set; }

        #endregion

        #region 计划飞机集合

        /// <summary>
        ///     计划飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanAircraftDTO> PlanAircrafts { get; set; }

        #endregion

        #region 现役飞机集合

        /// <summary>
        ///     现役飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; }

        #endregion

        #region 活动类型集合

        /// <summary>
        ///     活动类型集合
        /// </summary>
        public QueryableDataServiceCollectionView<ActionCategoryDTO> ActionCategories { get; set; }

        #endregion

        #region 座级集合

        /// <summary>
        ///     座级集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftCategoryDTO> AircraftCategories { get; set; }

        #endregion

        #region 机型集合

        /// <summary>
        ///     机型集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftTypeDTO> AircraftTypes { get; set; }

        #endregion

        #region 执行月份集合

        /// <summary>
        ///     执行月份集合
        /// </summary>
        public List<int> Months
        {
            get
            {
                return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            }
        }

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
            _loadedPlans = false;
            _loadedPlanAircrafts = false;

            Annuals.Load(true);

            if (!PlanAircrafts.AutoLoad)
                PlanAircrafts.AutoLoad = true;
            else
                PlanAircrafts.Load(true);

            if (!Aircrafts.AutoLoad)
                Aircrafts.AutoLoad = true;
            else
                Aircrafts.Load(true);
            ActionCategories.AutoLoad = true;
            AircraftCategories.AutoLoad = true;
            AircraftTypes.AutoLoad = true;
        }

        public void LoadPlanAircrafts()
        {
            if (_loadedPlans && _loadedPlanAircrafts)
            {
                //根据当前计划，过滤得到当前计划关联的计划飞机集合，用于界面banding
                if (PlanHistories != null && PlanAircrafts.Count != 0)
                {
                    ViewPlanAircrafts.Clear();
                    foreach (var ph in PlanHistories)
                    {
                        var planAircraft = PlanAircrafts.FirstOrDefault(p => p.Id == ph.PlanAircraftId);
                        ViewPlanAircrafts.Add(planAircraft);
                    }
                }
            }
        }

        #region 业务

        #region 当前运力增减计划

        private PlanDTO _curPlan = new PlanDTO();

        /// <summary>
        ///     当前运力增减计划
        /// </summary>
        public PlanDTO CurPlan
        {
            get { return _curPlan; }
            private set
            {
                if (_curPlan != value)
                {
                    _curPlan = value;
                    PlanHistories.Clear();
                    foreach (var ph in value.PlanHistories)
                    {
                        PlanHistories.Add(ph);
                    }
                    RaisePropertyChanged(() => CurPlan);
                    RaisePropertyChanged(() => PlanTitle);
                }
            }
        }

        #endregion

        #region 计划标题

        /// <summary>
        /// 计划标题
        /// </summary>
        public string PlanTitle
        {
            get
            {
                if (CurPlan != null)
                {
                    var sb = new StringBuilder();
                    sb.Append(CurPlan.Title);
                    sb.Append("【版本");
                    sb.Append(CurPlan.VersionNumber);
                    sb.Append("】");
                    return sb.ToString();
                }
                return null;
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

        #region 当前计划关联的计划飞机集合

        private ObservableCollection<PlanAircraftDTO> _viewPlanAircrafts = new ObservableCollection<PlanAircraftDTO>();

        /// <summary>
        ///     当前计划关联的计划飞机集合
        /// </summary>
        public ObservableCollection<PlanAircraftDTO> ViewPlanAircrafts
        {
            get { return _viewPlanAircrafts; }
            private set
            {
                if (_viewPlanAircrafts != value)
                {
                    _viewPlanAircrafts = value;
                    RaisePropertyChanged(() => ViewPlanAircrafts);
                }
            }
        }

        #endregion

        #region 选择的运力增减计划明细

        private PlanHistoryDTO _selPlanHistory;

        /// <summary>
        ///     选择的运力增减计划明细
        /// </summary>
        public PlanHistoryDTO SelPlanHistory
        {
            get { return _selPlanHistory; }
            private set
            {
                if (_selPlanHistory != value)
                {
                    _selPlanHistory = value;
                    RaisePropertyChanged(() => SelPlanHistory);

                    if (ViewPlanAircrafts.Any(pa => pa.Id == _selPlanHistory.PlanAircraftId))
                        SelPlanAircraft = ViewPlanAircrafts.FirstOrDefault(p => p.Id == _selPlanHistory.PlanAircraftId);
                    else SelPlanAircraft = null;

                    if (Aircrafts.Any(pa => pa.AircraftId == value.AircraftId))
                        SelAircraft = Aircrafts.SourceCollection.Cast<AircraftDTO>().FirstOrDefault(p => p.AircraftId == value.AircraftId);
                    else SelAircraft = null;
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的计划飞机

        private PlanAircraftDTO _selPlanAircraft;

        /// <summary>
        ///     选择的计划飞机
        /// </summary>
        public PlanAircraftDTO SelPlanAircraft
        {
            get { return _selPlanAircraft; }
            private set
            {
                if (_selPlanAircraft != value)
                {
                    _selPlanAircraft = value;
                    RaisePropertyChanged(() => SelPlanAircraft);
                }
            }
        }

        #endregion

        #region 选择的现役飞机
        private AircraftDTO _selAircraft;

        /// <summary>
        ///     选择的现役飞机
        /// </summary>
        public AircraftDTO SelAircraft
        {
            get { return _selAircraft; }
            private set
            {
                if (_selAircraft != value)
                {
                    _selAircraft = value;
                    RaisePropertyChanged(() => SelAircraft);
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
            AddEntityCommand.RaiseCanExecuteChanged();
            RemoveEntityCommand.RaiseCanExecuteChanged();
            CommitCommand.RaiseCanExecuteChanged();
            CheckCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 创建新计划

        /// <summary>
        ///     创建新计划
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var lastPlan = CurPlan;
            var newPlan = _service.CreateNewVersionPlan(lastPlan);

            Plans.AddNew(newPlan);
            CurPlan = Plans.OrderBy(p => p.VersionNumber).LastOrDefault();
            RefreshCommandState();
        }

        private bool CanNew(object obj)
        {
            // 操作台当前计划已到已审核状态时，按钮可用
            return CurPlan != null
                   && CurPlan.Status > (int)PlanStatus.待审核;
        }

        #endregion

        #region 增加计划行

        /// <summary>
        ///     增加计划行
        /// </summary>
        public DelegateCommand<object> AddEntityCommand { get; private set; }

        private void OnAddEntity(object obj)
        {
            // 创建新的计划飞机
            var pa = new PlanAircraftDTO
            {
                Id = Guid.NewGuid(),
                AirlinesId = CurPlan.AirlinesId,
                AirlinesName = CurPlan.AirlinesName,
                Status = (int)ManageStatus.计划,
                IsOwn = true
            };
            OpenEditDialog(null, PlanDetailCreateSource.New);
            //创建完新的计划明细后，将其与新的计划飞机关联起来。
            pa.AircraftTypeId = this.PlanDetail.AircraftTypeId;
            this.PlanDetail.PlanAircraftId = pa.Id;
            //将新建的实体添加到对应的注册集合中
            PlanAircrafts.AddNew(pa);
            ViewPlanAircrafts.Add(pa);
            CurPlan.PlanHistories.Add(this.PlanDetail);
            PlanHistories.Add(this.PlanDetail);
        }

        private bool CanAddEntity(object obj)
        {
            return CurPlan != null && CurPlan.Status < (int)PlanStatus.已审核;
        }

        #endregion

        #region 移除计划行

        /// <summary>
        ///     移除计划行
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; private set; }

        private void OnRemoveEntity(object obj)
        {
            RemovePlanDetail(SelPlanHistory);
            RefreshCommandState();
        }

        private bool CanRemoveEntity(object obj)
        {
            // 当前计划为空时，按钮不可用
            if (CurPlan == null) return false;
            // 选中计划明细为空时，按钮不可用
            if (SelPlanHistory == null) return false;
            // 选中计划明细没有对应的计划飞机时，按钮不可用
            if (SelPlanHistory.PlanAircraftId == null) return false;
            // 选中计划明细无需申请的，按钮可用
            if (SelPlanHistory.ActionCategoryId != Guid.Empty && !SelPlanHistory.NeedRequest) return true;

            // 计算选中计划明细对应的计划飞机在当前计划中的明细项集合
            var planAircraft = ViewPlanAircrafts.FirstOrDefault(p => p.Id == SelPlanHistory.PlanAircraftId);
            if (planAircraft != null)
            {
                var planDetails = planAircraft.PlanHistories.Where(ph => ph.PlanId == CurPlan.Id).ToList();
                // 1、选中计划明细对应的计划飞机在当前计划中只有一条明细项
                if (planDetails.Count == 1)
                {
                    // 当前计划的状态还没有到已审核，且计划飞机的管理状态还没到申请时，按钮可用
                    return CurPlan.Status < (int)PlanStatus.已审核 &&
                           planAircraft.Status < (int)ManageStatus.申请;
                }
                // 2、选中计划明细对应的计划飞机在当前计划中超过一条明细项
                // 选中计划明细的操作类型为引进时，按钮不可用
                else
                {
                    return this.SelPlanHistory.ActionType != "引进";
                }
                // 3、选中计划明细对应的计划飞机在当前计划中没有明细项（业务逻辑控制上不会出现这种情况）
            }
            //计划飞机有id,但计划飞机集合未加载完成（正常不会出现这种情况）
            else return false;
        }


        /// <summary>
        /// 移除计划明细项
        /// </summary>
        /// <param name="planDetail"></param>
        public void RemovePlanDetail(PlanHistoryDTO planDetail)
        {
            if (planDetail != null)
            {
                // 获取计划飞机
                var planAircraft = ViewPlanAircrafts.FirstOrDefault(p => p.Id == planDetail.PlanAircraftId);
                // 获取计划飞机的明细项集合
                if (planAircraft != null)
                {
                    var planAircraftHistories = planAircraft.PlanHistories;
                    // 获取计划飞机在当前计划中的明细项集合
                    var planDetails = PlanHistories.Where(ph => ph.PlanAircraftId == planAircraft.Id).ToList();

                    // 1、已有飞机（只有变更与退出计划）
                    if (planAircraft.AircraftId != null)
                    {
                        // 1.1、计划飞机在当前计划中只有一条明细项
                        if (planDetails.Count == 1)
                            planAircraft.Status = (int)ManageStatus.运营;
                        // 1.2、计划飞机在当前计划中超过一条明细项，即一条变更、一条退出
                        else
                        {
                            // 移除的是变更计划，计划飞机改为运营状态（可能之前也是运营状态）
                            if (planDetail.ActionType.Contains("变更"))
                                planAircraft.Status = (int)ManageStatus.运营;
                            // 移除的是退出计划，不做任何改变
                        }
                    }
                    // 2、没有飞机（只有引进与退出计划）
                    // 2.1、计划飞机相关的明细项数量为1
                    // 删除相关计划飞机。
                    else if (planAircraftHistories.Count == 1)
                    {
                        ViewPlanAircrafts.Remove(planAircraft);
                    }
                    // 2.2、计划飞机相关的计划历史数量不为1（即超过1）
                    // 2.2.1、计划飞机在当前计划中只有一条明细项
                    // 计划飞机的管理状态改为预备
                    else if (planDetails.Count == 1)
                    {
                        planAircraft.Status = (int)ManageStatus.预备;
                    }
                    // 2.2.2、计划飞机在当前计划中超过一条明细项，即一条引进、一条退出
                    // 不改变计划飞机状态

                    PlanHistories.Remove(planDetail);
                    //并重新获取计划飞机的集合
                    LoadPlanAircrafts();
                }
            }
            RefreshCommandState();
        }

        #endregion

        #region 提交审核

        /// <summary>
        ///     提交审核
        /// </summary>
        public DelegateCommand<object> CommitCommand { get; private set; }

        private void OnCommit(object obj)
        {
            if (!CurPlan.PlanHistories.Any())
            {
                MessageAlert("计划明细不能为空!");
            }
            if (CurPlan != null)
            {
                CurPlan.Status = (int)PlanStatus.待审核;
                RefreshCommandState();
            }
        }

        private bool CanCommit(object obj)
        {
            // 操作台当前计划处于草稿状态的，按钮可用
            return CurPlan != null && CurPlan.Status == (int)PlanStatus.草稿;
        }

        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> CheckCommand { get; private set; }

        private void OnCheck(object obj)
        {
            if (CurPlan != null)
            {
                CurPlan.Status = (int)PlanStatus.已审核;
                CurPlan.IsValid = true;
                RefreshCommandState();
            }
        }

        private bool CanCheck(object obj)
        {
            // 操作台当前计划处于审核状态的，按钮可用
            return CurPlan != null && CurPlan.Status == (int)PlanStatus.待审核;
        }

        #endregion

        #region Method

        /// <summary>
        /// 打开子窗体之前判断是否要打开
        /// </summary>
        /// <param name="planAircraft">计划飞机</param>
        /// <param name="source">调用的来源</param>
        internal void OpenEditDialog(PlanAircraftDTO planAircraft, PlanDetailCreateSource source)
        {
            this._planAircraft = planAircraft;
            this._operationPlan = null;
            this._changePlan = null;
            // 获取计划飞机在当前计划中的明细项集合
            var planDetails = new List<PlanHistoryDTO>();
            if (CurPlan != null && planAircraft != null)
                planDetails = CurPlan.PlanHistories.Where(ph => ph.PlanAircraftId == planAircraft.Id).ToList();

            // 1、计划飞机在当前计划中没有明细项；或新增计划明细时，传入空的计划飞机
            if (_planAircraft == null || !planDetails.Any())
                this.ShowEditDialog(null, source);
            // 2、计划飞机在当前计划中已有明细项
            else
            {
                if (planDetails.Count == 1)
                {
                    string content;
                    switch (source)
                    {
                        case PlanDetailCreateSource.New:
                            break;
                        case PlanDetailCreateSource.PlanAircraft:
                            content = "计划飞机在当前计划中已有引进计划明细项，是否要针对该计划飞机添加退出计划明细项？";
                            MessageConfirm("确认添加计划明细", content, (o, e) =>
                            {
                                if (e.DialogResult == true)
                                    this.ShowEditDialog(planDetails[0], source);
                            });
                            break;
                        case PlanDetailCreateSource.Aircraft:
                            content = planDetails[0].ActionType.Contains("变更")
                                          ? "飞机在当前计划中已有变更计划明细项，是否要针对该飞机添加退出计划明细项？"
                                          : "飞机在当前计划中已有退出计划明细项，是否要针对该飞机添加变更计划明细项？";
                            MessageConfirm("确认添加计划明细", content, (o, e) =>
                            {
                                if (e.DialogResult == true)
                                    this.ShowEditDialog(planDetails[0], source);
                            });
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("source");
                    }
                }
                else
                {
                    MessageAlert("提醒", "该计划飞机已有两条明细项，不能再添加新计划明细项！");
                }
            }

        }

        private void ShowEditDialog(PlanHistoryDTO existDetail, PlanDetailCreateSource source)
        {
            switch (source)
            {
                case PlanDetailCreateSource.New:
                    this._operationPlan = _service.CreatePlanHistory(CurPlan, this._planAircraft, "引进", 1);
                    this.PlanDetail = this._operationPlan;
                    this.IsChangeable = true;
                    break;
                case PlanDetailCreateSource.PlanAircraft:
                    this.IsPlanTypeVisible = Visibility.Collapsed;
                    // 计划飞机已有的明细项肯定是引进计划，只能添加退出计划
                    this._operationPlan = _service.CreatePlanHistory(CurPlan, this._planAircraft, existDetail != null ? "退出" : "引进", 1);
                    this.PlanDetail = this._operationPlan;
                    //这时不能修改机型和座机
                    this.IsChangeable = false;
                    break;
                case PlanDetailCreateSource.Aircraft:
                    this.IsPlanTypeVisible = Visibility.Visible;
                    // 1、计划飞机已有明细项
                    if (existDetail != null)
                    {
                        // 已有的是变更计划，只能添加退出计划
                        if (existDetail.ActionType == "变更")
                        {
                            this.IsChangeEnabled = false;
                            this.IsOperation = true;
                            this.IsChange = false;
                            this.OnOperation();
                        }
                        // 已有的是退出计划，只能添加变更计划
                        else
                        {
                            this.IsOperationEnabled = false;
                            this.IsOperation = false;
                            this.IsChange = true;
                            this.OnOperation();//生成之后，不让用户编辑，起到保存原计划历史的机型的作用，在取消时，能够用来恢复计划飞机数据
                            this.OnChange();
                        }
                    }
                    // 2、计划飞机没有明细项
                    else
                    {
                        this.IsChangeEnabled = true;
                        this.IsOperationEnabled = true;
                        if (!this.IsOperation) this.IsOperation = true;
                        this.OnOperation();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("source");
            }

            this.EditDialog.ShowDialog();
        }

        #region 调用业务逻辑处理

        private void OnOperation()
        {
            if (this._operationPlan == null)
            {
                // 针对运营飞机的运营计划只能是退出
                this._operationPlan = _service.CreatePlanHistory(CurPlan, this._planAircraft, "退出", 1);
            }
            this.PlanDetail = this._operationPlan;
            this.IsChangeable = false;
        }

        private void OnChange()
        {
            if (this._changePlan == null)
                this._changePlan = _service.CreatePlanHistory(CurPlan, this._planAircraft, "变更", 2);
            this.PlanDetail = this._changePlan;
            this.IsChangeable = true;
        }

        #endregion
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
                if (string.Equals(cell.Column.UniqueName, "Regional"))
                {
                    var planhistory = gridView.CurrentCellInfo.Item as PlanHistoryDTO;
                    if (planhistory != null)
                    {
                        var planAircraft = PlanAircrafts.FirstOrDefault(p => p.Id == planhistory.PlanAircraftId);
                        if (planAircraft != null)
                            planAircraft.Regional = planhistory.Regional;
                    }
                }
                else if (string.Equals(cell.Column.UniqueName, "AircraftType"))
                {
                    var planhistory = gridView.CurrentCellInfo.Item as PlanHistoryDTO;
                    if (planhistory != null)
                    {
                        var planAircraft = PlanAircrafts.FirstOrDefault(p => p.Id == planhistory.PlanAircraftId);
                        if (planAircraft != null)
                            planAircraft.AircraftTypeId = planhistory.AircraftTypeId;
                    }
                }
            }
        }

        #endregion
        #endregion

        #region 子窗体相关

        [Import]
        public PlanDetailEditDialog EditDialog;

        #region 当前编辑的计划明细项

        private PlanHistoryDTO _planDetail;

        /// <summary>
        /// 当前编辑的计划明细项
        /// </summary>
        public PlanHistoryDTO PlanDetail
        {
            get { return this._planDetail; }
            private set
            {
                if (this._planDetail != value)
                {
                    this._planDetail = value;
                    this.RaisePropertyChanged(() => this.PlanDetail);
                }
            }
        }

        #endregion

        #region 选中运营计划

        private bool _isOperation;

        /// <summary>
        /// 选中运营计划
        /// </summary>
        public bool IsOperation
        {
            get { return this._isOperation; }
            private set
            {
                if (this._isOperation != value)
                {
                    this._isOperation = value;
                    this.RaisePropertyChanged(() => this.IsOperation);
                    this.OnOperation();
                }
            }
        }

        #endregion

        #region 选中变更计划

        private bool _isChange;

        /// <summary>
        /// 选中变更计划
        /// </summary>
        public bool IsChange
        {
            get { return this._isChange; }
            private set
            {
                if (this._isChange != value)
                {
                    this._isChange = value;
                    this.RaisePropertyChanged(() => this.IsChange);
                    this.OnChange();
                }
            }
        }

        #endregion

        #region 运营计划按钮是否可用

        private bool _isOperationEnabled;

        /// <summary>
        /// 运营计划按钮是否可用
        /// </summary>
        public bool IsOperationEnabled
        {
            get { return this._isOperationEnabled; }
            private set
            {
                if (this._isOperationEnabled != value)
                {
                    this._isOperationEnabled = value;
                    this.RaisePropertyChanged(() => this.IsOperationEnabled);
                }
            }
        }

        #endregion

        #region 变更计划按钮是否可用

        private bool _isChangeEnabled;

        /// <summary>
        /// 变更计划按钮是否可用
        /// </summary>
        public bool IsChangeEnabled
        {
            get { return this._isChangeEnabled; }
            private set
            {
                if (this._isChangeEnabled != value)
                {
                    this._isChangeEnabled = value;
                    this.RaisePropertyChanged(() => this.IsChangeEnabled);
                }
            }
        }

        #endregion

        #region 是否隐藏Radio按钮

        private Visibility _isPlanTypeVisible = Visibility.Collapsed;

        /// <summary>
        /// 是否隐藏Radio按钮
        /// </summary>
        public Visibility IsPlanTypeVisible
        {
            get { return this._isPlanTypeVisible; }
            private set
            {
                if (this._isPlanTypeVisible != value)
                {
                    this._isPlanTypeVisible = value;
                    this.RaisePropertyChanged(() => this.IsPlanTypeVisible);
                }
            }
        }

        #endregion

        #region 控制编辑计划飞机的退出计划时，控制座机、机型是否可编辑

        private bool _isAicraftTypeAndRegionalChangeable = true;

        /// <summary>
        /// 座级、机型是否可改
        /// </summary>
        public bool IsAicraftTypeAndRegionalChangeable
        {
            get { return this._isAicraftTypeAndRegionalChangeable; }
            private set
            {
                if (this._isAicraftTypeAndRegionalChangeable != value)
                {
                    this._isAicraftTypeAndRegionalChangeable = value;
                    this.RaisePropertyChanged(() => this.IsAicraftTypeAndRegionalChangeable);
                }
            }
        }

        private bool _isChangeable = true;

        /// <summary>
        /// 座级、机型是否可改
        /// </summary>
        public bool IsChangeable
        {
            get { return _isChangeable; }
            private set
            {
                if (this._isChangeable != value)
                {
                    this._isChangeable = value;
                    this.RaisePropertyChanged(() => this.IsChangeable);
                    //this.IsAicraftTypeAndRegionalChangeable = (this.PlanDetail.IsNotOperationOrChangePlan && this._isChangeable);
                }
            }
        }

        #endregion


        #region 确认

        /// <summary>
        /// 确认
        /// </summary>
        public DelegateCommand<object> OkCommand { get; private set; }

        private void OnOk(object obj)
        {
            OnOkExecute(obj);
        }

        protected virtual bool CanOk(object obj)
        {
            return true;
        }

        protected virtual void OnOkExecute(object sender)
        {
            this.EditDialog.Close();
        }

        #endregion

        #region 取消

        /// <summary>
        /// 取消
        /// </summary>
        public DelegateCommand<object> CancelCommand { get; private set; }

        private void OnCancel(object obj)
        {
            OnCancelExecute(obj);
        }

        protected virtual bool CanCancel(object obj)
        {
            return true;
        }

        protected virtual void OnCancelExecute(object sender)
        {
            this.EditDialog.Close();
        }

        #endregion
        #endregion
    }
}