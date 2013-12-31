#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.Requests
{
    [Export(typeof (RequestVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class RequestVM : EditViewModelBase
    {
        private FleetPlanData _context;

          /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public RequestVM()
          {
              InitialRequest();// 初始化申请信息。
              InitialCommand();//初始化命令
          }

        #region 加载申请

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
                    RefreshCommandState();
                    RaisePropertyChanged(() => SelectedRequest);
                }
            }
        }

        /// <summary>
        ///     获取所有申请信息。
        /// </summary>
        public QueryableDataServiceCollectionView<RequestDTO> RequestsView { get; set; }

        /// <summary>
        ///     初始化申请信息。
        /// </summary>
        private void InitialRequest()
        {
            RequestsView = Service.CreateCollection(_context.Requests);
            RequestsView.PageSize = 20;
            RequestsView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedRequest == null)
                {
                    SelectedRequest = e.Entities.Cast<RequestDTO>().FirstOrDefault();
                }
                RefreshCommandState();
            };
        }

        #endregion

        //#region 方法
        ///// <summary>
        ///// 创建新的申请明细
        ///// </summary>
        ///// <param name="planHistory"></param>
        //internal void AddNewRequestDetail(pl planHistory)
        //{
        //    var sawsApproveHistory = new ApprovalHistoryDTO
        //    {
        //        PlanAircraftId = planHistory.PlanAircraft.ID,
        //        AirlinesID = planHistory.AirlinesID,
        //        SAWSRequestID = _currentRequestDataObject.ID,
        //        ImportCategoryID = planHistory.ActionCategoryID,
        //        RequestDeliverAnnualID = planHistory.PerformAnnualID,
        //        RequestDeliverMonth = planHistory.PerformMonth,
        //        SeatingCapacity = planHistory.SeatingCapacity,
        //        CarryingCapacity = planHistory.CarryingCapacity
        //    };
        //    var sawsApproveHistories = new SAWSApprovalHistoryDataObjectList();
        //    sawsApproveHistories.Add(sawsApproveHistory);
        //    // 把申请明细赋给关联的计划明细
        //    // planHistory.ApprovalHistory = sawsApproveHistories;
        //    //sawsApproveHistory.PlanAircraft.Status = (int)ManageStatus.Request;
        //    ApprovalHistoryDataObjectList.Add(sawsApproveHistory);
        //    CurrentRequestDataObject.SAWSApprovalHistories = ApprovalHistoryDataObjectList;
        //}

        ///// <summary>
        ///// 移除申请明细
        ///// </summary>
        ///// <param name="requestDetail"></param>
        //internal void RemoveRequestDetail(SAWSApprovalHistoryDataObject requestDetail)
        //{
        //    ApprovalHistoryDataObjectList.Remove(requestDetail);
        //    // 相关计划飞机的管理状态改为计划
        //    //requestDetail.PlanAircraft.Status = (int)ManageStatus.Plan;
        //    //RegisterModified(requestDetail.PlanAircraft.ID, requestDetail.PlanAircraft);
        //    RegisterModified(CurrentRequestDataObject.ID, CurrentRequestDataObject);
        //}

        //#endregion

        #region 命令

        #region 新增申请命令

        public DelegateCommand<object> AddRequestCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndAddRequest(object sender)
        {
            var request = new RequestDTO()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
            };
            RequestsView.AddNew(request);
        }

        /// <summary>
        ///     判断新增申请命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddAddRequest(object sender)
        {
            return GetButtonState();
        }

        #endregion

        #region 提交审核

        public DelegateCommand<object> SubmitRequestCommand { get; private set; }

        /// <summary>
        ///     提交审核。
        /// </summary>
        /// <param name="sender"></param>
        public void OnSubmitRequest(object sender)
        {
            if (SelectedRequest == null)
            {
                MessageAlert("提示", "请选择需要删除的记录");
                return;
            }
            SelectedRequest.Status = (int)RequestStatus.待审核;
            RefreshCommandState();
        }

        /// <summary>
        ///     判断提交审核命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>提交审核命令是否可用。</returns>
        public bool CanSubmitRequest(object sender)
        {
            if (!GetButtonState())
            {
                return false;
            }
            return SelectedRequest != null && SelectedRequest.Status < (int)RequestStatus.待审核;
        }

        #endregion

        #region 审核申请

        public DelegateCommand<object> ReviewRequestCommand { get; private set; }

        /// <summary>
        ///     执行编辑付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnReviewRequest(object sender)
        {
            if (SelectedRequest == null)
            {
                MessageAlert("提示", "请选择需要提交审核的记录");
                return;
            }
            SelectedRequest.Status = (int)RequestStatus.已审核;
            RefreshCommandState();
        }

        /// <summary>
        ///     判断编辑飞机付款计划行命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanReviewRequest(object sender)
        {
            if (!GetButtonState())
            {
                return false;
            }
            return SelectedRequest != null && SelectedRequest.Status < (int)RequestStatus.已审核
                   && SelectedRequest.Status > (int)RequestStatus.草稿;
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
                   && !RequestsView.IsSubmittingChanges;
        }

        /// <summary>
        ///     初始化命令
        /// </summary>
        private void InitialCommand()
        {
            AddRequestCommand = new DelegateCommand<object>(OndAddRequest, CanAddAddRequest);
            SubmitRequestCommand = new DelegateCommand<object>(OnSubmitRequest,
                CanSubmitRequest);
            ReviewRequestCommand = new DelegateCommand<object>(OnReviewRequest,
                CanReviewRequest);
        }

        #endregion

        #region 重载基类服务

        protected override IService CreateService()
        {
            _context = new FleetPlanData(AgentHelper.PaymentUri);
            return new FleetPlanService(_context);
        }

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
        }

        protected override void RefreshCommandState()
        {
         
        }

        protected override bool OnSaveExecuting(object sender)
        {
            return true;
        }

        #endregion

    }
}