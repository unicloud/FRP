#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/17，19:11
// 文件名：SupplierDTO.cs
// 程序集：UniCloud.Application.PurchaseBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    [DataServiceKey("SupplierId")]
    public class SupplierDTO
    {
        public SupplierDTO()
        {
            BankAccounts=new List<BankAccountDTO>();
        }

        /// <summary>
        ///     主键。
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     类型，分国外，国内；0、国外，1、国内。
        /// </summary>
        public string SupplierType { get; set; }

        /// <summary>
        ///     创建日期。
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        ///     更改日期。
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        ///     组织机构代码。
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     备注。
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 合作公司外键。
        /// </summary>
        public int SuppierCompanyId { get; set; }

        /// <summary>
        /// 供应商账户信息。
        /// </summary>
        public List<BankAccountDTO> BankAccounts { get; set; }

    }
}