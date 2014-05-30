#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/23，11:31
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
    ///     APU滑油用户DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class APUOilDTO
    {
        /// <summary>
        ///     APU滑油用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     序列号
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        ///     滑油监控状态状态
        /// </summary>
        public int Status { get; set; }
    }
}