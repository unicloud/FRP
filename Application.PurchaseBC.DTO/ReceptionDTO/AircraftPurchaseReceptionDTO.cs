#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/11 13:43:58
// 文件名：AircraftPurchaseReceptionDTO
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
    ///  购买飞机接收项目
    /// </summary>
    [DataServiceKey("AircraftPurchaseReceptionId")]
    public partial class AircraftPurchaseReceptionDTO : ReceptionDTO
    {
        private HashSet<AircraftPurchaseReceptionLineDTO> _receptionLines;

        #region 属性
        /// <summary>
        /// 购买飞机接收项目主键
        /// </summary>
        public int AircraftPurchaseReceptionId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     接收行
        /// </summary>
        public virtual ICollection<AircraftPurchaseReceptionLineDTO> ReceptionLines
        {
            get { return _receptionLines ?? (_receptionLines = new HashSet<AircraftPurchaseReceptionLineDTO>()); }
            set { _receptionLines = new HashSet<AircraftPurchaseReceptionLineDTO>(value); }
        }
        #endregion
    }
}
