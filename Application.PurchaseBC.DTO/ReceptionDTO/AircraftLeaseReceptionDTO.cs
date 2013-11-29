#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/11 9:34:02
// 文件名：AircraftLeaseReceptionDTO
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
    /// </summary>
    [DataServiceKey("AircraftLeaseReceptionId")]
    public partial class AircraftLeaseReceptionDTO : ReceptionDTO
    {

        public AircraftLeaseReceptionDTO()
        {
            ReceptionLines=new List<AircraftLeaseReceptionLineDTO>();
        }

        #region 属性
        /// <summary>
        /// 租赁飞机接收项目主键
        /// </summary>
        public int AircraftLeaseReceptionId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     接收行
        /// </summary>
        public List<AircraftLeaseReceptionLineDTO> ReceptionLines { get; set; }

        #endregion
    }
}
