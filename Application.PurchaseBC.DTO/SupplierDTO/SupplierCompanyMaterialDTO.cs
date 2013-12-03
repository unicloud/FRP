using System.Collections.Generic;
using System.Data.Services.Common;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    /// 供应商物料
    /// </summary>
   [DataServiceKey("SupplierCompanyId")]
   public  class SupplierCompanyMaterialDTO
    {
        /// <summary>
        ///     主键。
        /// </summary>
        public int SupplierCompanyId { get; set; }

        /// <summary>
        ///     名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     类型，分国外，国内；0、国外，1、国内。
        /// </summary>
        public string SupplierType { get; set; }

        /// <summary>
        ///     组织机构代码。
        /// </summary>
        public string Code { get; set; }

       /// <summary>
       /// 飞机物料
       /// </summary>
        public List<SupplierMaterialDTO> AircraftMaterials { get; set; }

        /// <summary>
        /// 发动机物料
        /// </summary>
        public List<SupplierMaterialDTO> EngineMaterials { get; set; }

        /// <summary>
        /// BFE物料
        /// </summary>
        public List<SupplierMaterialDTO> BFEMaterials { get; set; }

        /// <summary>
        ///     飞机角色是否可编辑。
        /// </summary>
        public virtual bool AircraftSupplier { get; set; }

        /// <summary>
        ///     BFE角色是否可编辑。
        /// </summary>
        public virtual bool BFESupplier { get; set; }

        /// <summary>
        ///     发动机角色是否可编辑。
        /// </summary>
        public virtual bool EngineSupplier { get; set; }

    }
}
