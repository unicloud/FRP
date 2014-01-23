#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/12 11:43:12
// 文件名：ContractDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion
using System;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    /// 维修合同基类
    /// </summary>
    public class MaintainContractDTO
    {
        /// <summary>
        /// 合同号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 签约对象
        /// </summary>
        public string Signatory { get; set; }

        /// <summary>
        /// 签约日期
        /// </summary>
        public DateTime SignDate { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Abstract { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     文档名称
        /// </summary>
        public string DocumentName { get; set; }
        #region 外键属性

        /// <summary>
        /// 签约对象ID
        /// </summary>
        public int SignatoryId { get; set; }

        /// <summary>
        ///     文档ID
        /// </summary>
        public Guid DocumentId { get; set; }
        #endregion
    }
}
