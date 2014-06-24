//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

#region 命名空间

using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.ServiceModel.Web;
using System.Web;
using UniCloud.Application.FleetPlanBC.AircraftPlanServices;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.FleetTransferServices;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DistributedServices.FleetPlan
{
    public class FleetPlanDataService : DataService<FleetPlanData>
    {
        /// <summary>
        ///     初始化服务端策略
        /// </summary>
        /// <param name="config">数据服务配置</param>
        public static void InitializeService(DataServiceConfiguration config)
        {
            #region 实体集访问控制

            config.SetEntitySetAccessRule("*", EntitySetRights.All);

            #endregion

            #region 服务操作访问控制

            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("PerformPlanQuery", ServiceOperationRights.All);

            config.SetServiceOperationAccessRule("TransferRequest", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("TransferPlan", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("TransferPlanAndRequest", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("TransferApprovalDoc", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("TransferOperationHistory", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("TransferAircraftBusiness", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("TransferOwnershipHistory", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("TransferPlanHistory", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("TransferApprovalRequest", ServiceOperationRights.All);

            #endregion

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        #region 服务操作

        /// <summary>
        ///     控制生成的服务是否需要缓存
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            base.OnStartProcessingRequest(args);

            var cachePolicy = HttpContext.Current.Response.Cache;

            // no-cache是会被缓存的，只不过每次在向客户端（浏览器）提供响应数据时，缓存都要向服务器评估缓存响应的有效性。 
            cachePolicy.SetCacheability(HttpCacheability.NoCache);

            // default cache expire: never 
            //cachePolicy.SetExpires(DateTime.MaxValue);

            // cached output depends on: accept, charset, encoding, and all parameters (like $filter, etc) 
            cachePolicy.VaryByHeaders["Accept"] = true;
            cachePolicy.VaryByHeaders["Accept-Charset"] = true;
            cachePolicy.VaryByHeaders["Accept-Encoding"] = true;
            cachePolicy.VaryByParams["*"] = true;

            cachePolicy.SetValidUntilExpires(true);
        }

        #endregion

        #region 计划执行情况查询 

        [WebGet]
        public PerformPlan PerformPlanQuery(string planHistoryId, string approvalHistoryId, int planType,
            string relatedGuid)
        {
            var planAppService = UniContainer.Resolve<IPlanAppService>();
            return planAppService.PerformPlanQuery(planHistoryId, approvalHistoryId, planType, relatedGuid);
        }

        #region 数据传输

        /// <summary>
        ///     传输申请
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentRequest"></param>
        [WebGet]
        public bool TransferRequest(string currentAirlines, string currentRequest)
        {
            var airlinesId = Guid.Parse(currentAirlines);
            var id = Guid.Parse(currentRequest);
            var transferService = UniContainer.Resolve<IFleetTransferService>();
            return transferService.TransferRequest(airlinesId, id);
        }

        /// <summary>
        ///     传输计划
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        [WebGet]
        public bool TransferPlan(string currentAirlines, string currentPlan)
        {
            var airlinesId = Guid.Parse(currentAirlines);
            var id = Guid.Parse(currentPlan);
            var transferService = UniContainer.Resolve<IFleetTransferService>();
            return transferService.TransferPlan(airlinesId, id);
        }

        /// <summary>
        ///     传输计划申请
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        /// <param name="currentRequest"></param>
        /// <returns></returns>
        [WebGet]
        public bool TransferPlanAndRequest(string currentAirlines, string currentPlan, string currentRequest)
        {
            var airlinesId = Guid.Parse(currentAirlines);
            var planId = Guid.Parse(currentPlan);
            var requestId = Guid.Parse(currentRequest);
            var transferService = UniContainer.Resolve<IFleetTransferService>();
            return transferService.TransferPlanAndRequest(airlinesId, planId, requestId);
        }

        /// <summary>
        ///     传输计划申请批文（针对指标飞机数据）
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        /// <param name="currentRequest"></param>
        /// <param name="currentApprovalDoc"></param>
        /// <returns></returns>
        [WebGet]
        public bool TransferApprovalRequest(string currentAirlines, string currentPlan, string currentRequest,
            string currentApprovalDoc)
        {
            var airlinesId = Guid.Parse(currentAirlines);
            var planId = Guid.Parse(currentPlan);
            var requestId = Guid.Parse(currentRequest);
            var approvalDocId = Guid.Parse(currentApprovalDoc);
            var transferService = UniContainer.Resolve<IFleetTransferService>();
            return transferService.TransferApprovalRequest(airlinesId, planId, requestId, approvalDocId);
        }

        /// <summary>
        ///     传输批文
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentApprovalDoc"></param>
        [WebGet]
        public bool TransferApprovalDoc(string currentAirlines, string currentApprovalDoc)
        {
            var airlinesId = Guid.Parse(currentAirlines);
            var id = Guid.Parse(currentApprovalDoc);
            var transferService = UniContainer.Resolve<IFleetTransferService>();
            return transferService.TransferApprovalDoc(airlinesId, id);
        }

        /// <summary>
        ///     传输运营历史
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentOperationHistory"></param>
        /// <returns></returns>
        [WebGet]
        public bool TransferOperationHistory(string currentAirlines, string currentOperationHistory)
        {
            var airlinesId = Guid.Parse(currentAirlines);
            var id = Guid.Parse(currentOperationHistory);
            var transferService = UniContainer.Resolve<IFleetTransferService>();
            return transferService.TransferOperationHistory(airlinesId, id);
        }


        /// <summary>
        ///     传输商业数据
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentAircraftBusiness"></param>
        [WebGet]
        public bool TransferAircraftBusiness(string currentAirlines, string currentAircraftBusiness)
        {
            var airlinesId = Guid.Parse(currentAirlines);
            var id = Guid.Parse(currentAircraftBusiness);
            var transferService = UniContainer.Resolve<IFleetTransferService>();
            return transferService.TransferAircraftBusiness(airlinesId, id);
        }

        /// <summary>
        ///     传输所有权历史
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentOwnershipHistory"></param>
        [WebGet]
        public bool TransferOwnershipHistory(string currentAirlines, string currentOwnershipHistory)
        {
            var airlinesId = Guid.Parse(currentAirlines);
            var id = Guid.Parse(currentOwnershipHistory);
            var transferService = UniContainer.Resolve<IFleetTransferService>();
            return transferService.TransferOwnershipHistory(airlinesId, id);
        }

        /// <summary>
        ///     传输计划明细
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlanHistory"></param>
        /// <returns></returns>
        [WebGet]
        public bool TransferPlanHistory(string currentAirlines, string currentPlanHistory)
        {
            var airlinesId = Guid.Parse(currentAirlines);
            var id = Guid.Parse(currentPlanHistory);
            var transferService = UniContainer.Resolve<IFleetTransferService>();
            return transferService.TransferPlanHistory(airlinesId, id);
        }

        #endregion

        #endregion
    }
}