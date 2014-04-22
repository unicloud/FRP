#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/17，19:11
// 文件名：SupplierCompanyDTO.cs
// 程序集：UniCloud.Application.PurchaseBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     供应商母公司相关信息。
    /// </summary>
    [DataServiceKey("SupplierCompanyId")]
    public class SupplierCompanyDTO
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
        ///     备注。
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 联系人外键
        /// </summary>
        public Guid LinkManId { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     更改日期
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        ///     飞机租赁角色。
        /// </summary>
        public bool AircraftLeaseSupplier { get; set; }

        /// <summary>
        ///     飞机购买角色。
        /// </summary>
        public bool AircraftPurchaseSupplier { get; set; }

        /// <summary>
        ///     BFE角色。
        /// </summary>
        public bool BFEPurchaseSupplier { get; set; }

        /// <summary>
        ///     发动机租赁角色。
        /// </summary>
        public  bool EngineLeaseSupplier { get; set; }

        /// <summary>
        ///     发动机购买角色。
        /// </summary>
        public bool EnginePurchaseSupplier { get; set; }

        /// <summary>
        /// 维修供应商角色
        /// </summary>
        public bool MaintainSupplier { get; set; }

        /// <summary>
        /// 其他供应商角色
        /// </summary>
        public bool OtherSupplier { get; set; }
    }
}