#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/4 14:18:04
// 文件名：ManageIndexAircraftVM
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
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.Requests
{
    [Export(typeof(ManageIndexAircraftVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageIndexAircraftVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;
        private FilterDescriptor _planDescriptor;
        private FilterDescriptor _planHistoryDescriptor;
        private FilterDescriptor _requestDescriptor;
        private List<ApprovalHistoryCache> _approvalHistoryCaches;

        [ImportingConstructor]
        public ManageIndexAircraftVM(IRegionManager regionManager, IFleetPlanService service)
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
            ApprovalDocs = _service.CreateCollection(_context.ApprovalDocs);
            var approvalDocDescriptor = new FilterDescriptor("Note", FilterOperator.IsEqualTo, "指标飞机批文");
            ApprovalDocs.FilterDescriptors.Add(approvalDocDescriptor);
            ApprovalDocs.LoadedData += (o, e) =>
                                       {
                                           if (SelApprovalDoc == null)
                                               SelApprovalDoc = ApprovalDocs.FirstOrDefault();
                                       };
            _service.RegisterCollectionView(ApprovalDocs);

            Requests = _service.CreateCollection(_context.Requests, o => o.ApprovalHistories);
            _requestDescriptor = new FilterDescriptor("Note", FilterOperator.IsEqualTo, "指标飞机申请（系统添加）");
            Requests.FilterDescriptors.Add(_requestDescriptor);
            Requests.LoadedData += (sender, e) => RaisePropertyChanged(()=>SelApprovalDoc);
            _service.RegisterCollectionView(Requests);

            Annuals = new QueryableDataServiceCollectionView<AnnualDTO>(_context, _context.Annuals);
            Annuals.LoadedData += (sender, e) =>
            {
                if (Annuals.Count != 0 && Annuals.First(p => p.IsOpen) != null)
                {
                    _planDescriptor.Value = Annuals.First(p => p.IsOpen).Year;
                    if (!Plans.AutoLoad)
                        Plans.AutoLoad = true;
                    else
                        Plans.Load(true);
                    RefreshCommandState();
                }
            };

            Plans = new QueryableDataServiceCollectionView<PlanDTO>(_context, _context.Plans);
            _planDescriptor = new FilterDescriptor("Year", FilterOperator.IsEqualTo, -1);
            var sort = new SortDescriptor { Member = "VersionNumber", SortDirection = ListSortDirection.Ascending };
            Plans.SortDescriptors.Add(sort);
            Plans.FilterDescriptors.Add(_planDescriptor);
            Plans.LoadedData += (sender, e) =>
            {
                var curPlan = Plans.OrderBy(p => p.VersionNumber).LastOrDefault();
                if (curPlan != null)
                {
                    _planHistoryDescriptor.Value = curPlan.Id;
                    if (!CurPlanHistories.AutoLoad)
                        CurPlanHistories.AutoLoad = true;
                    else
                        CurPlanHistories.Load(true);
                }
                RefreshCommandState();
                _approvalHistoryCaches = new List<ApprovalHistoryCache>();
            };

            CurPlanHistories = _service.CreateCollection(_context.PlanHistories);
            _planHistoryDescriptor = new FilterDescriptor("PlanId", FilterOperator.IsEqualTo, Guid.Empty);
            var group = new GroupDescriptor { Member = "CanRequest", SortDirection = ListSortDirection.Descending };
            CurPlanHistories.GroupDescriptors.Add(group);
            CurPlanHistories.FilterDescriptors.Add(_planHistoryDescriptor);
            _service.RegisterCollectionView(CurPlanHistories);

            PlanAircrafts = _service.CreateCollection(_context.PlanAircrafts);
            _service.RegisterCollectionView(PlanAircrafts);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            CommitCommand = new DelegateCommand<object>(OnCommit, CanCommit);
            CheckCommand = new DelegateCommand<object>(OnCheck, CanCheck);
            SendCommand = new DelegateCommand<object>(OnSend, CanSend);
        }

        #endregion

        #region 数据

        #region 公共属性

        public Dictionary<int, CanRequest> CanRequests
        {
            get
            {
                return Enum.GetValues(typeof(CanRequest)).Cast<object>().ToDictionary(value => (int)value, value => (CanRequest)value);
            }
        }

        #region 所有计划年度

        /// <summary>
        ///     所有计划年度
        /// </summary>
        public QueryableDataServiceCollectionView<AnnualDTO> Annuals { get; set; }

        #endregion

        #region 当前年度运力增减计划集合

        /// <summary>
        /// 当前年度运力增减计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> Plans { get; set; }

        #endregion

        #region 当前运力增减计划

        private ObservableCollection<PlanDTO> _curPlan = new ObservableCollection<PlanDTO>();

        /// <summary>
        ///     当前运力增减计划
        /// </summary>
        public ObservableCollection<PlanDTO> CurPlan
        {
            get { return _curPlan; }
            set
            {
                if (_curPlan != value)
                {
                    _curPlan = value;
                    RaisePropertyChanged(() => CurPlan);
                }
            }
        }

        /// <summary>
        /// 当前年度运力增减计划明细集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanHistoryDTO> CurPlanHistories { get; set; }


        private PlanHistoryDTO _selPlanHistory;

        /// <summary>
        /// 选择的计划明细
        /// </summary>
        public PlanHistoryDTO SelPlanHistory
        {
            get { return _selPlanHistory; }
            set
            {
                if (_selPlanHistory != value)
                {
                    _selPlanHistory = value;
                    RaisePropertyChanged(() => SelPlanHistory);
                }
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
            if (!Requests.AutoLoad)
                Requests.AutoLoad = true;
            else
                Requests.Load(true);

            if (!ApprovalDocs.AutoLoad)
                ApprovalDocs.AutoLoad = true;
            else
                ApprovalDocs.Load(true);

            if (!PlanAircrafts.AutoLoad)
                PlanAircrafts.AutoLoad = true;
            else
                PlanAircrafts.Load(true);

            Annuals.Load(true);
        }

        #region 业务


        #region 所有计划飞机集合

        /// <summary>
        ///     所有计划飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanAircraftDTO> PlanAircrafts { get; set; }

        #endregion

        #region 所有发改委指标批文集合

        /// <summary>
        ///     所有发改委指标批文集合
        /// </summary>
        public QueryableDataServiceCollectionView<ApprovalDocDTO> ApprovalDocs { get; set; }


        private ApprovalDocDTO _selApprovalDoc;

        /// <summary>
        /// 选择的发改委指标批文
        /// </summary>
        public ApprovalDocDTO SelApprovalDoc
        {
            get { return _selApprovalDoc; }
            private set
            {
                if (_selApprovalDoc != value)
                {
                    _selApprovalDoc = value;
                    if (value != null)
                    {
                        if (Requests.SourceCollection.Cast<RequestDTO>().Any())
                        {
                            _curRequest = Requests.SourceCollection.Cast<RequestDTO>().FirstOrDefault(p => value.Id == p.ApprovalDocId);
                        }
                    }
                    RaisePropertyChanged(() => SelApprovalDoc);
                    RaisePropertyChanged(()=>CurRequest);
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 所有指标飞机对应的系统申请集合

        /// <summary>
        ///     所有指标飞机对应的系统申请集合
        /// </summary>
        public QueryableDataServiceCollectionView<RequestDTO> Requests { get; set; }


        private RequestDTO _curRequest;

        /// <summary>
        /// 选择的批文对应的系统申请
        /// </summary>
        public RequestDTO CurRequest
        {
            get { return _curRequest; }
            set
            {
                if (_curRequest != value)
                {
                    _curRequest = value;
                    RaisePropertyChanged(() => CurRequest);
                }
            }
        }


        private ApprovalHistoryDTO _selApprovalHistory;

        /// <summary>
        /// 选择的申请明细
        /// </summary>
        public ApprovalHistoryDTO SelApprovalHistory
        {
            get { return _selApprovalHistory; }
            set
            {
                if (_selApprovalHistory != value)
                {
                    _selApprovalHistory = value;
                    RaisePropertyChanged(() => SelApprovalHistory);
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
            CommitCommand.RaiseCanExecuteChanged();
            CheckCommand.RaiseCanExecuteChanged();
            SendCommand.RaiseCanExecuteChanged();
        }

        #endregion

        protected override bool CanAddAttach(object obj)
        {
            return _selApprovalDoc != null;
        }

        #region 添加附件成功后执行的操作

        /// <summary>
        ///     子窗口关闭后执行的操作
        /// </summary>
        /// <param name="doc">添加的附件</param>
        /// <param name="sender">添加附件命令的参数</param>
        protected override void WindowClosed(DocumentDTO doc, object sender)
        {
            base.WindowClosed(doc, sender);

            SelApprovalDoc.NdrcDocumentId = doc.DocumentId;
            SelApprovalDoc.NdrcDocumentName = doc.Name;
        }

        #endregion

        #region 创建指标批文和申请

        /// <summary>
        ///     创建指标批文和申请
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            SelApprovalDoc = new ApprovalDocDTO
            {
                Id = Guid.NewGuid(),
                Note = "指标飞机批文",
                Status = (int)OperationStatus.草稿,
            };
            ApprovalDocs.AddNew(SelApprovalDoc);

            var newRequest = new RequestDTO
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                SubmitDate = DateTime.Now,
                Title = "指标飞机申请（系统添加）",
                Note = "指标飞机申请（系统添加）",
                AirlinesId = Guid.Parse("1978ADFC-A2FD-40CC-9A26-6DEDB55C335F"),
                Status = (int)RequestStatus.草稿,
                IsFinished = true,
                ApprovalDocId = SelApprovalDoc.Id,
            };
            Requests.AddNew(newRequest);
            CurRequest = newRequest;
            RefreshCommandState();
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 创建指标飞机申请明细
        internal void AddNewRequestDetail(PlanHistoryDTO planHistory)
        {
            var requestDetail = new ApprovalHistoryDTO
            {
                Id = Guid.NewGuid(),
                RequestId = CurRequest.Id,
                ImportCategoryId = planHistory.TargetCategoryId,
                AirlinesId = planHistory.AirlinesId,
                RequestDeliverAnnualId = planHistory.PerformAnnualId,
                RequestDeliverMonth = planHistory.PerformMonth,
                SeatingCapacity = planHistory.SeatingCapacity,
                CarryingCapacity = planHistory.CarryingCapacity,
                AircraftType = planHistory.AircraftTypeName,
                AircraftRegional = planHistory.Regional,
                AirlineName = planHistory.AirlinesName,
                ImportCategoryName = planHistory.ActionType + planHistory.ActionName,
            };
            var annual = Annuals.SourceCollection.Cast<AnnualDTO>().FirstOrDefault(p => p.Id == requestDetail.RequestDeliverAnnualId);
            if (annual != null) requestDetail.RequestDeliverAnnualName = annual.Year;
            if (planHistory.PlanAircraftId != null)
                requestDetail.PlanAircraftId = Guid.Parse(planHistory.PlanAircraftId.ToString());
            // 把申请明细赋给关联的计划明细
            if (planHistory.CanRequest == (int)CanRequest.可再次申请 && planHistory.ApprovalHistoryId != null && _approvalHistoryCaches != null)
            {
                _approvalHistoryCaches.Add(new ApprovalHistoryCache
                {
                    PlanHistoryId = planHistory.Id,
                    ApprovalHistoryId = Guid.Parse(planHistory.ApprovalHistoryId.ToString()),
                });//用于撤销操作
            }
            planHistory.ApprovalHistoryId = requestDetail.Id;

            // 计划飞机管理状态修改为申请:
            var planAircraft =
                PlanAircrafts.SourceCollection.Cast<PlanAircraftDTO>()
                    .FirstOrDefault(p => p.Id == planHistory.PlanAircraftId);
            if (planAircraft != null) planAircraft.Status = (int)ManageStatus.申请;

            planHistory.CanRequest = (int)CanRequest.已有发改委指标;

            CurRequest.ApprovalHistories.Add(requestDetail);
            RefreshCommandState();
        }
        #endregion

        #region 移除指标飞机申请明细
        internal void RemoveRequestDetail(ApprovalHistoryDTO requestDetail)
        {
            //先获取与这个申请明细相关的计划明细
            var planHistory = CurPlanHistories.SourceCollection.Cast<PlanHistoryDTO>().FirstOrDefault(p => p.ApprovalHistoryId == requestDetail.Id);
            if (planHistory != null)
            {
                var planAircraft = PlanAircrafts.SourceCollection.Cast<PlanAircraftDTO>()
                    .FirstOrDefault(p => p.Id == planHistory.PlanAircraftId);
                var approvalHistoryCache = _approvalHistoryCaches.FirstOrDefault(p => p.PlanHistoryId == planHistory.Id);
                //如果原计划明细状态为“可再次申请”，则删除申请明细前需要将ApprovalHistoryId置为原来的，计划飞机状态还是“申请”状态
                if (approvalHistoryCache != null && planAircraft != null)
                {
                    planHistory.ApprovalHistoryId = approvalHistoryCache.ApprovalHistoryId;
                    planHistory.CanRequest = (int)CanRequest.可再次申请;
                    planAircraft.Status = (int)ManageStatus.申请;
                }
                //如果远计划明细状态为“可申请”，则删除申请明细前将ApprovalHistoryId置为null，并将计划飞机状态置为“计划”状态
                else if (approvalHistoryCache == null && planAircraft != null)
                {
                    planHistory.ApprovalHistoryId = null;
                    planHistory.CanDeliver = (int)CanRequest.可申请;
                    planAircraft.Status = (int)ManageStatus.计划;
                }
            }
            CurRequest.ApprovalHistories.Remove(requestDetail);
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
            if (SelApprovalDoc == null)
            {
                MessageAlert("批文不能为空");
                return;
            }
            SelApprovalDoc.Status = (int)OperationStatus.待审核;
            CurRequest.Status = (int)RequestStatus.待审核;
            RefreshCommandState();
        }

        private bool CanCommit(object obj)
        {
            return SelApprovalDoc != null && CurRequest != null && SelApprovalDoc.Status < (int)RequestStatus.待审核;
        }

        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> CheckCommand { get; private set; }

        private void OnCheck(object obj)
        {
            if (SelApprovalDoc == null)
            {
                MessageAlert("批文不能为空");
                return;
            }
            SelApprovalDoc.Status = (int)OperationStatus.已审核;
            CurRequest.Status = (int)RequestStatus.已审核;
            RefreshCommandState();
        }

        private bool CanCheck(object obj)
        {
            return SelApprovalDoc != null && CurRequest != null && SelApprovalDoc.Status < (int)OperationStatus.已审核
                   && SelApprovalDoc.Status > (int)OperationStatus.草稿;
        }

        #endregion

        #region 发送

        /// <summary>
        ///     发送
        /// </summary>
        public DelegateCommand<object> SendCommand { get; private set; }

        private void OnSend(object obj)
        {
            string content = "是否向【民航局】提交" + SelApprovalDoc.NdrcApprovalNumber + "，" + SelApprovalDoc.NdrcDocumentName + "？";
            MessageConfirm("确认提交", content, (o, e) =>
            {
                if (e.DialogResult == true)
                {
                    // 审核、已提交状态下可以发送。如果已处于提交状态，需要重新发送的，不必改变状态。
                    if (SelApprovalDoc != null && CurRequest != null && SelApprovalDoc.Status != (int)RequestStatus.已提交)
                    {
                        SelApprovalDoc.Status = (int)OperationStatus.已提交;
                        CurRequest.Status = (int)RequestStatus.已提交;
                        CurRequest.SubmitDate = DateTime.Now;
                    }
                    this._service.SubmitChanges(sc =>
                    {
                        if (sc.Error == null)
                        {
                            PlanDTO curPlan = Plans.First();
                            if (curPlan != null)
                            {
                                // 发送不成功的，也认为是已经做了发送操作，不回滚状态。始终可以重新发送。
                                this._service.TransferApprovalRequest(curPlan.AirlinesId, curPlan.Id, CurRequest.Id, SelApprovalDoc.Id, _context);
                                RefreshCommandState();
                            }
                        }
                    }, null);
                }
            });
            RefreshCommandState();
        }

        private bool CanSend(object obj)
        {
            // 选中批文为空时，按钮不可用
            if (SelApprovalDoc == null) return false;
            // 选中批文对应的申请为空时，按钮不可用
            if (CurRequest == null) return false;
            // 有没保存的修改时，按钮不可用
            if (_service.HasChanges) return false;
            // 选中申请的状态处于已审核或已提交时，按钮可用
            return SelApprovalDoc.Status == (int)OperationStatus.已审核 ||
                   SelApprovalDoc.Status == (int)OperationStatus.已提交;
        }

        #endregion
        #endregion
    }
}
