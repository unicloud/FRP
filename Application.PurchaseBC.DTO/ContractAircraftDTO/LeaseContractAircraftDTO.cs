using System;

using System.Data.Services.Common;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///  租赁合同飞机
    /// </summary>
    [DataServiceKey("LeaseContractAircraftId")]
    public partial class LeaseContractAircraftDTO : ContractAircraftDTO
    {
        #region 属性
        /// <summary>
        /// 租赁合同飞机
        /// </summary>
        public int LeaseContractAircraftId { get; set; }

        #endregion
    }
}
