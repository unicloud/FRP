#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace UniCloud.Presentation.FleetPlan.Approvals
{
    [Export(typeof (ApprovalVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ApprovalVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;

        [ImportingConstructor]
        public ApprovalVM(IRegionManager regionManager, IFleetPlanService service)
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
            ApprovalDocs.LoadedData += (s, e) =>
            {
                _viewApprovalDocs = new ObservableCollection<ApprovalDocDTO>();
                foreach (ApprovalDocDTO appDoc in ApprovalDocs.SourceCollection.Cast<ApprovalDocDTO>())
                {
                    List<RequestDTO> requests =
                        Requests.SourceCollection.Cast<RequestDTO>().Where(p => p.ApprovalDocId == appDoc.Id).ToList();
                    if (appDoc.Status < (int) OperationStatus.已提交 || requests.Any() || requests.Any(r => !r.IsFinished))
                    {
                        ViewApprovalDocs.Add(appDoc);
                    }
                    RaisePropertyChanged(() => ViewApprovalDocs);
                }
            };
            _service.RegisterCollectionView(ApprovalDocs);

            Requests = _service.CreateCollection(_context.Requests, o => o.ApprovalHistories, o => o.RelatedDocs);
            Requests.LoadedData += (s, e) =>
            {
                if (!ApprovalDocs.AutoLoad)
                    ApprovalDocs.AutoLoad = true;
                else
                    ApprovalDocs.Load(true);

                _enRouteRequests = new ObservableCollection<RequestDTO>();
                foreach (RequestDTO req in Requests.SourceCollection.Cast<RequestDTO>())
                {
                    if (req.Note != "指标飞机申请（系统添加）" && req.Status <= (int) RequestStatus.已审批)
                    {
                        EnRouteRequests.Add(req);
                    }
                    RaisePropertyChanged(() => EnRouteRequests);
                }
            };
            _service.RegisterCollectionView(Requests);
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
            EditCommand = new DelegateCommand<object>(OnEdit, CanEdit);
        }

        #endregion

        #region 数据

        #region 公共属性

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
        }

        #region 业务

        #region 所有批文集合

        private ApprovalDocDTO _selApprovalDoc;
        private ObservableCollection<ApprovalDocDTO> _viewApprovalDocs = new ObservableCollection<ApprovalDocDTO>();

        /// <summary>
        ///     所有批文集合
        /// </summary>
        public QueryableDataServiceCollectionView<ApprovalDocDTO> ApprovalDocs { get; set; }

        /// <summary>
        ///     未使用的批文集合（批文中的飞机未完成计划，或未提交的批文）
        /// </summary>
        public ObservableCollection<ApprovalDocDTO> ViewApprovalDocs
        {
            get { return _viewApprovalDocs; }
            private set
            {
                if (_viewApprovalDocs != value)
                {
                    _viewApprovalDocs = value;
                    RaisePropertyChanged(() => ViewApprovalDocs);
                }
            }
        }

        /// <summary>
        ///     选择的批文
        /// </summary>
        public ApprovalDocDTO SelApprovalDoc
        {
            get { return _selApprovalDoc; }
            private set
            {
                if (_selApprovalDoc != value)
                {
                    _selApprovalDoc = value;
                    ApprovalRequests=new ObservableCollection<RequestDTO>();
                    if (value != null)
                    {
                        foreach (RequestDTO req in Requests.SourceCollection.Cast<RequestDTO>())
                        {
                            if (req.ApprovalDocId == value.Id)
                                ApprovalRequests.Add(req);
                        }
                        if (ApprovalRequests.Count != 0) SelApprovalRequest = ApprovalRequests.First();
                    }
                    RaisePropertyChanged(() => SelApprovalDoc);
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 所有申请集合

        private ObservableCollection<RequestDTO> _approvalRequests = new ObservableCollection<RequestDTO>();
        private ObservableCollection<RequestDTO> _enRouteRequests = new ObservableCollection<RequestDTO>();
        private ApprovalHistoryDTO _selApprovalHistory;

        private RequestDTO _selApprovalRequest;
        private RequestDTO _selEnRouteRequest;

        /// <summary>
        ///     所有未审批完成申请集合
        /// </summary>
        public QueryableDataServiceCollectionView<RequestDTO> Requests { get; set; }

        /// <summary>
        ///     批文对应的申请集合
        /// </summary>
        public ObservableCollection<RequestDTO> ApprovalRequests
        {
            get { return _approvalRequests; }
            private set
            {
                if (_approvalRequests != value)
                {
                    _approvalRequests = value;
                    RaisePropertyChanged(() => ApprovalRequests);
                    RefreshCommandState();
                }
            }
        }

        /// <summary>
        ///     在途申请集合
        /// </summary>
        public ObservableCollection<RequestDTO> EnRouteRequests
        {
            get { return _enRouteRequests; }
            private set
            {
                if (_enRouteRequests != value)
                {
                    _enRouteRequests = value;
                    RaisePropertyChanged(() => EnRouteRequests);
                }
            }
        }

        /// <summary>
        ///     选择的批文申请
        /// </summary>
        public RequestDTO SelApprovalRequest
        {
            get { return _selApprovalRequest; }
            private set
            {
                if (_selApprovalRequest != value)
                {
                    _selApprovalRequest = value;
                    RaisePropertyChanged(() => SelApprovalRequest);
                }
            }
        }

        /// <summary>
        ///     选择的在途申请
        /// </summary>
        public RequestDTO SelEnRouteRequest
        {
            get { return _selEnRouteRequest; }
            private set
            {
                if (_selEnRouteRequest != value)
                {
                    _selEnRouteRequest = value;
                    RaisePropertyChanged(() => SelEnRouteRequest);
                }
            }
        }

        /// <summary>
        ///     选择的申请明细
        /// </summary>
        public ApprovalHistoryDTO SelApprovalHistory
        {
            get { return _selApprovalHistory; }
            private set
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
            EditCommand.RaiseCanExecuteChanged();
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
            if (sender is Guid) //添加民航局批文
            {
                SelApprovalDoc.CaacDocumentId = doc.DocumentId;
                SelApprovalDoc.CaacDocumentName = doc.Name;
            }
            else //添加发改委批文
            {
                SelApprovalDoc.NdrcDocumentId = doc.DocumentId;
                SelApprovalDoc.NdrcDocumentName = doc.Name;
            }
        }

        #endregion

        #region 创建批文

        /// <summary>
        ///     创建新申请
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var newApprovalDoc = new ApprovalDocDTO
            {
                Id = Guid.NewGuid(),
                Status = (int) OperationStatus.草稿,
            };
            ApprovalDocs.AddNew(newApprovalDoc);
            RefreshCommandState();
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 往批文添加申请

        internal void AddRequestToApprovalDoc(RequestDTO request)
        {
            //var req = this.service.AddRequestToApprovalDoc(this.SelApprovalDoc, request);
            //this._needReFreshViewEnRouteRequest = true;
            //this.RaiseViewEnRouteRequest();
            //this._needReFreshViewRequest = true;
            //this.RaiseViewRequest();
            //this.SelRequest = req;
        }

        #endregion

        #region 移除批文下的申请

        internal void RemoveRequest(RequestDTO request)
        {
            //this.service.RemoveRequest(request);
            //this._needReFreshViewEnRouteRequest = true;
            //this.RaiseViewEnRouteRequest();
            //this._needReFreshViewRequest = true;
            //this.RaiseViewRequest();
            //this.SelRequest = ViewRequest.LastOrDefault();
        }

        #endregion

        #region 提交审核

        /// <summary>
        ///     提交审核
        /// </summary>
        public DelegateCommand<object> CommitCommand { get; private set; }

        private void OnCommit(object obj)
        {
            SelApprovalDoc.Status = (int) OperationStatus.待审核;
            RefreshCommandState();
        }

        private bool CanCommit(object obj)
        {
            // 选中批文为空时，按钮不可用
            if (SelApprovalDoc == null)
            {
                return false;
            }
            // 批文号、民航局批文文档、审批日期为空时，按钮不可用
            if (string.IsNullOrWhiteSpace(SelApprovalDoc.CaacApprovalNumber) ||
                SelApprovalDoc.CaacDocumentId == Guid.Empty ||
                SelApprovalDoc.CaacExamineDate == null)
            {
                return false;
            }
            // 选中批文的状态处于草稿，且批文申请明细不为空时，按钮可用
            return SelApprovalDoc.Status == (int) OperationStatus.草稿 && ApprovalRequests.Any();
        }

        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> CheckCommand { get; private set; }

        private void OnCheck(object obj)
        {
            SelApprovalDoc.Status = (int) OperationStatus.已审核;
            RefreshCommandState();
        }

        private bool CanCheck(object obj)
        {
            // 选中批文为空时，按钮不可用
            if (SelApprovalDoc == null)
            {
                return false;
            }
            // 批文号、民航局批文文档、审批日期为空时，按钮不可用
            if (string.IsNullOrWhiteSpace(SelApprovalDoc.CaacApprovalNumber) ||
                SelApprovalDoc.CaacDocumentId == Guid.Empty ||
                SelApprovalDoc.CaacExamineDate == null)
            {
                return false;
            }
            // 选中批文的状态处于审核，且批文申请明细不为空时，按钮可用
            return SelApprovalDoc.Status == (int) OperationStatus.待审核 && ApprovalRequests.Any();
        }

        #endregion

        #region 发送

        /// <summary>
        ///     发送
        /// </summary>
        public DelegateCommand<object> SendCommand { get; private set; }

        private void OnSend(object obj)
        {
            string content = "是否向【民航局】报送批文：" + SelApprovalDoc.CaacApprovalNumber + "？";
            MessageConfirm("确认报送批文", content, (o, e) =>
            {
                if (e.DialogResult == true)
                {
                    // 审核、已提交状态下可以发送。如果已处于提交状态，需要重新发送的，不必改变状态。
                    if (SelApprovalDoc != null && SelApprovalDoc.Status != (int) OperationStatus.已提交)
                    {
                        SelApprovalDoc.Status = (int) OperationStatus.已提交;
                    }
                    //this.service.SubmitChanges(sc =>
                    //{
                    //    if (sc.Error == null)
                    //    {
                    //        // 发送不成功的，也认为是已经做了发送操作，不回滚状态。始终可以重新发送。
                    //        this.service.TransferApprovalDoc(this.SelApprovalDoc.ApprovalDocID, tp => { }, null);
                    //        RefreshButtonState();
                    //    }
                    //}, null);
                }
            });
            RefreshCommandState();
        }

        private bool CanSend(object obj)
        {
            // 选中批文为空时，按钮不可用
            if (SelApprovalDoc == null) return false;
            // 选中批文的状态处于已审核或已提交时，按钮可用
            return SelApprovalDoc.Status == (int) OperationStatus.已审核 ||
                   SelApprovalDoc.Status == (int) OperationStatus.已提交;
        }

        #endregion

        #region 修改批文

        /// <summary>
        ///     修改批文
        /// </summary>
        public DelegateCommand<object> EditCommand { get; private set; }

        private void OnEdit(object obj)
        {
            const string content = "确认后批文状态将改为草稿并允许编辑，是否要对该批文进行修改？";
            MessageConfirm("确认修改批文", content, (o, e) =>
            {
                if (e.DialogResult == true)
                {
                    SelApprovalDoc.Status = (int) OperationStatus.草稿;
                    //this.service.SubmitChanges(sc => { }, null);
                    RefreshCommandState();
                }
            });
        }

        private bool CanEdit(object obj)
        {
            // 选中批文为空时，按钮不可用
            if (SelApprovalDoc == null) return false;
            // 选中批文的状态不是草稿时，按钮可用
            return SelApprovalDoc.Status != (int) OperationStatus.草稿;
        }

        #endregion

        #endregion
    }
}