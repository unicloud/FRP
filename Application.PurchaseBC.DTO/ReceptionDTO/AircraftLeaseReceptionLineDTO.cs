#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/11 9:34:33
// 文件名：AircraftLeaseReceptionLineDTO
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
    ///  租赁飞机接收项目
    /// 接收行
    /// </summary>
    [DataServiceKey("AircraftLeaseReceptionLineId")]
    public partial class AircraftLeaseReceptionLineDTO
    {
        /// <summary>
        /// 租赁飞机接收行主键
        /// </summary>
        public int AircraftLeaseReceptionLineId { get; set; }

        /// <summary>
        /// 接收数量
        /// </summary>
 
        public int ReceivedAmount { get; set; }
        /// <summary>
        /// 实际接收数量
        /// </summary>
        public int AcceptedAmount { get; set; }
        /// <summary>
        /// 是否完成
        /// </summary>
 
        public bool IsCompleted { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
 
        public string Note { get; set; }

        /// <summary>
        /// 飞机生产序列号
        /// </summary>
        public string MSN { get; set; }
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
        /// 机型
        /// </summary>
        public string AircraftType { get; set; }
        /// <summary>
        /// 选呼号
        /// </summary>
        public string DailNumber { get; set; }
        /// <summary>
        /// 计划交付时间
        /// </summary>
        public DateTime DeliverDate { get; set; }
        /// <summary>
        /// 计划交付地点
        /// </summary>
        public string DeliverPlace { get; set; }
        /// <summary>
        /// 调机航班号
        /// </summary>
        public string FlightNumber { get; set; }
        /// <summary>
        /// 引进方式
        /// </summary>
        public string ImportCategoryId { get; set; }
        /// <summary>
        /// 引进批文号
        /// </summary>
        public string ApprovalDocNumber { get; set; }

        #region 外键属性
        /// <summary>
        ///     接收ID
        /// </summary>
        public int ReceptionId { get; set; }

        /// <summary>
        ///     租赁合同飞机ID
        /// </summary>
        public int ContractAircraftId { get; set; }

        #endregion

        #region 导航属性
        #endregion

    }
}
