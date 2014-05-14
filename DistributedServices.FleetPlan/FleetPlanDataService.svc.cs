//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System;
using System.ServiceModel.Web;
using UniCloud.Application.FleetPlanBC.AircraftPlanServices;
using UniCloud.Application.FleetPlanBC.ApprovalDocServices;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.FleetTransferServices;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.DistributedServices.FleetPlan
{
    using System.Data.Services;
    using System.Data.Services.Common;
    using System.Web;

    public class FleetPlanDataService : DataService<FleetPlanData>
    {
        /// <summary>
        /// ��ʼ������˲���
        /// </summary>
        /// <param name="config">���ݷ�������</param>
        public static void InitializeService(DataServiceConfiguration config)
        {
            #region ʵ�弯���ʿ���

            config.SetEntitySetAccessRule("*", EntitySetRights.All);

            #endregion

            #region ����������ʿ���

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

            #endregion

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
          
        }

        #region �������

        /// <summary>
        /// �������ɵķ����Ƿ���Ҫ����
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            base.OnStartProcessingRequest(args);

            HttpCachePolicy cachePolicy = HttpContext.Current.Response.Cache;

            // no-cache�ǻᱻ����ģ�ֻ����ÿ������ͻ��ˣ���������ṩ��Ӧ����ʱ�����涼Ҫ�����������������Ӧ����Ч�ԡ� 
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

        #region �ƻ�ִ�������ѯ 

        [WebGet]
        public PerformPlan PerformPlanQuery(string planHistoryId, string approvalHistoryId, int planType,
            string relatedGuid)
        {
         var  planAppService = DefaultContainer.Resolve<IPlanAppService>();
          return  planAppService.PerformPlanQuery(planHistoryId, approvalHistoryId, planType, relatedGuid);
        }

        #region ���ݴ���

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentRequest"></param>
        [WebGet]
        public bool TransferRequest(Guid currentAirlines, Guid currentRequest)
        {
            var transferService = DefaultContainer.Resolve<IFleetTransferService>();
            return transferService.TransferRequest(currentAirlines, currentRequest);
        }

        /// <summary>
        /// ����ƻ�
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        [WebGet]
        public bool TransferPlan(Guid currentAirlines, Guid currentPlan)
        {
            var transferService = DefaultContainer.Resolve<IFleetTransferService>();
            return transferService.TransferRequest(currentAirlines, currentPlan);

        }

        /// <summary>
        /// ����ƻ�����
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        /// <param name="currentRequest"></param>
        /// <returns></returns>
        [WebGet]
        public bool TransferPlanAndRequest(Guid currentAirlines, Guid currentPlan, Guid currentRequest)
        {
            var transferService = DefaultContainer.Resolve<IFleetTransferService>();
            return transferService.TransferPlanAndRequest(currentAirlines, currentPlan, currentRequest);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentApprovalDoc"></param>
        [WebGet]
        public bool TransferApprovalDoc(Guid currentAirlines, Guid currentApprovalDoc)
        {
            var transferService = DefaultContainer.Resolve<IFleetTransferService>();
            return transferService.TransferApprovalDoc(currentAirlines, currentApprovalDoc);
        }

        /// <summary>
        /// ������Ӫ��ʷ
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentOperationHistory"></param>
        /// <returns></returns>
        [WebGet]
        public bool TransferOperationHistory(Guid currentAirlines, Guid currentOperationHistory)
        {
            var transferService = DefaultContainer.Resolve<IFleetTransferService>();
            return transferService.TransferOperationHistory(currentAirlines, currentOperationHistory);
        }


        /// <summary>
        /// ������ҵ����
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentAircraftBusiness"></param>
        [WebGet]
        public bool TransferAircraftBusiness(Guid currentAirlines, Guid currentAircraftBusiness)
        {
            var transferService = DefaultContainer.Resolve<IFleetTransferService>();
            return transferService.TransferAircraftBusiness(currentAirlines, currentAircraftBusiness);
        }

        /// <summary>
        /// ��������Ȩ��ʷ
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentOwnershipHistory"></param>
        [WebGet]
        public bool TransferOwnershipHistory(Guid currentAirlines, Guid currentOwnershipHistory)
        {
            var transferService = DefaultContainer.Resolve<IFleetTransferService>();
            return transferService.TransferOwnershipHistory(currentAirlines, currentOwnershipHistory);
        }

        /// <summary>
        /// ����ƻ���ϸ
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlanHistory"></param>
        /// <returns></returns>
        [WebGet]
        public bool TransferPlanHistory(Guid currentAirlines, Guid currentPlanHistory)
        {
            var transferService = DefaultContainer.Resolve<IFleetTransferService>();
            return transferService.TransferPlanHistory(currentAirlines, currentPlanHistory);
        }

        #endregion

        #endregion
    }
}
