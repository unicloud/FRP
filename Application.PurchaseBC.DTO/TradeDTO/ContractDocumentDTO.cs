#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/15，16:12
// 文件名：OrderDocumentDTO.cs
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
    ///     合同文档
    /// </summary>
    [DataServiceKey("Id")]
    public class ContractDocumentDTO
    {
        /// <summary>
        ///     ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     合同名称
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        ///     合同编号
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        ///     合同文件名
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        ///     合同文档检索ID
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Abstract { get; set; }

        /// <summary>
        /// 文档路径
        /// </summary>
        public string DocumentPath { get; set; }
    }
}