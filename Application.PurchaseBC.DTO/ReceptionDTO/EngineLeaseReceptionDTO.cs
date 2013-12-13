#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/11 13:44:43
// 文件名：EngineLeaseReceptionDTO
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
    ///  租赁发动机接收项目
    /// </summary>
    [DataServiceKey("EngineLeaseReceptionId")]
    public partial class EngineLeaseReceptionDTO : ReceptionDTO
    {
        private HashSet<EngineLeaseReceptionLineDTO> _receptionLines;

        #region 属性
        /// <summary>
        /// 租赁发动机接收项目主键
        /// </summary>
        public int EngineLeaseReceptionId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     接收行
        /// </summary>
        public virtual ICollection<EngineLeaseReceptionLineDTO> ReceptionLines
        {
            get { return _receptionLines ?? (_receptionLines = new HashSet<EngineLeaseReceptionLineDTO>()); }
            set { _receptionLines = new HashSet<EngineLeaseReceptionLineDTO>(value); }
        }
        #endregion
    }
}
