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
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
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
        private FilterDescriptor _annualDescriptor;
        private FilterDescriptor _planDescriptor;

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
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (CurAnnual.Count != 0)
                {
                    _curAnnual = CurAnnual.FirstOrDefault();
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
            };
            _service.RegisterCollectionView(Plans);//注册查询集合

            Aircrafts = _service.CreateCollection(_context.Aircrafts);
            _service.RegisterCollectionView(Aircrafts);//注册查询集合

            ActionCategories = new QueryableDataServiceCollectionView<ActionCategoryDTO>(_context, _context.ActionCategories);

            AircraftCategories = new QueryableDataServiceCollectionView<AircraftCategoryDTO>(_context, _context.AircraftCategories);

            AircraftTypes = new QueryableDataServiceCollectionView<AircraftTypeDTO>(_context, _context.AircraftTypes);

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
            CancelCommand = new DelegateCommand<object>(OnCancel, CanCancel);
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

        #region 所有运力增减计划集合

        /// <summary>
        /// 所有运力增减计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> Plans { get; set; }

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
            CurAnnual.Load(true);
            Aircrafts.Load(true);
            AircraftTypes.Load(true);
            ActionCategories.Load(true);
            AircraftCategories.Load(true);
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

                    if (Aircrafts.Any(pa => pa.AircraftId == value.AircraftId))
                        SelAircraft = Aircrafts.FirstOrDefault(p => p.AircraftId == value.AircraftId);
                    else SelAircraft = null;
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
                    OperationHistories.Clear();
                    foreach (var op in value.OperationHistories)
                    {
                        OperationHistories.Add(op);
                    }
                    AircraftBusinesses.Clear();
                    foreach (var ab in value.AircraftBusinesses)
                    {
                        AircraftBusinesses.Add(ab);
                    }
                    //选择相关的计划明细
                    if (PlanHistories.Any(pa => pa.AircraftId == value.AircraftId))
                        SelPlanHistory = PlanHistories.FirstOrDefault(p => p.AircraftId == value.AircraftId);
                    else SelPlanHistory = null;

                    this.SelOperationHistory = this.OperationHistories.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                    //如果为变更计划，则这个时候取到的SelOperationHistory为空，重新取集合中的最后一条来展示在子窗体中
                    if (SelOperationHistory == null)
                        SelOperationHistory = this.OperationHistories.LastOrDefault();
                    this.SelAircraftBusiness = this.AircraftBusinesses.LastOrDefault(p => p.Status < (int)OperationStatus.已提交);
                    //如果为退出计划，则这个时候取到的SelAircraftBusiness为空，重新取集合中的最后一条来展示在子窗体中
                    if (SelAircraftBusiness == null)
                        SelAircraftBusiness = this.AircraftBusinesses.LastOrDefault();

                    RaisePropertyChanged(() => SelAircraft);
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

        }

        private bool CanComplete(object obj)
        {
            return true;
        }

        #endregion

        #region 提交审核

        /// <summary>
        ///     提交审核
        /// </summary>
        public DelegateCommand<object> CommitCommand { get; private set; }

        private void OnCommit(object obj)
        {

        }

        private bool CanCommit(object obj)
        {
            return true;
        }

        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> CheckCommand { get; private set; }

        private void OnCheck(object obj)
        {
        }

        private bool CanCheck(object obj)
        {
            return true;
        }
        #endregion

        #region 发送

        /// <summary>
        ///     发送
        /// </summary>
        public DelegateCommand<object> SendCommand { get; private set; }

        private void OnSend(object obj)
        {

        }

        private bool CanSend(object obj)
        {
            return true;
        }

        #endregion

        #region 修改完成

        /// <summary>
        ///     修改完成
        /// </summary>
        public DelegateCommand<object> RepealCommand { get; private set; }

        private void OnRepeal(object obj)
        {
        }

        private bool CanRepeal(object obj)
        {
            return true;
        }

        #endregion
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

        }

        #endregion
    }
}