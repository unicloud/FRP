#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/16 14:05:07
// 文件名：ReceptionLineDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Application.PurchaseBC.DTO
{
    public partial class ReceptionLineDTO
    {
        // 接收数量
        public int ReceivedAmount { get; set; }
        // 实际接收数量
        public int AcceptedAmount { get; set; }
        // 是否完成
        public bool IsCompleted { get; set; }
        // 备注
        public string Note { get; set; }

        #region 外键属性

        /// <summary>
        ///     接收ID
        /// </summary>
        public int ReceptionId { get; set; }
        #endregion
    }
}
