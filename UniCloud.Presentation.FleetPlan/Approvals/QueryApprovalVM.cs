#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/19，16:01
// 文件名：QueryApprovalVM.cs
// 程序集：UniCloud.Presentation.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.FleetPlan.Approvals
{
    [Export(typeof (QueryApprovalVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QueryApprovalVM : ViewModelBase
    {
        private readonly FleetPlanData _context;
        private readonly IFleetPlanService _service;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public QueryApprovalVM(IFleetPlanService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialRequest(); //初始化申请
        }

        #region 加载申请

        private ApprovalRequestDTO _selectedRequest;

        /// <summary>
        ///     选择申请。
        /// </summary>
        public ApprovalRequestDTO SelectedRequest
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
        ///     获取所有申请信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ApprovalRequestDTO> RequestsView { get; set; }

        /// <summary>
        ///     初始化申请信息。
        /// </summary>
        private void InitialRequest()
        {
            RequestsView = _service.CreateCollection(_context.ApprovalRequests);
            _service.RegisterCollectionView(RequestsView);
            RequestsView.PageSize = 20;
            RequestsView.LoadedData += (sender, e) =>
            {
                SetIsBusy();
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedRequest == null)
                {
                    SelectedRequest = e.Entities.Cast<ApprovalRequestDTO>().FirstOrDefault();
                }
            };
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
        }

        protected override void SetIsBusy()
        {
            IsBusy = RequestsView.IsBusy;
        }

        #endregion
    }
}