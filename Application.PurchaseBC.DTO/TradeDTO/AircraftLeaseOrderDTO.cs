#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/26，11:57
// 方案：FRP
// 项目：Application.PurchaseBC.DTO
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     租赁飞机订单DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class AircraftLeaseOrderDTO
    {
        private List<ContractContentDTO> _contractContents;
        private List<AircraftLeaseOrderLineDTO> _orderLines;
        private List<RelatedDocDTO> _relatedDocs;

        /// <summary>
        ///     订单ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     交易ID
        /// </summary>
        public int TradeId { get; set; }

        /// <summary>
        ///     合同名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     版本号
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        ///     经办人
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        ///     联系人ID
        /// </summary>
        public int LinkmanId { get; set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     生效日期
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        ///     订单状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     合同文件名
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        ///     合同文档检索ID
        /// </summary>
        public Guid ContractDocGuid { get; set; }

        /// <summary>
        ///     源GUID
        /// </summary>
        public Guid SourceGuid { get; set; }

        /// <summary>
        ///     日志记录
        /// </summary>
        public string LogWriter { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     租赁飞机订单行集合
        /// </summary>
        public virtual List<AircraftLeaseOrderLineDTO> AircraftLeaseOrderLines
        {
            get { return _orderLines ?? (_orderLines = new List<AircraftLeaseOrderLineDTO>()); }
            set { _orderLines = value; }
        }

        /// <summary>
        ///     租赁飞机合同内容集合
        /// </summary>
        public virtual List<ContractContentDTO> ContractContents
        {
            get { return _contractContents ?? (_contractContents = new List<ContractContentDTO>()); }
            set { _contractContents = value; }
        }

        /// <summary>
        ///     关联文档集合
        /// </summary>
        public virtual List<RelatedDocDTO> RelatedDocs
        {
            get { return _relatedDocs ?? (_relatedDocs = new List<RelatedDocDTO>()); }
            set { _relatedDocs = value; }
        }
    }
}