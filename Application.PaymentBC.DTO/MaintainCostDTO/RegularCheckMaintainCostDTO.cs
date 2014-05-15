#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 13:37:56
// 文件名：RegularCheckMaintainCostDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 13:37:56
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    /// 定检维修成本
    /// </summary>
    [DataServiceKey("Id")]
    public class RegularCheckMaintainCostDTO : MaintainCostDTO
    {
        #region 属性

        /// <summary>
        ///  主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public int RegularCheckType { get; set; }
        /// <summary>
        /// 定检级别
        /// </summary>
        public string RegularCheckLevel { get; set; }
        #endregion

        #region 外键属性
        public Guid AircraftId { get; set; }
        public Guid ActionCategoryId { get; set; }
        public Guid AircraftTypeId { get; set; }
        #endregion

    }
}
