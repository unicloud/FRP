#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/11 13:45:42
// 文件名：EnginePurchaseReceptionDTO
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
    /// </summary>
    [DataServiceKey("EnginePurchaseReceptionId")]
    public partial class EnginePurchaseReceptionDTO : ReceptionDTO
    {
        public EnginePurchaseReceptionDTO()
        {
            ReceptionLines = new List<EnginePurchaseReceptionLineDTO>();
        }

        #region 属性
        /// <summary>
        /// 购买发动机接收项目主键
        /// </summary>
        public int EnginePurchaseReceptionId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     接收行
        /// </summary>
        public List<EnginePurchaseReceptionLineDTO> ReceptionLines { get; set; }
        #endregion
    }
}
