#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/03/03，14:36
// 方案：FRP
// 项目：Application.PartBC.DTO
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     控制方案DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class ControlSchemeDTO
    {
        /// <summary>
        ///     控制方案ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     维修工作代码
        /// </summary>
        public string WorkCode { get; set; }

        /// <summary>
        ///     控制单位
        /// </summary>
        public string CtrlUnitName { get; set; }

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
    }
}