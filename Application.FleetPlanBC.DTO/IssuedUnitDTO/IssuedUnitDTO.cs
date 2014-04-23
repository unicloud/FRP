#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/22 17:24:52
// 文件名：IssuedUnitDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    ///     管理者
    /// </summary>
    [DataServiceKey("Id")]
    public class IssuedUnitDTO
    {
        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     管理单位中文名称
        /// </summary>
        public string CnName { get; set; }

        /// <summary>
        ///     管理单位中文简称
        /// </summary>
        public string CnShortName { get; set; }

        /// <summary>
        ///     是否内部发文单位
        /// </summary>
        public bool IsInner { get; set; }

        #endregion
    }
}