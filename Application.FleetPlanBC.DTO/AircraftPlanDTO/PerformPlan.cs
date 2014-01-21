#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    ///     计划执行情况
    /// </summary>
    [DataServiceKey("Id")]
    public class PerformPlan
    {
        /// <summary>
        ///     主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     计划的批文历史
        /// </summary>
        public ApprovalHistoryDTO ApprovalHistory { get; set; }

        /// <summary>
        ///     计划的运营历史
        /// </summary>
        public OperationHistoryDTO OperationHistory { get; set; }

        /// <summary>
        ///     商业数据
        /// </summary>
        public AircraftBusinessDTO AircraftBusiness { get; set; }
    }
}