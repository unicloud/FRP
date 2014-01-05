#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
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
        private readonly FleetPlanData _context;
        private readonly IFleetPlanService _service;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public RequestVM(IFleetPlanService service) : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialRequest(); // 初始化申请信息
            InitialCommand(); //初始化命令
            InitialAircraftType(); //初始化机型
            InitialPlan(); // 初始化计划历史信息
            InitialAircraftCategory(); //初始化座级
            InitialActionCategory(); //初始化活动类型历史信息
            InitialAnnual(); //执行年度
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
            RequestsView = _service.CreateCollection(_context.Requests);
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

        #region 加载机型

        private AircraftTypeDTO _selectedAircraftType;

        /// <summary>
        ///     选择机型。
        /// </summary>
        public AircraftTypeDTO SelectedAircraftType
        {
            get { return _selectedAircraftType; }
            set
            {
                if (_selectedAircraftType != value)
                {
                    _selectedAircraftType = value;
                    RaisePropertyChanged(() => SelectedAircraftType);
                }
            }
        }

        /// <summary>
        ///     获取所有机型信息。
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftTypeDTO> AircraftTypesView { get; set; }

        /// <summary>
        ///     初始化机型信息。
        /// </summary>
        private void InitialAircraftType()
        {
            AircraftTypesView = _service.CreateCollection(_context.AircraftTypes);
            AircraftTypesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedAircraftType == null)
                {
                    SelectedAircraftType = e.Entities.Cast<AircraftTypeDTO>().FirstOrDefault();
                }
            };
        }

        #endregion

        #region 加载座级

        private AircraftCategoryDTO _selectedAircraftCategory;

        /// <summary>
        ///     选择座级。
        /// </summary>
        public AircraftCategoryDTO SelectedAircraftCategory
        {
            get { return _selectedAircraftCategory; }
            set
            {
                if (_selectedAircraftCategory != value)
                {
                    _selectedAircraftCategory = value;
                    RaisePropertyChanged(() => SelectedAircraftCategory);
                }
            }
        }

        /// <summary>
        ///     获取所有座级信息。
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftCategoryDTO> AircraftCategoriesView { get; set; }

        /// <summary>
        ///     初始化座级信息。
        /// </summary>
        private void InitialAircraftCategory()
        {
            AircraftCategoriesView = _service.CreateCollection(_context.AircraftCategories);
            AircraftCategoriesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedAircraftCategory == null)
                {
                    SelectedAircraftCategory = e.Entities.Cast<AircraftCategoryDTO>().FirstOrDefault();
                }
            };
        }

        #endregion

        #region 加载计划

        private PlanDTO _selectedPlan;

        /// <summary>
        ///     选择计划历史。
        /// </summary>
        public PlanDTO SelectedPlan
        {
            get { return _selectedPlan; }
            set
            {
                if (_selectedPlan != value)
                {
                    _selectedPlan = value;
                    RaisePropertyChanged(() => SelectedPlan);
                }
            }
        }

        /// <summary>
        ///     获取所有计划历史信息。
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> PlansView { get; set; }

        /// <summary>
        ///     初始化计划历史信息。
        /// </summary>
        private void InitialPlan()
        {
            PlansView = _service.CreateCollection(_context.Plans);
            PlansView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedPlan == null)
                {
                    SelectedPlan = e.Entities.Cast<PlanDTO>().FirstOrDefault();
                }
            };
        }

        #endregion

        #region 加载执行年度

        private AnnualDTO _selectedAnnual;

        /// <summary>
        ///     选择执行年度历史。
        /// </summary>
        public AnnualDTO SelectedAnnual
        {
            get { return _selectedAnnual; }
            set
            {
                if (_selectedAnnual != value)
                {
                    _selectedAnnual = value;
                    RaisePropertyChanged(() => SelectedAnnual);
                }
            }
        }

        /// <summary>
        ///     获取所有执行年度历史信息。
        /// </summary>
        public QueryableDataServiceCollectionView<AnnualDTO> AnnualsView { get; set; }

        /// <summary>
        ///     初始化执行年度历史信息。
        /// </summary>
        private void InitialAnnual()
        {
            AnnualsView = _service.CreateCollection(_context.Annuals);
            AnnualsView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedAnnual == null)
                {
                    SelectedAnnual = e.Entities.Cast<AnnualDTO>().FirstOrDefault();
                }
            };
        }

        #endregion

        #region 加载活动类型

        private ActionCategoryDTO _selectedActionCategory;

        /// <summary>
        ///     选择活动类型历史。
        /// </summary>
        public ActionCategoryDTO SelectedActionCategory
        {
            get { return _selectedActionCategory; }
            set
            {
                if (_selectedActionCategory != value)
                {
                    _selectedActionCategory = value;
                    RaisePropertyChanged(() => SelectedActionCategory);
                }
            }
        }

        /// <summary>
        ///     获取所有活动类型历史信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ActionCategoryDTO> ActionCategoriesView { get; set; }

        /// <summary>
        ///     初始化活动类型历史信息。
        /// </summary>
        private void InitialActionCategory()
        {
            ActionCategoriesView = _service.CreateCollection(_context.ActionCategories);
            ActionCategoriesView.FilterDescriptors.Add(new FilterDescriptor("ActionType", FilterOperator.IsEqualTo, "引进"));
            ActionCategoriesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedActionCategory == null)
                {
                    SelectedActionCategory = e.Entities.Cast<ActionCategoryDTO>().FirstOrDefault();
                }
            };
        }

        #endregion

        #region 获取月份集合
        /// <summary>
        /// 设置月份
        /// </summary>
        public List<int> Months
        {
            get
            {
                return new List<int>
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
            }
        }

        #endregion

        #region 命令

        #region 新增申请命令

        public DelegateCommand<object> AddRequestCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndAddRequest(object sender)
        {
            var request = new RequestDTO
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
            SelectedRequest.Status = (int) RequestStatus.待审核;
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
            return SelectedRequest != null && SelectedRequest.Status < (int) RequestStatus.待审核;
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
            SelectedRequest.Status = (int) RequestStatus.已审核;
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
            return SelectedRequest != null && SelectedRequest.Status < (int) RequestStatus.已审核
                   && SelectedRequest.Status > (int) RequestStatus.草稿;
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

        public override void LoadData()
        {
            //加载申请
            if (!RequestsView.AutoLoad)
            {
                RequestsView.AutoLoad = true;
            }
            else
            {
                RequestsView.Load(true);
            }

            //加载计划
            if (!PlansView.AutoLoad)
            {
                PlansView.AutoLoad = true;
            }
            else
            {
                PlansView.Load(true);
            }

            AircraftTypesView.AutoLoad = true; //加载机型
            AircraftCategoriesView.AutoLoad = true; //加载座级
            //AnnualsView.AutoLoad = true; //加载执行年度
            ActionCategoriesView.AutoLoad = true; //加载操作类型
        }

        protected override void RefreshCommandState()
        {
        }

        protected override bool OnSaveExecuting(object sender)
        {
            return true;
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
    }
}