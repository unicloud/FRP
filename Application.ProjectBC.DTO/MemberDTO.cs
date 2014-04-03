#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，23:03
// 方案：FRP
// 项目：Application.ProjectBC.DTO
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.ProjectBC.DTO
{
    /// <summary>
    ///     工作组成员DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class MemberDTO
    {
        /// <summary>
        ///     工作组成员ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     是否工作组负责人
        /// </summary>
        public bool IsManager { get; set; }

        /// <summary>
        ///     工作组成员用户ID
        /// </summary>
        public int MemberUserId { get; set; }
    }
}