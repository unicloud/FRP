#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/11 17:01:23
// 文件名：ContractEngineDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;


#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///  合同发动机基类
    /// </summary>
    [DataServiceKey("ContractNumber", "RankNumber")]
    public partial class ContractEngineDTO
    {

        #region 属性

        
        /// <summary>
        /// 发动机生产序列号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNumber { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// Rank号
        /// </summary>
        public string RankNumber { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     接收数量
        /// </summary>
        public int ReceivedAmount { get; set; }

        /// <summary>
        ///     接受数量
        /// </summary>
        public int AcceptedAmount { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public virtual string ImportType { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public virtual string ImportActionName { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     引进方式ID
        /// </summary>
        public Guid ImportCategoryId { get; set; }

        /// <summary>
        ///    供应商ID
        /// </summary>
        public int SupplierId { get; set; }
        #endregion
    }
}
