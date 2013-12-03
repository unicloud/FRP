
using System.Data.Services.Common;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///  采购合同飞机
    /// </summary>
    [DataServiceKey("PurchaseContractAircraftId")]
    public partial class PurchaseContractAircraftDTO : ContractAircraftDTO
    {
        #region 属性
        /// <summary>
        /// 采购合同飞机
        /// </summary>
        public int PurchaseContractAircraftId { get; set; }

        #endregion
    }
}
