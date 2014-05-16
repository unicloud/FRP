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
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
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
        private FilterDescriptor _planAcDescriptor;
        FilterDescriptor filter = new FilterDescriptor("PlanId", FilterOperator.IsEqualTo, Guid.Empty);
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
            Annuals = new QueryableDataServiceCollectionView<AnnualDTO>(_context, _context.Annuals);
            _annualDescriptor = new FilterDescriptor("Year", FilterOperator.IsGreaterThanOrEqualTo, DateTime.Now.Year - 2);
            Annuals.FilterDescriptors.Add(_annualDescriptor);
            Annuals.OrderBy(p => p.Year);
            Annuals.LoadedData += (sender, e) =>
            {
                if (Annuals.Count != 0)
                {
                    _curAnnual = e.Entities.Cast<AnnualDTO>().SingleOrDefault(p => p.IsOpen);
                    if (!Plans.AutoLoad)
                        Plans.AutoLoad = true;
                    else
                        Plans.Load(true);
                }
                RefreshCommandState();
            }; //获取年度集合，同时得到当前计划年度，再获取计划集合，同时得到当前计划

            Plans = _service.CreateCollection(_context.Plans);
            Plans.LoadedData += (sender, e) =>
            {
                CurPlan = e.Entities.Cast<PlanDTO>().Where(p => p.Year == _curAnnual.Year).OrderBy(p => p.VersionNumber).LastOrDefault();
                if (!AllPlanHistories.AutoLoad)
                    AllPlanHistories.AutoLoad = true;
                else
                    AllPlanHistories.Load(true);
            };
            _service.RegisterCollectionView(Plans);//注册查询集合，获取计划集合，同时得到当前计划

            AllPlanHistories = _service.CreateCollection(_context.PlanHistories);
            AllPlanHistories.LoadedData += (o, e) =>
            {
                ViewPlanHistories = new ObservableCollection<PlanHistoryDTO>();
                if (CurPlan != null)
                {
                    foreach (var ph in AllPlanHistories.SourceCollection.Cast<PlanHistoryDTO>())
                    {
                        ph.ActionCategories.AddRange(_service.GetActionCategoriesForPlanHistory(ph));
                        ph.AircraftCategories.AddRange(_service.GetAircraftCategoriesForPlanHistory(ph));
                        ph.AircraftTypes.AddRange(_service.GetAircraftTypesForPlanHistory(ph));
                        if (ph.PlanId == CurPlan.Id)
                            ViewPlanHistories.Add(ph);
                        _context.ChangeState(ph, EntityStates.Unchanged);
                    }
                    RaisePropertyChanged(() => ViewPlanHistories);
                }
            };
            _service.RegisterCollectionView(AllPlanHistories);//注册查询集合，获取计划集合，同时得到当前计划

            ViewPlanAircrafts = _service.CreateCollection(_context.PlanAircrafts);
            _planAcDescriptor = new FilterDescriptor("AircraftId", FilterOperator.IsEqualTo, null);
            ViewPlanAircrafts.FilterDescriptors.Add(_planAcDescriptor);
            _service.RegisterCollectionView(ViewPlanAircrafts);//注册查询集合，获取所有还没飞机的计划飞机集合，用户界面展示

            Aircrafts = new QueryableDataServiceCollectionView<AircraftDTO>(_context, _context.Aircrafts);//获取所有运营飞机的集合，TODO：判断是否必要筛选掉已退出运营的飞机

            AllPlanAircrafts = new QueryableDataServiceCollectionView<PlanAircraftDTO>(_context, _context.PlanAircrafts);//获取所有的计划飞机，用于关联到运营飞机，用于从运营飞机编制计划时使用

            AllActionCategories = new QueryableDataServiceCollectionView<ActionCategoryDTO>(_context, _context.ActionCategories);//获取所有的计划飞机，用于关联到运营飞机，用于从运营飞机编制计划时使用
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
            CellEditEndCommand = new DelegateCommand<object>(OnCellEditEnd);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 计划是否可编辑

        private bool _isPlanReadOnly = false;

        /// <summary>
        /// 计划是否可编辑
        /// </summary>
        public bool IsPlanReadOnly
        {
            get { return _isPlanReadOnly; }
            private set
            {
                if (_isPlanReadOnly != value)
                {
                    _isPlanReadOnly = value;
                    RaisePropertyChanged(() => IsPlanReadOnly);
                }
            }
        }

        #endregion

        #region 计划年度集合

        /// <summary>
        ///     当前计划年度
        /// </summary>
        public QueryableDataServiceCollectionView<AnnualDTO> Annuals { get; set; }

        #endregion

        #region 活动类型集合

        /// <summary>
        ///     活动类型集合
        /// </summary>
        public QueryableDataServiceCollectionView<ActionCategoryDTO> AllActionCategories { get; set; }

        #endregion

        #region 所有运力增减计划集合

        /// <summary>
        /// 所有运力增减计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> Plans { get; set; }

        /// <summary>
        /// 计划明细集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanHistoryDTO> AllPlanHistories { get; set; }

        #endregion

        #region 所有计划飞机集合

        /// <summary>
        ///     所有计划飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanAircraftDTO> AllPlanAircrafts { get; set; }

        #endregion

        #region 计划飞机集合

        /// <summary>
        ///     计划飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanAircraftDTO> ViewPlanAircrafts { get; set; }

        #endregion

        #region 现役飞机集合

        /// <summary>
        ///     现役飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; }

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
            Annuals.Load(true);
            AllPlanAircrafts.Load(true);
            AllActionCategories = _service.GetActionCategories(() => RaisePropertyChanged(() => AllActionCategories), true);

            AllActionCategories.Load(true);

            if (!ViewPlanAircrafts.AutoLoad)
                ViewPlanAircrafts.AutoLoad = true;
            else
                ViewPlanAircrafts.Load(true);

            if (!Aircrafts.AutoLoad)
                Aircrafts.AutoLoad = true;
            else
                Aircrafts.Load(true);
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
                    ViewPlanHistories = new ObservableCollection<PlanHistoryDTO>();
                    if (value != null)
                    {
                        foreach (var ph in AllPlanHistories.SourceCollection.Cast<PlanHistoryDTO>())
                        {
                            if (ph.PlanId == value.Id)
                                ViewPlanHistories.Add(ph);
                        }
                        //计划到审核阶段了，不能编辑
                        if (CurPlan.Status >= (int)PlanStatus.待审核)
                            IsPlanReadOnly = true;
                    }
                   
                    RaisePropertyChanged(() => ViewPlanHistories);
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

        private ObservableCollection<PlanHistoryDTO> _viewPlanHistories = new ObservableCollection<PlanHistoryDTO>();

        /// <summary>
        ///     计划明细集合
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
                    if (_selPlanHistory != null)
                    {
                        if (ViewPlanAircrafts.SourceCollection.Cast<PlanAircraftDTO>().Any(pa => pa.Id == _selPlanHistory.PlanAircraftId))
                            SelPlanAircraft = ViewPlanAircrafts.SourceCollection.Cast<PlanAircraftDTO>().FirstOrDefault(p => p.Id == _selPlanHistory.PlanAircraftId);
                        else SelPlanAircraft = null;//根据选择的计划明细找到关联的计划飞机

                        if (Aircrafts.SourceCollection.Cast<AircraftDTO>().Any(pa => pa.AircraftId == value.AircraftId))
                            SelAircraft = Aircrafts.SourceCollection.Cast<AircraftDTO>().FirstOrDefault(p => p.AircraftId == value.AircraftId);
                        else SelAircraft = null;//根据选择的计划明细找到关联的运营飞机
                    }
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

        #region 重写保存前操作

        protected override bool OnSaveExecuting(object sender)
        {
            var ph = AllPlanHistories.ToList();
            ph.ForEach(p =>
            {
                p.ActionCategories = new ObservableCollection<ActionCateDTO>();
                p.AircraftTypes = new ObservableCollection<AircraftTyDTO>();
                p.AircraftCategories = new ObservableCollection<AircraftCateDTO>();
            });
            return true;
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
            var newPlan = _service.CreateNewVersionPlan(lastPlan, AllPlanHistories);//创建新版本的计划

            Plans.AddNew(newPlan);
            foreach (var ph in AllPlanHistories.SourceCollection.Cast<PlanHistoryDTO>())
            {
                ph.ActionCategories.AddRange(_service.GetActionCategoriesForPlanHistory(ph));
                ph.AircraftCategories.AddRange(_service.GetAircraftCategoriesForPlanHistory(ph));
                ph.AircraftTypes.AddRange(_service.GetAircraftTypesForPlanHistory(ph));
            }
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
            EditPlanAircraft = null;

            OpenEditDialog(null, PlanDetailCreateSource.New);
            //将新建的实体添加到对应的注册集合中
            ViewPlanHistories.Add(EditPlanHistory);
            AllPlanHistories.AddNew(EditPlanHistory);
            SelPlanHistory = EditPlanHistory;
            ViewPlanAircrafts.AddNew(EditPlanAircraft);
            SelPlanAircraft = EditPlanAircraft;
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
                //获取计划飞机在当前计划中所有的计划明细历史
                var planDetails = ViewPlanHistories.Where(ph => ph.PlanAircraftId == planAircraft.Id).ToList();
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
                    return SelPlanHistory.ActionType != "引进";
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
                    var planHistories =
                        AllPlanHistories.Where(ph => ph.PlanAircraftId == planAircraft.Id).ToList();

                    // 获取计划飞机在当前计划中的明细项集合
                    var planDetails = ViewPlanHistories.Where(ph => ph.PlanAircraftId == planAircraft.Id).ToList();

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
                            if (planDetail.ActionType == "变更")
                                planAircraft.Status = (int)ManageStatus.运营;
                            // 移除的是退出计划，不做任何改变
                        }
                    }
                    // 2、没有飞机（只有引进与退出计划）
                    // 2.1、计划飞机相关的明细项数量为1
                    // 删除相关计划飞机。
                    else if (planHistories.Count == 1)
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

                    AllPlanHistories.Remove(planDetail);
                    ViewPlanHistories.Remove(planDetail);
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
            if (!ViewPlanHistories.Any())
            {
                MessageAlert("计划明细不能为空!");
            }
            if (CurPlan != null)
            {
                CurPlan.Status = (int)PlanStatus.待审核;
                IsPlanReadOnly = true;
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
                IsPlanReadOnly = true;
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
                if (string.Equals(cell.Column.UniqueName, "ActionType"))
                {
                    var planhistory = gridView.CurrentCellInfo.Item as PlanHistoryDTO;
                    if (planhistory != null)
                    {
                        planhistory.AircraftCategories = new ObservableCollection<AircraftCateDTO>();
                        planhistory.AircraftCategories.AddRange(_service.GetAircraftCategoriesForPlanHistory(planhistory));

                        var actionCategory =
                            SelPlanHistory.ActionCategories.FirstOrDefault(p => p.Id == planhistory.ActionCategoryId);
                        if (actionCategory != null && actionCategory.NeedRequest)
                        {
                            planhistory.NeedRequest = actionCategory.NeedRequest;
                            planhistory.IsSubmit = true;//将“是否上报”初始为“是”
                            planhistory.CanRequest = (int)CanRequest.未报计划;
                            planhistory.CanDeliver = (int)CanDeliver.未申请;
                        }
                        else if (actionCategory != null && !actionCategory.NeedRequest)
                        {
                            planhistory.NeedRequest = actionCategory.NeedRequest;
                            planhistory.IsSubmit = false;//将“是否上报”初始为“否”
                            planhistory.CanRequest = (int)CanRequest.无需申请;
                            planhistory.CanDeliver = (int)CanDeliver.可交付;
                        }
                    }
                }
                if (string.Equals(cell.Column.UniqueName, "Regional"))
                {
                    var planhistory = gridView.CurrentCellInfo.Item as PlanHistoryDTO;
                    if (planhistory != null)
                    {
                        var planAircraft = ViewPlanAircrafts.FirstOrDefault(p => p.Id == planhistory.PlanAircraftId);
                        if (planAircraft != null)
                        {
                            planhistory.AircraftTypes = new ObservableCollection<AircraftTyDTO>();
                            planhistory.AircraftTypes.AddRange(_service.GetAircraftTypesForPlanHistory(planhistory));
                            planAircraft.Regional = planhistory.Regional;
                        }
                    }
                }
                else if (string.Equals(cell.Column.UniqueName, "AircraftType"))
                {
                    var planhistory = gridView.CurrentCellInfo.Item as PlanHistoryDTO;
                    if (planhistory != null)
                    {
                        var planAircraft = ViewPlanAircrafts.FirstOrDefault(p => p.Id == planhistory.PlanAircraftId);
                        if (planAircraft != null && planhistory.AircraftTypeId != Guid.Empty)
                        {
                            var aircraftType = planhistory.AircraftTypes.FirstOrDefault(p => p.Id == planhistory.AircraftTypeId);
                            if (aircraftType != null)
                            {
                                planhistory.CaacAircraftTypeName = aircraftType.CaacAircraftTypeName;
                                planAircraft.AircraftTypeId = planhistory.AircraftTypeId;
                                planAircraft.AircraftTypeName = aircraftType.Name;
                            }
                        }
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

        private PlanHistoryDTO _editPlanHistory;

        /// <summary>
        /// 当前编辑的计划明细项
        /// </summary>
        public PlanHistoryDTO EditPlanHistory
        {
            get { return _editPlanHistory; }
            private set
            {
                if (_editPlanHistory != value)
                {
                    _editPlanHistory = value;
                    RaisePropertyChanged(() => EditPlanHistory);
                }
            }
        }

        #endregion

        #region 当前编辑计划明细关联的的计划飞机

        /// <summary>
        /// 当前编辑计划明细关联的的计划飞机
        /// </summary>
        public PlanAircraftDTO EditPlanAircraft;

        #endregion

        #region 业务逻辑处理
        /// <summary>
        /// 打开子窗体之前判断是否要打开
        /// </summary>
        /// <param name="planAircraft">计划飞机</param>
        /// <param name="source">调用的来源</param>
        public void OpenEditDialog(PlanAircraftDTO planAircraft, PlanDetailCreateSource source)
        {
            EditPlanAircraft = planAircraft;
            EditPlanHistory = null;
            _operationPlan = null;
            _changePlan = null;
            // 获取计划飞机在当前计划中的明细项集合
            var planDetails = new List<PlanHistoryDTO>();
            if (CurPlan != null && EditPlanAircraft != null)
                planDetails = ViewPlanHistories.Where(ph => ph.PlanAircraftId == EditPlanAircraft.Id).ToList();

            // 1、计划飞机在当前计划中没有明细项(新增计划明细或计划飞机为预备状态）
            if (!planDetails.Any())
                ShowEditDialog(null, source);
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
                                {
                                    ShowEditDialog(planDetails[0], source);
                                }

                            });
                            break;
                        case PlanDetailCreateSource.Aircraft:
                            content = planDetails[0].ActionType.Contains("变更")
                                          ? "飞机在当前计划中已有变更计划明细项，是否要针对该飞机添加退出计划明细项？"
                                          : "飞机在当前计划中已有退出计划明细项，是否要针对该飞机添加变更计划明细项？";
                            MessageConfirm("确认添加计划明细", content, (o, e) =>
                            {
                                if (e.DialogResult == true)
                                {
                                    ShowEditDialog(planDetails[0], source);
                                }

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

        public void ShowEditDialog(PlanHistoryDTO existDetail, PlanDetailCreateSource source)
        {
            switch (source)
            {
                case PlanDetailCreateSource.New:
                    IsPlanTypeVisible = Visibility.Collapsed;
                    _operationPlan = _service.CreatePlanHistory(CurPlan, AllPlanHistories, ref EditPlanAircraft, null, "引进", 1); //此时EditPlanAircraft=null
                    EditPlanHistory = _operationPlan;
                    IsChangeable = true;
                    break;
                case PlanDetailCreateSource.PlanAircraft:
                    IsPlanTypeVisible = Visibility.Collapsed;
                    // 计划飞机已有的明细项肯定是引进计划，只能添加退出计划
                    _operationPlan = _service.CreatePlanHistory(CurPlan, AllPlanHistories, ref EditPlanAircraft, null, existDetail != null ? "退出" : "引进", 1); //existDetail=null为计划飞机是预备状态的情况
                    EditPlanHistory = _operationPlan;
                    ViewPlanHistories.Add(EditPlanHistory);
                    AllPlanHistories.AddNew(EditPlanHistory);
                    //这时不能修改机型和座机
                    IsChangeable = false;
                    break;
                case PlanDetailCreateSource.Aircraft:
                    IsPlanTypeVisible = Visibility.Visible;
                    // 1、计划飞机已有明细项
                    if (existDetail != null)
                    {
                        // 已有的是变更计划，只能添加退出计划
                        if (existDetail.ActionType == "变更")
                        {
                            IsChangeEnabled = false;
                            IsOperation = true;
                            IsChange = false;
                            OnOperation();
                            ViewPlanHistories.Add(EditPlanHistory);
                            AllPlanHistories.AddNew(EditPlanHistory);
                        }
                        // 已有的是退出计划，只能添加变更计划
                        else
                        {
                            IsOperationEnabled = false;
                            IsOperation = false;
                            IsChange = true;
                            OnOperation();//生成之后，不让用户编辑，起到保存原计划历史的机型的作用，在取消时，能够用来恢复计划飞机数据
                            OnChange();
                            ViewPlanHistories.Add(EditPlanHistory);
                            AllPlanHistories.AddNew(EditPlanHistory);
                        }
                    }
                    // 2、计划飞机没有明细项
                    else
                    {
                        IsChangeEnabled = true;
                        IsOperationEnabled = true;
                        if (!IsOperation) IsOperation = true;
                        OnOperation();
                        ViewPlanHistories.Add(EditPlanHistory);
                        AllPlanHistories.AddNew(EditPlanHistory);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("source");
            }

            EditDialog.ShowDialog();
        }

        #region 调用业务逻辑处理

        private void OnOperation()
        {
            if (_operationPlan == null && EditPlanAircraft!=null)
            {
                var aircraft =
                    Aircrafts.SourceCollection.Cast<AircraftDTO>()
                        .FirstOrDefault(p => p.AircraftId == EditPlanAircraft.AircraftId);
                // 针对运营飞机的运营计划只能是退出
                _operationPlan = _service.CreatePlanHistory(CurPlan, AllPlanHistories, ref EditPlanAircraft, aircraft, "退出", 1);
            }
            EditPlanHistory = _operationPlan;
            IsChangeable = false;
        }

        private void OnChange()
        {
            if (_changePlan == null && EditPlanAircraft != null)
            {
                var aircraft =
                    Aircrafts.SourceCollection.Cast<AircraftDTO>()
                        .FirstOrDefault(p => p.AircraftId == EditPlanAircraft.AircraftId);
                _changePlan = _service.CreatePlanHistory(CurPlan, AllPlanHistories, ref EditPlanAircraft, aircraft, "变更", 2);
            }
            EditPlanHistory = _changePlan;
            IsChangeable = true;
        }

        #endregion
        #endregion

        #region 选中运营计划

        private bool _isOperation;

        /// <summary>
        /// 选中运营计划
        /// </summary>
        public bool IsOperation
        {
            get { return _isOperation; }
            private set
            {
                if (_isOperation != value)
                {
                    _isOperation = value;
                    RaisePropertyChanged(() => IsOperation);
                    OnOperation();
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
            get { return _isChange; }
            private set
            {
                if (_isChange != value)
                {
                    _isChange = value;
                    RaisePropertyChanged(() => IsChange);
                    OnChange();
                }
            }
        }

        #endregion

        #region 选择的活动类型

        private ActionCateDTO _selActionCategory;

        /// <summary>
        /// 选择的活动类型
        /// </summary>
        public ActionCateDTO SelActionCategory
        {
            get { return _selActionCategory; }
            private set
            {
                if (_selActionCategory != value)
                {
                    _selActionCategory = value;
                    if (value != null)
                    {
                        if (value.NeedRequest)
                        {
                            EditPlanHistory.NeedRequest = value.NeedRequest;
                            EditPlanHistory.IsSubmit = true;//将“是否上报”初始为“是”
                            EditPlanHistory.CanRequest = (int)CanRequest.未报计划;
                            EditPlanHistory.CanDeliver = (int)CanDeliver.未申请;
                        }
                        else
                        {
                            EditPlanHistory.NeedRequest = value.NeedRequest;
                            EditPlanHistory.IsSubmit = false;//将“是否上报”初始为“否”
                            EditPlanHistory.CanRequest = (int)CanRequest.无需申请;
                            EditPlanHistory.CanDeliver = (int)CanDeliver.可交付;
                        }
                        EditPlanHistory.ActionCategoryId = value.Id;
                        EditPlanHistory.ActionType = value.ActionType;
                        EditPlanHistory.ActionName = value.ActionName;
                        EditPlanHistory.AircraftCategories = new ObservableCollection<AircraftCateDTO>();
                        EditPlanHistory.AircraftCategories.AddRange(_service.GetAircraftCategoriesForPlanHistory(EditPlanHistory));
                    }
                    RaisePropertyChanged(() => SelActionCategory);
                }
            }
        }

        #endregion

        #region 选择的座级

        private AircraftCateDTO _selAircraftCategory;

        /// <summary>
        /// 选择的座级
        /// </summary>
        public AircraftCateDTO SelAircraftCategory
        {
            get { return _selAircraftCategory; }
            set
            {
                if (_selAircraftCategory != value)
                {
                    _selAircraftCategory = value;
                    if (value != null)
                    {
                        EditPlanHistory.Regional = value.Regional;
                        EditPlanHistory.AircraftTypes = new ObservableCollection<AircraftTyDTO>();
                        EditPlanHistory.AircraftTypes.AddRange(_service.GetAircraftTypesForPlanHistory(EditPlanHistory));
                        EditPlanAircraft.Regional = value.Regional;
                        RaisePropertyChanged(() => SelAircraftType);
                    }
                    RaisePropertyChanged(() => SelAircraftCategory);
                }
            }
        }

        #endregion

        #region 选择的机型

        private AircraftTyDTO _selAircraftType;

        /// <summary>
        /// 选择的机型
        /// </summary>
        public AircraftTyDTO SelAircraftType
        {
            get { return _selAircraftType; }
            set
            {
                if (_selAircraftType != value)
                {
                    _selAircraftType = value;
                    if (value != null)
                    {
                        EditPlanHistory.Regional = value.Regional;
                        EditPlanHistory.AircraftTypeName = value.Name;
                        EditPlanHistory.AircraftTypeId = value.Id;
                        EditPlanHistory.CaacAircraftTypeName = value.CaacAircraftTypeName;
                        EditPlanAircraft.AircraftTypeId = value.Id;
                        EditPlanAircraft.AircraftTypeName = value.Name;
                    }
                    RaisePropertyChanged(() => SelAircraftType);
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
            get { return _isOperationEnabled; }
            private set
            {
                if (_isOperationEnabled != value)
                {
                    _isOperationEnabled = value;
                    RaisePropertyChanged(() => IsOperationEnabled);
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
            get { return _isChangeEnabled; }
            private set
            {
                if (_isChangeEnabled != value)
                {
                    _isChangeEnabled = value;
                    RaisePropertyChanged(() => IsChangeEnabled);
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
            get { return _isPlanTypeVisible; }
            private set
            {
                if (_isPlanTypeVisible != value)
                {
                    _isPlanTypeVisible = value;
                    RaisePropertyChanged(() => IsPlanTypeVisible);
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
            get { return _isAicraftTypeAndRegionalChangeable; }
            private set
            {
                if (_isAicraftTypeAndRegionalChangeable != value)
                {
                    _isAicraftTypeAndRegionalChangeable = value;
                    RaisePropertyChanged(() => IsAicraftTypeAndRegionalChangeable);
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
                if (_isChangeable != value)
                {
                    _isChangeable = value;
                    RaisePropertyChanged(() => IsChangeable);
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
            EditDialog.Close();
        }

        #endregion

        #region 取消

        /// <summary>
        /// 取消
        /// </summary>
        public DelegateCommand<object> CancelCommand { get; private set; }

        private void OnCancel(object obj)
        {
            var planAircraft = ViewPlanAircrafts.FirstOrDefault(p => EditPlanHistory.PlanAircraftId == p.Id);
            if (planAircraft != null && planAircraft.AircraftId == null)
            {
                var planAircraftPlanHistories = AllPlanHistories.Where(p => p.PlanAircraftId == planAircraft.Id);
                if (planAircraftPlanHistories.Count() == 1)
                {
                    ViewPlanAircrafts.Remove(planAircraft);
                }
            }
            AllPlanHistories.Remove(EditPlanHistory);
            ViewPlanHistories.Remove(EditPlanHistory);
            OnCancelExecute(obj);
        }

        protected virtual bool CanCancel(object obj)
        {
            return true;
        }

        protected virtual void OnCancelExecute(object sender)
        {
            EditDialog.Close();
        }

        #endregion
        #endregion
    }
}