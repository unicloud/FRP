#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：MaintainCtrlLineDTO
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     MaintainCtrlLine
    /// </summary>
    [DataServiceKey("Id")]
    public class MaintainCtrlLineDTO
    {
        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     基准间隔
        /// </summary>
        public int StandardInterval { get; set; }

        /// <summary>
        ///     最大间隔
        /// </summary>
        public int MaxInterval { get; set; }

        /// <summary>
        ///     最小间隔
        /// </summary>
        public int MinInterval { get; set; }

        /// <summary>
        ///     控制单位
        /// </summary>
        public string CtrlUnitName { get; set; }

        /// <summary>
        ///     维修工作代码
        /// </summary>
        public string WorkCode { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     控制单位Id
        /// </summary>
        public int CtrlUnitId { get; set; }

        /// <summary>
        ///     维修工作Id
        /// </summary>
        public int MaintainWorkId { get; set; }

        /// <summary>
        ///     维修控制组Id
        /// </summary>
        public int MaintainCtrlId { get; set; }

        #endregion
    }
}