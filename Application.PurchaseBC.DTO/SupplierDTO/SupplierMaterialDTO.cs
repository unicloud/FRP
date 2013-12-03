using System;
using System.Data.Services.Common;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    /// 合作公司物料
    /// </summary>
    [DataServiceKey("SupplierMaterialId")]
    public class SupplierMaterialDTO
    {
        /// <summary>
        ///     主键
        /// </summary>
        public int SupplierMaterialId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     物料Id
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// 合作公司Id
        /// </summary>
        public int SupplierCompanyId { get; set; }
    }
}
