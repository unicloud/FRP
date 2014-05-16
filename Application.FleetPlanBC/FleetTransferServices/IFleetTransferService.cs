using System;

namespace UniCloud.Application.FleetPlanBC.FleetTransferServices
{
    public interface IFleetTransferService
    {
        #region 数据传输

        /// <summary>
        /// 传输申请
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentRequest"></param>
        bool TransferRequest(Guid currentAirlines, Guid currentRequest);

        /// <summary>
        /// 传输计划
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        bool TransferPlan(Guid currentAirlines, Guid currentPlan);

        /// <summary>
        /// 传输计划申请
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        /// <param name="currentRequest"></param>
        /// <returns></returns>
        bool TransferPlanAndRequest(Guid currentAirlines, Guid currentPlan, Guid currentRequest);

        /// <summary>
        /// 传输批文
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentApprovalDoc"></param>
        bool TransferApprovalDoc(Guid currentAirlines, Guid currentApprovalDoc);

        /// <summary>
        /// 传输运营历史
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentOperationHistory"></param>
        /// <returns></returns>
        bool TransferOperationHistory(Guid currentAirlines, Guid currentOperationHistory);


        /// <summary>
        /// 传输商业数据
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentAircraftBusiness"></param>
        bool TransferAircraftBusiness(Guid currentAirlines, Guid currentAircraftBusiness);

        /// <summary>
        /// 传输所有权历史
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentOwnershipHistory"></param>
        bool TransferOwnershipHistory(Guid currentAirlines, Guid currentOwnershipHistory);

        bool TransferPlanHistory(Guid currentAirlines, Guid currentPlanHistory);

        /// <summary>
        /// 传输计划申请批文（针对指标飞机数据）
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        /// <param name="currentRequest"></param>
        /// <param name="currentApproval"></param>
        /// <returns></returns>
        bool TransferApprovalRequest(Guid currentAirlines, Guid currentPlan, Guid currentRequest, Guid currentApproval);

        #endregion
    }
}
