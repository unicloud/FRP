#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/17，19:11
// 方案：FRP
// 项目：Application.PurchaseBC.DTO
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
    ///     交易DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class TradeDTO
    {
        /// <summary>
        ///     交易ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     交易类型
        ///     <remarks>
        ///         包括购买飞机、租赁飞机、购买发动机、租赁发动机、购买BFE
        ///     </remarks>
        /// </summary>
        public string TradeType { get; set; }

        /// <summary>
        ///     交易编号
        /// </summary>
        public string TradeNumber { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     合作公司外键。
        /// </summary>
        public int SuppierCompanyId { get; set; }

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     是否关闭
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        ///     交易状态
        /// </summary>
        public int Status { get; set; }
    }
}