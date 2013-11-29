using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Application.PurchaseBC.DTO
{
   [DataServiceKey("SupplierRoleId")]
   public  class SupplierRoleDTO
    {
        /// <summary>
        ///     主键。
        /// </summary>
        public int SupplierRoleId { get; set; }

        /// <summary>
        ///     是否有效。
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     供应商公司外键。
        /// </summary>
        public int SupplierCompanyId { get; set; }
    }
}
