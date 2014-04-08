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

namespace UniCloud.Application.FleetPlanBC.DTO
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

        /// <summary>
        ///     供应商类型
        ///     <remarks>
        ///         国外、国内
        ///     </remarks>
        /// </summary>
        public int SupplierType { get; set; }

        /// <summary>
        ///     组织机构代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     供应商中文名称
        /// </summary>
        public string CnName { get; set; }

        /// <summary>
        ///     供应商英文名称
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        ///     供应商中文简称
        /// </summary>
        public string CnShortName { get; set; }

        /// <summary>
        ///     供应商英文简称
        /// </summary>
        public string EnShortName { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 航空公司Guid
        /// </summary>
        public Guid? AirlineGuid { get; set; }

        #endregion
    }
}
