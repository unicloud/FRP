#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 10:34:49
// 文件名：SupplierDTO
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

namespace UniCloud.Application.FleetPlanBC.DTO.SupplierDTO
{
    /// <summary>
    /// 供应商（所有权人）
    /// </summary>
    [DataServiceKey("Id")]
    public class SupplierDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        #endregion
    }
}
