#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.Document;
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
        private readonly FleetPlanData _context;
        private readonly IFleetPlanService _service;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public ApprovalVM(IFleetPlanService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialRequest(); //初始化申请
            InitialApprovalDoc(); //初始化批文
            InitialCommand(); //初始化命令
        }

        #region 加载申请

        private ApprovalHistoryDTO _selectedApprovalHistory;
        private RequestDTO _selectedEnRouteRequest;
        private RequestDTO _selectedRequest;

        /// <summary>
        ///     选择申请。
        /// </summary>
        public RequestDTO SelectedRequest
        {
            get { return _selectedRequest; }
            set
            {
                if (_selectedRequest != value)
                {
                    _selectedRequest = value;
                    RaisePropertyChanged(() => SelectedRequest);
                }
            }
        }

        /// <summary>
        ///     选择在途申请。
        /// </summary>
        public RequestDTO SelectedEnRouteRequest
        {
            get { return _selectedEnRouteRequest; }
            set
            {
                if (_selectedEnRouteRequest != value)
                {
                    _selectedEnRouteRequest = value;
                    RaisePropertyChanged(() => SelectedEnRouteRequest);
                }
            }
        }

        /// <summary>
        ///     选择申请明细。
        /// </summary>
        public ApprovalHistoryDTO SelectedApprovalHistory
        {
            get { return _selectedApprovalHistory; }
            set
            {
                if (_selectedApprovalHistory != value)
                {
                    _selectedApprovalHistory = value;
                    RaisePropertyChanged(() => SelectedApprovalHistory);
                }
            }
        }

        /// <summary>
        ///     获取所有申请信息。
        /// </summary>
        public QueryableDataServiceCollectionView<RequestDTO> RequestsView { get; set; }

        /// <summary>
        ///     在途申请集合
        /// </summary>
        public IEnumerable<RequestDTO> ViewEnRouteRequest
        {
            get
            {
                return
                    RequestsView.SourceCollection.Cast<RequestDTO>()
                        .Where(r => r.Status == (int) RequestStatus.已提交 || r.Status == (int) RequestStatus.已审批);
            }
        }

        /// <summary>
        ///     批文的申请集合
        /// </summary>
        public IEnumerable<RequestDTO> ViewRequest
        {
            get
            {
                var requests=
                    SelectedApprovalDoc == null
                        ? null
                        : RequestsView.Where(r => r.ApprovalDocId == SelectedApprovalDoc.Id);
                if (requests==null)
                {
                    return null;
                }
                SelectedRequest = requests.FirstOrDefault();
                return requests;
            }
        }

        /// <summary>
        ///     初始化申请信息。
        /// </summary>
        private void InitialRequest()
        {
            RequestsView = _service.CreateCollection(_context.Requests, o => o.ApprovalHistories);
            _service.RegisterCollectionView(RequestsView);
            RequestsView.LoadedData += (sender, e) =>
            {
                SetIsBusy();
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                RefreshCommandState();
                RefreshRequest();
            };
        }

        /// <summary>
        ///     刷新申请
        /// </summary>
        private void RefreshRequest()
        {
            RaisePropertyChanged(() => ViewEnRouteRequest);
            RaisePropertyChanged(() => ViewRequest);
        }

        #endregion

        #region 加载批文

        private ApprovalDocDTO _selectedApprovalDoc;

        /// <summary>
        ///     选择批文。
        /// </summary>
        public ApprovalDocDTO SelectedApprovalDoc
        {
            get { return _selectedApprovalDoc; }
            set
            {
                if (_selectedApprovalDoc != value)
                {
                    _selectedApprovalDoc = value;
                    RaisePropertyChanged(() => SelectedApprovalDoc);
                    RaisePropertyChanged(()=>ApprovalDocReadOnly);
                    RefreshRequest();
                }
            }
        }


        /// <summary>
        ///     获取所有批文信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ApprovalDocDTO> ApprovalDocsView { get; set; }

        /// <summary>
        ///     初始化批文信息。
        /// </summary>
        private void InitialApprovalDoc()
        {
            ApprovalDocsView = _service.CreateCollection(_context.ApprovalDocs);
            _service.RegisterCollectionView(ApprovalDocsView);
            ApprovalDocsView.PageSize = 20;
            ApprovalDocsView.LoadedData += (sender, e) =>
            {
                SetIsBusy();
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                RefreshCommandState();
                RefreshRequest();
            };
        }

        #endregion

        #region 附件添加相关

        private bool _isDropDownClose;

        private string _selDocType;

        public bool IsDropDownClose
        {
            get { return _isDropDownClose; }
            set
            {
                if (_isDropDownClose != value)
                {
                    _isDropDownClose = value;
                    RaisePropertyChanged(() => IsDropDownClose);
                }
            }
        }

        /// <summary>
        ///     选中添加附件的类型
        /// </summary>
        public string SelDocType
        {
            get { return _selDocType; }
            set
            {
                _selDocType = value;
                IsDropDownClose = false;
                if (value != null)
                {
                    AddAttach();
                }
                RaisePropertyChanged(() => SelDocType);
            }
        }

        /// <summary>
        ///     添加附件类型的集合
        /// </summary>
        public List<string> DocTypes
        {
            get
            {
                return new List<string>
                {
                    "民航局批文文档",
                    "发改委批文文档",
                };
            }
        }

        /// <summary>
        ///     附件添加按钮是否可用
        /// </summary>
        public bool AttachButtonEnabled
        {
            get
            {
                if (!GetButtonState())
                {
                    return false;
                }
                return SelectedApprovalDoc != null && SelectedApprovalDoc.Status < (int) OperationStatus.已审核;
            }
        }

        /// <summary>
        ///     添加附件
        /// </summary>
        private void AddAttach()
        {
            if (SelectedApprovalDoc == null)
            {
                MessageAlert("请选择一条记录！");
                SelDocType = null;
                return;
            }
            OnAddAttach(Guid.Empty);
        }

        protected override void WindowClosed(DocumentDTO doc, object sender)
        {
            if (doc!=null)
            {
                if (SelDocType.Equals("民航局批文文档"))
                {
                    SelectedApprovalDoc.CaacDocumentId = doc.DocumentId;
                    SelectedApprovalDoc.CaacDocumentName = doc.Name;
                }
                else
                {
                    SelectedApprovalDoc.NdrcDocumentId = doc.DocumentId;
                    SelectedApprovalDoc.NdrcDocumentName = doc.Name;
                }
            }
            SelDocType = null;
        }
        #endregion

        #region 申请拖拽，批文增加、删除,批文状态控制

        /// <summary>
        ///     批文是否只读
        /// </summary>
        public bool ApprovalDocReadOnly
        {
            get
            {
                return SelectedApprovalDoc == null ||
                       SelectedApprovalDoc.Status >= (int) OperationStatus.已审核;
            }
        }

        /// <summary>
        ///     申请能否被拖拽
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool DragRequest(object request)
        {
            return request is RequestDTO && SelectedApprovalDoc != null &&
                   SelectedApprovalDoc.Status < (int) OperationStatus.已审核 &&
                   (request as RequestDTO).Status == (int) RequestStatus.已提交;
        }

        /// <summary>
        /// 判断批文中的申请是否可拖拽或双击
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool DrapApproval(object request)
        {
            return request is RequestDTO && SelectedApprovalDoc != null &&
                   SelectedApprovalDoc.Status < (int) OperationStatus.已审核;
        }

        /// <summary>
        ///     创建批文历史
        /// </summary>
        /// <param name="request"></param>
        public void AddRequest(RequestDTO request)
        {
            if (!GetButtonState())
            {
                MessageAlert("当前界面处于加载中或提交数据中，请稍后...");
            }
            if (!DragRequest(request))
            {
                MessageAlert("当前申请不能添加到批文中");
                return;
            }
            request.ApprovalDocId = SelectedApprovalDoc.Id;
            // 申请状态改为已审批
            request.Status = (int) RequestStatus.已审批;
            // 相关申请明细对应计划飞机置为批准，其管理状态置为批文
            request.ApprovalHistories.ToList().ForEach(ah =>
            {
                ah.IsApproved = true;
                ah.PlanAircraftStatus = (int) ManageStatus.批文;
            });
            RaisePropertyChanged(()=>ViewRequest);
        }

        /// <summary>
        ///     删除申请明细
        /// </summary>
        public void RemoveRequest(RequestDTO request)
        {
            if (!GetButtonState())
            {
                MessageAlert("当前界面处于加载中或提交数据中，请稍后...");
            }
            if (!DrapApproval(request))
            {
                MessageAlert("当前申请已经过审核不能删除");
                return;
            }
            request.ApprovalDocId = null;
            // 申请状态改为已审批
            request.Status = (int) RequestStatus.已提交;
            // 相关申请明细对应计划飞机置为批准，其管理状态置为批文
            request.ApprovalHistories.ToList().ForEach(ah =>
            {
                ah.IsApproved = false;
                ah.PlanAircraftStatus = (int)ManageStatus.申请;
            });
            RefreshRequest();

        }

        #endregion

        #region 命令

        #region 新增批文命令

        public DelegateCommand<object> AddApprovalCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndAddApproval(object sender)
        {
            var approvalDoc = new ApprovalDocDTO
            {
                Id = Guid.NewGuid(),
            };
            ApprovalDocsView.AddNewItem(approvalDoc);
            RefreshCommandState();
        }

        /// <summary>
        ///     判断新增命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddApproval(object sender)
        {
            return GetButtonState();
        }

        #endregion

        #region 提交审核

        public DelegateCommand<object> SubmitApprovalCommand { get; private set; }

        /// <summary>
        ///     提交审核。
        /// </summary>
        /// <param name="sender"></param>
        public void OnSubmitApproval(object sender)
        {
            if (SelectedApprovalDoc == null)
            {
                MessageAlert("批文不能为空");
                return;
            }
            SelectedApprovalDoc.Status = (int) OperationStatus.待审核;
            RefreshCommandState();
        }

        /// <summary>
        ///     判断提交审核命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>提交审核命令是否可用。</returns>
        public bool CanSubmitApproval(object sender)
        {
            if (!GetButtonState())
            {
                return false;
            }
            return SelectedApprovalDoc != null && SelectedApprovalDoc.Status < (int) RequestStatus.待审核;
        }

        #endregion

        #region 审核

        public DelegateCommand<object> ReviewApprovalCommand { get; private set; }

        /// <summary>
        ///     执行编辑付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnReviewApproval(object sender)
        {
            if (SelectedApprovalDoc == null)
            {
                MessageAlert("批文不能为空");
                return;
            }
            SelectedApprovalDoc.Status = (int)OperationStatus.已审核;
            RefreshCommandState();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanReviewApproval(object sender)
        {
            if (!GetButtonState())
            {
                return false;
            }
            return SelectedApprovalDoc != null && SelectedApprovalDoc.Status < (int) OperationStatus.已审核
                   && SelectedApprovalDoc.Status > (int) OperationStatus.草稿;
        }

        #endregion

        /// <summary>
        ///     获取按钮状态
        /// </summary>
        /// <returns></returns>
        private bool GetButtonState()
        {
            //当处于加载中，按钮是不可用的
            return !RequestsView.IsLoading
                   && !RequestsView.IsSubmittingChanges
                   && !ApprovalDocsView.IsLoading
                   && !ApprovalDocsView.IsLoading;

        }

        /// <summary>
        ///     初始化命令
        /// </summary>
        private void InitialCommand()
        {
            AddApprovalCommand = new DelegateCommand<object>(OndAddApproval, CanAddApproval);
            SubmitApprovalCommand = new DelegateCommand<object>(OnSubmitApproval,
                CanSubmitApproval);
            ReviewApprovalCommand = new DelegateCommand<object>(OnReviewApproval,
                CanReviewApproval);
        }

        #endregion

        #region 重载基类服务

        public override void LoadData()
        {
            if (!RequestsView.AutoLoad)
            {
                RequestsView.AutoLoad = true;
            }
            else
            {
                RequestsView.Load(true);
            }

            if (!ApprovalDocsView.AutoLoad)
            {
                ApprovalDocsView.AutoLoad = true;
            }
            else
            {
                ApprovalDocsView.Load(true);
            }
        }

        protected override void RefreshCommandState()
        {
            AddApprovalCommand.RaiseCanExecuteChanged();
            SubmitApprovalCommand.RaiseCanExecuteChanged();
            ReviewApprovalCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged(() => AttachButtonEnabled);
            RaisePropertyChanged(() => ApprovalDocReadOnly);
        }

        protected override bool OnSaveExecuting(object sender)
        {
            ApprovalDocsView.ToList().ForEach(p => RequestsView.Where(c => c.ApprovalDocId == p.Id)
                .SelectMany(c => c.ApprovalHistories).ToList()
                .ForEach(c =>
                {
                    if (c.IsApproved)
                    {
                        c.PlanAircraftStatus = (int) ManageStatus.批文;
                    }
                }));
            return true;
        }

        protected override void OnSaveSuccess(object sender)
        {
           
        }

        protected override void SetIsBusy()
        {
            if (RequestsView == null || ApprovalDocsView==null)
            {
                IsBusy = true;
                return;
            }
            IsBusy = RequestsView.IsBusy || ApprovalDocsView.IsBusy;
        }

        #endregion
    }
}