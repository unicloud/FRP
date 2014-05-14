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
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.Requests
{
    [Export(typeof(RequestVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class RequestVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;
        private FilterDescriptor _annualDescriptor;
        private FilterDescriptor _planDescriptor;
        private FilterDescriptor _planHistoryDescriptor;

        [ImportingConstructor]
        public RequestVM(IRegionManager regionManager, IFleetPlanService service)
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
            Requests = _service.CreateCollection(_context.Requests, o => o.ApprovalHistories, o => o.RelatedDocs);
            var cfd = new CompositeFilterDescriptor { LogicalOperator = FilterCompositionLogicalOperator.And };
            var requestDescriptor = new FilterDescriptor("Note", FilterOperator.IsNotEqualTo, "指标飞机申请（系统添加）");
            cfd.FilterDescriptors.Add(requestDescriptor);
            var statusDateDescriptor = new FilterDescriptor("Status", FilterOperator.IsLessThan, (int)RequestStatus.已审批);
            cfd.FilterDescriptors.Add(statusDateDescriptor);
            Requests.FilterDescriptors.Add(cfd);
            Requests.LoadedData += (o, e) =>
                                   {
                                       if (SelRequest == null)
                                           SelRequest = Requests.FirstOrDefault();
                                   };
            _service.RegisterCollectionView(Requests);

            CurAnnuals = new QueryableDataServiceCollectionView<AnnualDTO>(_context, _context.Annuals);
            _annualDescriptor = new FilterDescriptor("IsOpen", FilterOperator.IsEqualTo, true);
            CurAnnuals.FilterDescriptors.Add(_annualDescriptor);
            CurAnnuals.LoadedData += (sender, e) =>
            {
                if (CurAnnuals.Count != 0)
                {
                    _planDescriptor.Value = CurAnnuals.First().Year;
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
                PlanDTO curPlan = Plans.OrderBy(p => p.VersionNumber).LastOrDefault();
                if (curPlan != null)
                {
                    _planHistoryDescriptor.Value = curPlan.Id;
                    if (!CurPlanHistories.AutoLoad)
                        CurPlanHistories.AutoLoad = true;
                    else
                        CurPlanHistories.Load(true);
                }
                RefreshCommandState();
            };

            CurPlanHistories = new QueryableDataServiceCollectionView<PlanHistoryDTO>(_context, _context.PlanHistories);
            _planHistoryDescriptor = new FilterDescriptor("PlanId", FilterOperator.IsEqualTo, Guid.Empty);
            var group = new GroupDescriptor { Member = "CanRequest", SortDirection = ListSortDirection.Descending };
            CurPlanHistories.GroupDescriptors.Add(group);
            CurPlanHistories.FilterDescriptors.Add(_planHistoryDescriptor);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveDocCommand = new DelegateCommand<object>(OnRemoveDoc, CanRemoveDoc);
            CommitCommand = new DelegateCommand<object>(OnCommit, CanCommit);
            CheckCommand = new DelegateCommand<object>(OnCheck, CanCheck);
            SendCommand = new DelegateCommand<object>(OnSend, CanSend);
            EditCommand = new DelegateCommand<object>(OnEdit, CanEdit);
        }

        #endregion

        #region 数据

        #region 公共属性

        public Dictionary<int, CanRequest> CanRequests
        {
            get
            {
                return Enum.GetValues(typeof(CanRequest))
                    .Cast<object>()
                    .ToDictionary(value => (int)value, value => (CanRequest)value);
            }
        }

        #region 当前计划年度

        /// <summary>
        ///     当前计划年度
        /// </summary>
        public QueryableDataServiceCollectionView<AnnualDTO> CurAnnuals { get; set; }

        #endregion

        #region 当前年度运力增减计划集合

        /// <summary>
        ///     当前年度运力增减计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> Plans { get; set; }

        #endregion

        #region 当前运力增减计划

        private ObservableCollection<PlanDTO> _curPlan = new ObservableCollection<PlanDTO>();
        private PlanHistoryDTO _selPlanHistory;

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
        ///     当前年度运力增减计划明细集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanHistoryDTO> CurPlanHistories { get; set; }


        /// <summary>
        ///     选择的计划明细
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

            CurAnnuals.Load(true);
        }

        #region 业务

        #region 所有申请集合

        private ApprovalHistoryDTO _selApprovalHistory;
        private RequestDTO _selRequest;

        /// <summary>
        ///     所有未审批完成申请集合
        /// </summary>
        public QueryableDataServiceCollectionView<RequestDTO> Requests { get; set; }

        /// <summary>
        ///     选择的申请
        /// </summary>
        public RequestDTO SelRequest
        {
            get { return _selRequest; }
            private set
            {
                if (_selRequest != value)
                {
                    _selRequest = value;
                    RaisePropertyChanged(() => SelRequest);
                }
            }
        }


        /// <summary>
        ///     选择的申请明细
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
            RemoveDocCommand.RaiseCanExecuteChanged();
            CommitCommand.RaiseCanExecuteChanged();
            CheckCommand.RaiseCanExecuteChanged();
        }

        #endregion

        protected override bool CanAddAttach(object obj)
        {
            return _selRequest != null;
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
            if (sender is Guid)
            {
                SelRequest.CaacDocumentId = doc.DocumentId;
                SelRequest.CaacDocumentName = doc.Name;
            }
            else
            {
                var relatedDoc = new RelatedDocDTO
                {
                    Id = RandomHelper.Next(),
                    DocumentId = doc.DocumentId,
                    DocumentName = doc.Name,
                    SourceId = SelRequest.Id
                };
                SelRequest.RelatedDocs.Add(relatedDoc);
            }
        }

        #endregion

        #region 创建新申请

        /// <summary>
        ///     创建新申请
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var newRequest = new RequestDTO
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                SubmitDate = DateTime.Now,
                AirlinesId = Guid.Parse("1978ADFC-A2FD-40CC-9A26-6DEDB55C335F"),
                Status = (int)RequestStatus.草稿,
            };
            Requests.AddNew(newRequest);
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
            //this.service.CreateNewRequestDetail(this.SelRequest, planHistory);
            //this._needReFreshViewApprovalHistory = true;
            //RaiseViewApprovalHistory();
            //this._needReFreshViewPlanHistory = true;
            //RaiseViewPlanHistory();
        }

        #endregion

        #region 移除指标飞机申请明细

        internal void RemoveRequestDetail(ApprovalHistoryDTO requestDetail)
        {
            //this.service.RemoveRequestDetail(requestDetail);
            //this._needReFreshViewApprovalHistory = true;
            //RaiseViewApprovalHistory();
            //this._needReFreshViewPlanHistory = true;
            //RaiseViewPlanHistory();
        }

        #endregion

        #region 删除关联文档

        /// <summary>
        ///     删除关联文档
        /// </summary>
        public DelegateCommand<object> RemoveDocCommand { get; private set; }

        private void OnRemoveDoc(object obj)
        {
            var doc = obj as RelatedDocDTO;
            if (doc != null)
            {
                MessageConfirm("确认移除", "是否移除关联文档：" + doc.DocumentName + "？", (o, e) =>
                {
                    if (e.DialogResult == true)
                    {
                        _selRequest.RelatedDocs.Remove(doc);
                    }
                });
            }
        }

        private bool CanRemoveDoc(object obj)
        {
            return _selRequest != null;
        }

        #endregion

        #region 提交审核

        /// <summary>
        ///     提交审核
        /// </summary>
        public DelegateCommand<object> CommitCommand { get; private set; }

        private void OnCommit(object obj)
        {
            SelRequest.Status = (int)RequestStatus.待审核;
            RefreshCommandState();
        }

        private bool CanCommit(object obj)
        {
            // 选中申请为空时，按钮不可用
            if (SelRequest == null)
            {
                return false;
            }
            // 选中申请的标题、文号、文档为空时，按钮不可用
            if (string.IsNullOrWhiteSpace(SelRequest.Title) ||
                string.IsNullOrWhiteSpace(SelRequest.CaacDocNumber) ||
                SelRequest.CaacDocumentId == Guid.Empty)
            {
                return false;
            }
            // 选中申请的状态处于草稿，且申请明细不为空时，按钮可用
            return SelRequest.Status == (int)RequestStatus.草稿 && SelRequest.ApprovalHistories.Any();
        }

        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> CheckCommand { get; private set; }

        private void OnCheck(object obj)
        {
            SelRequest.Status = (int)RequestStatus.已审核;
            RefreshCommandState();
        }

        private bool CanCheck(object obj)
        {
            // 选中申请为空时，按钮不可用
            if (SelRequest == null)
            {
                return false;
            }
            // 选中申请的标题、文号、文档为空时，按钮不可用
            if (string.IsNullOrWhiteSpace(SelRequest.Title) ||
                string.IsNullOrWhiteSpace(SelRequest.CaacDocNumber) ||
                SelRequest.CaacDocumentId == Guid.Empty)
            {
                return false;
            }
            // 选中申请的状态处于审核，且申请明细不为空时，按钮可用
            return SelRequest.Status == (int)RequestStatus.待审核 && SelRequest.ApprovalHistories.Any();
        }

        #endregion

        #region 发送

        /// <summary>
        ///     发送
        /// </summary>
        public DelegateCommand<object> SendCommand { get; private set; }

        private void OnSend(object obj)
        {
            string content = "是否向【民航局】报送" + SelRequest.CaacDocNumber + "，" + SelRequest.Title + "？";
            MessageConfirm("确认报送申请", content, (o, e) =>
            {
                if (e.DialogResult == true)
                {
                    // 审核、已提交状态下可以发送。如果已处于提交状态，需要重新发送的，不必改变状态。
                    if (SelRequest != null && SelRequest.Status != (int)RequestStatus.已提交)
                    {
                        SelRequest.Status = (int)RequestStatus.已提交;
                    }
                    SelRequest.SubmitDate = DateTime.Now;
                    //this.service.SubmitChanges(sc =>
                    //{
                    //    if (sc.Error == null)
                    //    {
                    //        // 发送不成功的，也认为是已经做了发送操作，不回滚状态。始终可以重新发送。
                    //        this.service.TransferPlanAndRequest(this.CurrentPlan.PlanID, this.SelRequest.RequestID, tp => { }, null);
                    //        RefreshButtonState();
                    //    }
                    //}, null);
                }
            });
            RefreshCommandState();
        }

        private bool CanSend(object obj)
        {
            // 选中申请为空时，按钮不可用
            if (SelRequest == null) return false;
            // 选中申请的状态处于已审核或已提交时，按钮可用
            return SelRequest.Status == (int)RequestStatus.已审核 ||
                   SelRequest.Status == (int)RequestStatus.已提交;
        }

        #endregion

        #region 修改申请

        /// <summary>
        ///     修改申请
        /// </summary>
        public DelegateCommand<object> EditCommand { get; private set; }

        private void OnEdit(object obj)
        {
            const string content = "确认后申请状态将改为草稿并允许编辑，是否要对该申请进行修改？";
            MessageConfirm("确认修改申请", content, (o, e) =>
            {
                if (e.DialogResult == true)
                {
                    SelRequest.Status = (int)RequestStatus.草稿;
                    //this.service.SubmitChanges(sc => { }, null);
                    RefreshCommandState();
                }
            });
        }

        private bool CanEdit(object obj)
        {
            // 选中申请为空时，按钮不可用
            if (SelRequest == null)
            {
                return false;
            }
            // 选中申请的状态不是草稿时，按钮可用
            return SelRequest.Status != (int)RequestStatus.草稿;
        }

        #endregion

        #endregion
    }
}