#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/25 16:45:50
// 文件名：FleetPlanDeliverVM
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace UniCloud.Presentation.FleetPlan.PerformFleetPlan
{
    [Export(typeof(FleetPlanDeliverVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FleetPlanDeliverVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;
        private AnnualDTO _curAnnual = new AnnualDTO();
        private AircraftDTO EditAircraft;
        private FilterDescriptor _annualDescriptor;
        private FilterDescriptor _planDescriptor;
        private FilterDescriptor _planHistoryDescriptor;

        [ImportingConstructor]
        public FleetPlanDeliverVM(IRegionManager regionManager, IFleetPlanService service)
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
            CurAnnual = _service.CreateCollection(_context.Annuals);
            _annualDescriptor = new FilterDescriptor("IsOpen", FilterOperator.IsEqualTo, true);
            CurAnnual.FilterDescriptors.Add(_annualDescriptor);
            CurAnnual.LoadedData += (sender, e) =>
            {
                if (CurAnnual.Count != 0)
                {
                    _curAnnual = CurAnnual.FirstOrDefault();
                    if (_curAnnual != null)
                    {
                        _planDescriptor.Value = _curAnnual.Year;
                        if (!Plans.AutoLoad)
                            Plans.AutoLoad = true;
                        else
                            Plans.Load(true);
                    }
                    Plans.Load(true);
                }
            };

            Plans = _service.CreateCollection(_context.Plans);
            _planDescriptor = new FilterDescriptor("Year", FilterOperator.IsEqualTo,-1);
            Plans.FilterDescriptors.Add(_planDescriptor);
            Plans.LoadedData += (sender, e) =>
            {
                CurPlan = Plans.OrderBy(p => p.VersionNumber).LastOrDefault();
                if (CurPlan != null)
                {
                    _planHistoryDescriptor.Value = CurPlan.Id;
                    if (!CurPlanHistories.AutoLoad)
                        CurPlanHistories.AutoLoad = true;
                    else
                        CurPlanHistories.Load(true);
                }
                RefreshCommandState();
            };
            _service.RegisterCollectionView(Plans);//注册查询集合

            CurPlanHistories = _service.CreateCollection(_context.PlanHistories);
            _planHistoryDescriptor=new FilterDescriptor("PlanId",FilterOperator.IsEqualTo,Guid.Empty);
            CurPlanHistories.FilterDescriptors.Add(_planHistoryDescriptor);
            CurPlanHistories.LoadedData += (sender, e) =>
            {
                ViewPlanHistories=new ObservableCollection<PlanHistoryDTO>();
                foreach (var ph in CurPlanHistories.SourceCollection.Cast<PlanHistoryDTO>())
                {
                    ph.ActionCategories.AddRange(_service.GetActionCategoriesForPlanHistory(ph));
                    ph.AircraftCategories.AddRange(_service.GetAircraftCategoriesForPlanHistory(ph));
                    ph.AircraftTypes.AddRange(_service.GetAircraftTypesForPlanHistory(ph));
                    _context.ChangeState(ph, EntityStates.Unchanged);
                    ViewPlanHistories.Add(ph);
                }
                RefreshCommandState();
            };
            _service.RegisterCollectionView(CurPlanHistories);//注册查询集合

            Aircrafts = _service.CreateCollection(_context.Aircrafts);
            //var sort = new SortDescriptor { Member = "OperateStatus", SortDirection = ListSortDirection.Descending };
            //Aircrafts.SortDescriptors.Add(sort);
            _service.RegisterCollectionView(Aircrafts);//注册查询集合
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            CompleteCommand = new DelegateCommand<object>(OnComplete, CanComplete);
            CommitCommand = new DelegateCommand<object>(OnCommit, CanCommit);
            CheckCommand = new DelegateCommand<object>(OnCheck, CanCheck);
            SendCommand = new DelegateCommand<object>(OnSend, CanSend);
            RepealCommand = new DelegateCommand<object>(OnRepeal, CanRepeal);
            OkCommand = new DelegateCommand<object>(OnOk, CanOk);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 当前计划年度

        /// <summary>
        ///     当前计划年度
        /// </summary>
        public QueryableDataServiceCollectionView<AnnualDTO> CurAnnual { get; set; }

        #endregion

        #region 当前年度所有运力增减计划集合

        /// <summary>
        /// 当前年度所有运力增减计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> Plans { get; set; }

        #endregion

        #region 当前计划的计划明细集合

        /// <summary>
        /// 当前计划的计划明细集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanHistoryDTO> CurPlanHistories { get; set; }

 
        private ObservableCollection<PlanHistoryDTO> _viewPlanHistories=new ObservableCollection<PlanHistoryDTO>();

        /// <summary>
        /// 当前计划的计划明细集合
        /// </summary>
        public ObservableCollection<PlanHistoryDTO> ViewPlanHistories
        {
            get { return this._viewPlanHistories; }
            private set
            {
                if (this._viewPlanHistories != value)
                {
                    _viewPlanHistories = value;
                    this.RaisePropertyChanged(() => this.ViewPlanHistories);
                }
            }
        }
        
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
            AircraftTypes = _service.GetAircraftTypes(() => { });
            ActionCategories = _service.GetActionCategories(() => { });
            AircraftCategories = _service.GetAircraftCategories(() => { });

            CurAnnual.Load(true);

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

                    if (this._selPlanHistory != null)
                    {
                        if (Aircrafts.Any(pa => pa.AircraftId == value.AircraftId))
                            SelAircraft = Aircrafts.SourceCollection.Cast<AircraftDTO>().FirstOrDefault(p => p.AircraftId == value.AircraftId);
                        else SelAircraft = null;
                    }
                    RefreshCommandState();
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
                    OperationHistories=new ObservableCollection<OperationHistoryDTO>();
                    AircraftBusinesses=new ObservableCollection<AircraftBusinessDTO>();

                    if (_selAircraft != null)
                    {
                        foreach (var op in value.OperationHistories)
                        {
                            OperationHistories.Add(op);
                        }
                        foreach (var ab in value.AircraftBusinesses)
                        {
                            AircraftBusinesses.Add(ab);
                        }
                        //选择相关的计划明细
                        if (CurPlanHistories.Any(pa => pa.AircraftId == value.AircraftId))
                            SelPlanHistory = CurPlanHistories.FirstOrDefault(p => p.AircraftId == value.AircraftId);
                        else SelPlanHistory = null;
                    }
                    this.SelOperationHistory = this.OperationHistories.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                    //如果为变更计划，则这个时候取到的SelOperationHistory为空，重新取集合中的最后一条来展示在子窗体中
                    if (SelOperationHistory == null)
                        SelOperationHistory = this.OperationHistories.LastOrDefault();
                    this.SelAircraftBusiness = this.AircraftBusinesses.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                    //如果为退出计划，则这个时候取到的SelAircraftBusiness为空，重新取集合中的最后一条来展示在子窗体中
                    if (SelAircraftBusiness == null)
                        SelAircraftBusiness = this.AircraftBusinesses.LastOrDefault();

                    RaisePropertyChanged(() => SelAircraft);
                    RaisePropertyChanged(() => OhTitle);
                    RaisePropertyChanged(() => AbTitle);

                }
            }
        }

        #endregion

        #region 运营权历史集合
        private ObservableCollection<OperationHistoryDTO> _operationHistories = new ObservableCollection<OperationHistoryDTO>();

        /// <summary>
        ///     运营权历史集合
        /// </summary>
        public ObservableCollection<OperationHistoryDTO> OperationHistories
        {
            get { return _operationHistories; }
            private set
            {
                if (_operationHistories != value)
                {
                    _operationHistories = value;
                    RaisePropertyChanged(() => OperationHistories);
                }
            }
        }

        #endregion

        #region 运营权历史标题

        /// <summary>
        /// 运营权历史标题
        /// </summary>
        public string OhTitle
        {
            get
            {
                if (SelAircraft != null)
                {
                    var sb = new StringBuilder();
                    sb.Append(SelAircraft.RegNumber);
                    sb.Append("运营权历史");
                    return sb.ToString();
                }
                return null;
            }
        }

        #endregion

        #region 选择的运营权历史
        private OperationHistoryDTO _selOperationHistory;

        /// <summary>
        ///     选择的运营权历史
        /// </summary>
        public OperationHistoryDTO SelOperationHistory
        {
            get { return _selOperationHistory; }
            private set
            {
                if (_selOperationHistory != value)
                {
                    _selOperationHistory = value;
                    RaisePropertyChanged(() => SelOperationHistory);
                }
            }
        }

        #endregion

        #region 商业数据历史集合
        private ObservableCollection<AircraftBusinessDTO> _aircraftBusinesses = new ObservableCollection<AircraftBusinessDTO>();

        /// <summary>
        ///     商业数据历史集合
        /// </summary>
        public ObservableCollection<AircraftBusinessDTO> AircraftBusinesses
        {
            get { return _aircraftBusinesses; }
            private set
            {
                if (_aircraftBusinesses != value)
                {
                    _aircraftBusinesses = value;
                    RaisePropertyChanged(() => AircraftBusinesses);
                }
            }
        }

        #endregion

        #region 商业数据历史标题

        /// <summary>
        /// 商业数据历史标题
        /// </summary>
        public string AbTitle
        {
            get
            {
                if (SelAircraft != null)
                {
                    var sb = new StringBuilder();
                    sb.Append(SelAircraft.RegNumber);
                    sb.Append("商业数据历史");
                    return sb.ToString();
                }
                return null;
            }
        }

        #endregion

        #region 选择的商业数据历史

        private AircraftBusinessDTO _selAircraftBusiness;

        /// <summary>
        ///     选择的商业数据历史
        /// </summary>
        public AircraftBusinessDTO SelAircraftBusiness
        {
            get { return _selAircraftBusiness; }
            private set
            {
                if (_selAircraftBusiness != value)
                {
                    _selAircraftBusiness = value;
                    RaisePropertyChanged(() => SelAircraftBusiness);
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
            CompleteCommand.RaiseCanExecuteChanged();
            CommitCommand.RaiseCanExecuteChanged();
            CheckCommand.RaiseCanExecuteChanged();
            SendCommand.RaiseCanExecuteChanged();
            RepealCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 完成计划

        /// <summary>
        ///     完成计划
        /// </summary>
        public DelegateCommand<object> CompleteCommand { get; private set; }

        private void OnComplete(object obj)
        {
            EditAircraft = new AircraftDTO();
            var aircraft = new AircraftDTO();
            //获取计划明细对应的飞机，可能为空
            if (this.SelPlanHistory != null)
            {
                if (Aircrafts.Any(pa => pa.AircraftId == SelPlanHistory.AircraftId))
                    aircraft = Aircrafts.SourceCollection.Cast<AircraftDTO>().FirstOrDefault(p => p.AircraftId == SelPlanHistory.AircraftId);
                else aircraft = null;
            }
            // 调用完成计划的操作
            _service.CompletePlan(this.SelPlanHistory, aircraft, ref EditAircraft);

            if (EditAircraft == null) return;
            Aircrafts.AddNew(EditAircraft);
            // 定位选中的飞机，并确保运营历史、商业数据历史刷新
            if (SelAircraft != EditAircraft)
            {
                this.SelAircraft = EditAircraft;
            }
            this.OpenEditDialog(this.SelPlanHistory);
        }

        private bool CanComplete(object obj)
        {
            // 有未保存内容时，按钮不可用
            if (_service.HasChanges)
            {
                return false;
            }
            // 没有选中的计划明细时，按钮不可用
            if (this.SelPlanHistory == null)
            {
                return false;
            }
            // 选中计划明细的完成状态为无，且计划明细为可交付时，按钮可用
            return this.SelPlanHistory.CompleteStatus == CompleteStatus.无状态 && this.SelPlanHistory.CanDeliver == CanDeliver.可交付;
        }

        #endregion

        #region 提交审核

        /// <summary>
        ///     提交审核
        /// </summary>
        public DelegateCommand<object> CommitCommand { get; private set; }

        private void OnCommit(object obj)
        {
            var aircraft = new AircraftDTO();
            //如果是引进计划，则需要同时修改其商业数据历史的状态
            if (SelPlanHistory.ActionType == "引进")
            {
                if (SelPlanHistory.AircraftId != null)
                {
                    aircraft = Aircrafts.FirstOrDefault(p => p.AircraftId == SelPlanHistory.AircraftId);
                    if (aircraft != null)
                    {
                        var aircraftBusiness =
                            aircraft.AircraftBusinesses.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                        if (aircraftBusiness != null)
                            aircraftBusiness.Status = (int)OperationStatus.待审核;
                    }
                }
            }
            aircraft = Aircrafts.FirstOrDefault(p => p.AircraftId == SelPlanHistory.AircraftId);
            if (aircraft != null)
            {
                if (SelPlanHistory.PlanType == 1)//运营计划 
                {
                    var operationHistory =
                      aircraft.OperationHistories.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                    if (operationHistory != null)
                    {
                        operationHistory.Status = (int)OperationStatus.待审核;
                        SelPlanHistory.RelatedStatus = (int)OperationStatus.待审核;
                    }
                }
                if (SelPlanHistory.PlanType == 1)//变更计划
                {
                    var aircraftBusiness =
                      aircraft.AircraftBusinesses.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                    if (aircraftBusiness != null)
                    {
                        aircraftBusiness.Status = (int)OperationStatus.待审核;
                        SelPlanHistory.RelatedStatus = (int)OperationStatus.待审核;
                    }
                }
            }
            RefreshCommandState();
        }

        private bool CanCommit(object obj)
        {
            // 有未保存内容时，按钮不可用
            if (_service.HasChanges)
            {
                return false;
            }
            // 没有选中的计划明细时，按钮不可用
            if (this.SelPlanHistory == null)
            {
                return false;
            }
            // 选中计划明细的完成状态为草稿时，按钮可用
            return this.SelPlanHistory.CompleteStatus == CompleteStatus.草稿;
        }

        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> CheckCommand { get; private set; }

        private void OnCheck(object obj)
        {
            var aircraft = new AircraftDTO();
            //如果是引进计划，则需要同时修改其商业数据历史的状态
            if (SelPlanHistory.ActionType == "引进")
            {
                if (SelPlanHistory.AircraftId != null)
                {
                    aircraft = Aircrafts.FirstOrDefault(p => p.AircraftId == SelPlanHistory.AircraftId);
                    if (aircraft != null)
                    {
                        var aircraftBusiness =
                            aircraft.AircraftBusinesses.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                        if (aircraftBusiness != null)
                            aircraftBusiness.Status = (int)OperationStatus.已审核;
                    }
                }
            }
            aircraft = Aircrafts.FirstOrDefault(p => p.AircraftId == SelPlanHistory.AircraftId);
            if (aircraft != null)
            {
                if (SelPlanHistory.PlanType == 1)//运营计划 
                {
                    var operationHistory =
                      aircraft.OperationHistories.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                    if (operationHistory != null)
                    {
                        operationHistory.Status = (int)OperationStatus.已审核;
                        SelPlanHistory.RelatedStatus = (int)OperationStatus.已审核;
                    }
                }
                if (SelPlanHistory.PlanType == 1)//变更计划
                {
                    var aircraftBusiness =
                      aircraft.AircraftBusinesses.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                    if (aircraftBusiness != null)
                    {
                        aircraftBusiness.Status = (int)OperationStatus.已审核;
                        SelPlanHistory.RelatedStatus = (int)OperationStatus.已审核;
                    }
                }
            }
        }

        private bool CanCheck(object obj)
        {
            // 有未保存内容时，按钮不可用
            if (_service.HasChanges)
            {
                return false;
            }
            // 没有选中的计划明细时，按钮不可用
            if (this.SelPlanHistory == null)
            {
                return false;
            }
            // 选中计划明细的完成状态为审核时，按钮可用
            return this.SelPlanHistory.CompleteStatus == CompleteStatus.审核;
        }
        #endregion

        #region 发送

        /// <summary>
        ///     发送
        /// </summary>
        public DelegateCommand<object> SendCommand { get; private set; }

        private void OnSend(object obj)
        {
            var content = "是否向【民航局】报送" + this.SelPlanHistory.RegNumber + "计划完成情况？";
            MessageConfirm("确认报送计划完成情况", content, (o, e) =>
             {
                 if (e.DialogResult == true)
                 {
                     var aircraft = new AircraftDTO();
                     //如果是引进计划，则需要同时修改其商业数据历史的状态
                     if (SelPlanHistory.ActionType == "引进")
                     {
                         if (SelPlanHistory.AircraftId != null)
                         {
                             aircraft = Aircrafts.FirstOrDefault(p => p.AircraftId == SelPlanHistory.AircraftId);
                             if (aircraft != null)
                             {
                                 var aircraftBusiness =
                                     aircraft.AircraftBusinesses.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                                 if (aircraftBusiness != null)
                                     aircraftBusiness.Status = (int)OperationStatus.已提交;
                             }
                         }
                     }
                     // 审核、已提交状态下可以发送。如果已处于提交状态，需要重新发送的，不必改变状态。
                     aircraft = Aircrafts.FirstOrDefault(p => p.AircraftId == SelPlanHistory.AircraftId);
                     if (aircraft != null)
                     {
                         if (SelPlanHistory.PlanType == 1)//运营计划 
                         {
                             var operationHistory =
                               aircraft.OperationHistories.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                             if (operationHistory != null)
                             {
                                 operationHistory.Status = (int)OperationStatus.已提交;
                                 SelPlanHistory.RelatedStatus = (int)OperationStatus.已提交;
                             }
                         }
                         if (SelPlanHistory.PlanType == 1)//变更计划
                         {
                             var aircraftBusiness =
                               aircraft.AircraftBusinesses.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                             if (aircraftBusiness != null)
                             {
                                 aircraftBusiness.Status = (int)OperationStatus.已提交;
                                 SelPlanHistory.RelatedStatus = (int)OperationStatus.已提交;
                             }
                         }
                     }
                     //_service.SubmitChanges(sc =>
                     //{
                     //    if (sc.Error == null)
                     //    {
                     //        // 发送不成功的，也认为是已经做了发送操作，不回滚状态。始终可以重新发送。
                     //        this.service.TransferPlanHistory(this.SelPlanHistory.PlanHistoryID, tp => { }, null);
                     //        RefreshView();
                     //    }
                     //}, null);
                 }
             });
        }

        private bool CanSend(object obj)
        {
            // 有未保存内容时，按钮不可用
            if (_service.HasChanges) return false;
            // 没有选中的计划明细时，按钮不可用
            if (this.SelPlanHistory == null) return false;
            // 选中计划明细的完成状态为已审核或已提交时，按钮可用
            return this.SelPlanHistory.CompleteStatus > CompleteStatus.审核;
        }

        #endregion

        #region 修改完成

        /// <summary>
        ///     修改完成
        /// </summary>
        public DelegateCommand<object> RepealCommand { get; private set; }

        private void OnRepeal(object obj)
        {
            const string content = "确认后计划完成状态将改为草稿并允许编辑，是否要对该计划明细进行修改？";
            MessageConfirm("确认修改计划完成情况", content, (o, e) =>
             {
                 if (e.DialogResult == true)
                 {
                     var aircraft = Aircrafts.FirstOrDefault(p => p.AircraftId == SelPlanHistory.AircraftId);
                     if (aircraft != null)
                     {
                         if (SelPlanHistory.PlanType == 1)//运营计划 
                         {
                             var operationHistory =
                               aircraft.OperationHistories.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                             if (operationHistory != null)
                             {
                                 operationHistory.Status = (int)OperationStatus.草稿;
                                 SelPlanHistory.RelatedStatus = (int)OperationStatus.草稿;
                             }
                         }
                         if (SelPlanHistory.PlanType == 1)//变更计划
                         {
                             var aircraftBusiness =
                               aircraft.AircraftBusinesses.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                             if (aircraftBusiness != null)
                             {
                                 aircraftBusiness.Status = (int)OperationStatus.草稿;
                                 SelPlanHistory.RelatedStatus = (int)OperationStatus.草稿;
                             }
                         }
                     }
                     RefreshCommandState();
                 }
             });
        }

        private bool CanRepeal(object obj)
        {
            // 有未保存内容时，按钮不可用
            if (_service.HasChanges) return false;
            // 没有选中的计划明细时，按钮不可用
            if (this.SelPlanHistory == null) return false;
            // 选中计划明细的完成状态不是无和草稿时，按钮可用
            return this.SelPlanHistory.CompleteStatus != CompleteStatus.无状态 && this.SelPlanHistory.CompleteStatus != CompleteStatus.草稿;
        }

        #endregion
        #endregion

        #region 子窗体相关
        [Import]
        public PlanDeliverEditDialog EditDialog;


        /// <summary>
        /// 打开子窗体之前先设置好子窗体中的编辑属性
        /// </summary>
        /// <param name="planHistory">计划明细</param>
        internal void OpenEditDialog(PlanHistoryDTO planHistory)
        {
            this.ShowEditDialog();

        }

        private void ShowEditDialog()
        {
            this.IsAircraft = true;
            this.IsOperationHistory = false;
            this.IsAircraftBusiness = false;
            this.EditDialog.ShowDialog();
        }


        #region 子窗体选择运营飞机

        private bool _isAircraft;

        /// <summary>
        /// 选中变更计划
        /// </summary>
        public bool IsAircraft
        {
            get { return this._isAircraft; }
            private set
            {
                if (this._isAircraft != value)
                {
                    this._isAircraft = value;
                    if (value == true)
                    {
                        this.IsAircraftVisibility = Visibility.Visible;
                    }
                    else
                    {
                        this.IsAircraftVisibility = Visibility.Collapsed;
                    }
                    this.RaisePropertyChanged(() => this.IsAircraft);
                }
            }
        }

        #endregion

        #region 子窗体选择飞机运营历史

        private bool _isOperationHistory;

        /// <summary>
        /// 选中变更计划
        /// </summary>
        public bool IsOperationHistory
        {
            get { return this._isOperationHistory; }
            private set
            {
                if (this._isOperationHistory != value)
                {
                    this._isOperationHistory = value;
                    if (value == true)
                    {
                        this.IsOperationHistoryVisibility = Visibility.Visible;
                    }
                    else
                    {
                        this.IsOperationHistoryVisibility = Visibility.Collapsed;
                    }
                    this.RaisePropertyChanged(() => this.IsOperationHistory);
                }
            }
        }

        #endregion

        #region 子窗体选择飞机商业数据历史

        private bool _isAircraftBusiness;

        /// <summary>
        /// 选中变更计划
        /// </summary>
        public bool IsAircraftBusiness
        {
            get { return this._isAircraftBusiness; }
            private set
            {
                if (this._isAircraftBusiness != value)
                {
                    this._isAircraftBusiness = value;
                    if (value == true)
                    {
                        this.IsAircraftBusinessVisibility = Visibility.Visible;
                    }
                    else
                    {
                        this.IsAircraftBusinessVisibility = Visibility.Collapsed;
                    }
                    this.RaisePropertyChanged(() => this.IsAircraftBusiness);
                }
            }
        }

        #endregion

        #region 子窗体选择运营飞机是否显示

        private Visibility _isAircraftVisibility = Visibility.Visible;

        /// <summary>
        /// 选中变更计划
        /// </summary>
        public Visibility IsAircraftVisibility
        {
            get { return this._isAircraftVisibility; }
            private set
            {
                if (this._isAircraftVisibility != value)
                {
                    this._isAircraftVisibility = value;
                    this.RaisePropertyChanged(() => this.IsAircraftVisibility);
                }
            }
        }

        #endregion

        #region 子窗体选择飞机运营历史是否显示

        private Visibility _isOperationHistoryVisibility = Visibility.Collapsed;

        /// <summary>
        /// 选中变更计划
        /// </summary>
        public Visibility IsOperationHistoryVisibility
        {
            get { return this._isOperationHistoryVisibility; }
            private set
            {
                if (this._isOperationHistoryVisibility != value)
                {
                    this._isOperationHistoryVisibility = value;
                    this.RaisePropertyChanged(() => this.IsOperationHistoryVisibility);
                }
            }
        }

        #endregion

        #region 子窗体选择飞机商业数据历史是否显示

        private Visibility _isAircraftBusinessVisibility = Visibility.Collapsed;

        /// <summary>
        /// 选中变更计划
        /// </summary>
        public Visibility IsAircraftBusinessVisibility
        {
            get { return this._isAircraftBusinessVisibility; }
            private set
            {
                if (this._isAircraftBusinessVisibility != value)
                {
                    this._isAircraftBusinessVisibility = value;
                    this.RaisePropertyChanged(() => this.IsAircraftBusinessVisibility);
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
            var aircraft = sender as AircraftDTO;
            this.EditDialog.Close();
            var planHistories = CurPlanHistories;
            if (planHistories != null && aircraft != null)
                this.SelPlanHistory = CurPlanHistories.FirstOrDefault(p => p.AircraftId == aircraft.AircraftId);
        }

        #endregion

        #endregion
    }
}