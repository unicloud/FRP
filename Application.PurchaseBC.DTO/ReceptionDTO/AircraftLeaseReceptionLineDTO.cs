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
    public partial class AircraftLeaseReceptionLineDTO : ReceptionLineDTO
    {
        /// <summary>
        /// 租赁飞机接收行主键
        /// </summary>
        public int AircraftLeaseReceptionLineId { get; set; }
        //飞机注册号
        public string AcReg { get; set; }
        //飞机生产序列号
        public string MSN { get; set; }
        //合同号
        public string ContractNumber { get; set; }
        //机型
        public string AircraftType { get; set; }
        //制造商
        public string Manufacturer { get; set; }
        //发动机型号
        public string EngineType { get; set; }
        //选呼号
        public string DailNumber { get; set; }
        //计划交付时间
        public DateTime DeliverDate { get; set; }
        //计划交付地点
        public string DeliverPlace { get; set; }
        //调机航班号
        public string FlightNumber { get; set; }
        //供应商名称
        public string SuppplierName { get; set; }
        //引进方式
        public string ImportCategoryId { get; set; }
        //引进批文号
        public string ApprovalDocNumber { get; set; }

        #region 外键属性

        /// <summary>
        ///     租赁飞机订单行ID
        /// </summary>
        public int OrderLineId { get; set; }

        #endregion

    }
}
