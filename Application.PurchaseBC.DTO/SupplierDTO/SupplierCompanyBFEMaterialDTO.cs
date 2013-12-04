
using System.Data.Services.Common;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    /// 合作公司发动机物料
    /// </summary>
   [DataServiceKey("SupplierCompanyMaterialId")]
    public class SupplierCompanyBFEMaterialDTO
    {  
        /// <summary>
        /// 主键
        /// </summary>
        public int SupplierCompanyMaterialId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///合作公司Id
        /// </summary>
        public int SupplierCompanyId { get; set; }

       /// <summary>
       /// 物料Id
       /// </summary>
        public int MaterialId { get; set; }
    }
}
