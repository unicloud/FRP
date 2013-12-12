#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/11 13:45:56
// 文件名：EnginePurchaseReceptionLineDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///  购买发动机接收项目
    /// 接收行
    /// </summary>
    [DataServiceKey("EnginePurchaseReceptionLineId")]
    public partial class EnginePurchaseReceptionLineDTO : ReceptionLineDTO
    {
        /// <summary>
        /// 购买发动机接收行主键
        /// </summary>
        public int EnginePurchaseReceptionLineId { get; set; }
        //发动机生产序列号
        public string SerialNumber { get; set; }
        //合同号
        public string ContractNumber { get; set; }
        //合同名称
        public string ContractName { get; set; }
        //Rank号
        public string RankNumber { get; set; }
        //引进方式
        public string ImportCategoryId { get; set; }
        //计划交付时间
        public DateTime DeliverDate { get; set; }
        //计划交付地点
        public string DeliverPlace { get; set; }

        #region 外键属性

        /// <summary>
        ///     采购合同发动机ID
        /// </summary>
        public int ContractEngineId { get; set; }

        #endregion
    }
}
